using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Services
{
    public class ApiOperationServicesDAL : IApiOperationServicesDAL
    {

        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;

        //--Constructor of the class
        public ApiOperationServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }

        public async Task<Apiconfiguration?> GetAPIConfiguration(string UrlName)
        {
            Apiconfiguration? result = new Apiconfiguration();

            using (var repo = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 *")

                        .From("APIConfigurations")
                        .Where("UrlName=@0", UrlName);


                    result = repo.Query<Apiconfiguration>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;

                }
                catch (Exception)
                {

                    throw;
                }


            }


        }


        public async Task<string?> GetApiData(Dictionary<string, object>? requestParameters, Apiconfiguration? apiConfiguration)
        {
            string result = "";
            object queryParams = BuildQueryParams(requestParameters);
            string sqlQuery = apiConfiguration?.SqlQuery ?? string.Empty;
            var (commandText, commandType) = ResolveCommand(sqlQuery);
            List<string> declaredParams = commandType == CommandType.StoredProcedure ? ExtractDeclaredParameters(sqlQuery) : new List<string>();
            DynamicParameters dapperParams = BuildDapperParams(requestParameters, declaredParams);

            try
            {

                if (String.IsNullOrWhiteSpace(apiConfiguration.Ormtype) || apiConfiguration.Ormtype == "PetaPoco")
                {
                    if (requestParameters != null && requestParameters.Count > 0)
                    {
                        using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                        {
                            dbConnection.Open();
                            result = dbConnection.Query<string>(commandText, dapperParams, commandType: commandType).FirstOrDefault();
                            dbConnection.Close();
                            await Task.FromResult(result);
                            return result;
                        }
                    }

                    using (var repo = _contextHelper.GetDataContextHelper())
                    {
                        result = repo.Fetch<string>(apiConfiguration.SqlQuery, queryParams).FirstOrDefault();
                        await Task.FromResult(result);
                        return result;
                    }
                }
                else if (apiConfiguration.Ormtype == "Dapper")
                {
                    using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                    {
                        dbConnection.Open();

                        result = dbConnection.Query<string>(commandText, dapperParams , commandType: commandType).FirstOrDefault();

                        dbConnection.Close();

                        await Task.FromResult(result);
                        return result;
                    }
                }
                else
                {
                    using (var repo = _contextHelper.GetDataContextHelper())
                    {
                        result = repo.Fetch<string>(apiConfiguration.SqlQuery, queryParams).FirstOrDefault();
                        await Task.FromResult(result);
                        return result;
                    }
                }
                

            }
            catch (Exception)
            {

                throw;
            }

        }

        private static object BuildQueryParams(Dictionary<string, object>? requestParameters)
        {
            if (requestParameters == null || requestParameters.Count == 0)
            {
                return new { };
            }

            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in requestParameters)
            {
                expando[item.Key] = item.Value;
            }

            return (ExpandoObject)expando;
        }

        private static DynamicParameters BuildDapperParams(Dictionary<string, object>? requestParameters, List<string>? allowedParamNames = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            if (requestParameters == null || requestParameters.Count == 0)
            {
                return parameters;
            }

            HashSet<string>? allowed = null;
            if (allowedParamNames != null && allowedParamNames.Count > 0)
            {
                allowed = new HashSet<string>(allowedParamNames, StringComparer.OrdinalIgnoreCase);
            }

            foreach (var item in requestParameters)
            {
                if (allowed != null && !allowed.Contains(item.Key))
                {
                    continue;
                }
                parameters.Add(item.Key, item.Value);
            }

            return parameters;
        }

        private static (string commandText, CommandType commandType) ResolveCommand(string sqlQuery)
        {
            string normalized = (sqlQuery ?? string.Empty).Trim();
            if (normalized.StartsWith(";"))
            {
                normalized = normalized.TrimStart(';').Trim();
            }

            if (normalized.StartsWith("EXEC", StringComparison.OrdinalIgnoreCase))
            {
                string afterExec = normalized.Substring(4).Trim();
                string procedureName = afterExec.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
                procedureName = procedureName.Replace("[", string.Empty).Replace("]", string.Empty);

                if (!string.IsNullOrWhiteSpace(procedureName))
                {
                    return (procedureName, CommandType.StoredProcedure);
                }
            }

            return (sqlQuery, CommandType.Text);
        }

        private static List<string> ExtractDeclaredParameters(string sqlQuery)
        {
            if (string.IsNullOrWhiteSpace(sqlQuery))
            {
                return new List<string>();
            }

            var matches = Regex.Matches(sqlQuery, @"@([A-Za-z_][A-Za-z0-9_]*)");
            return matches.Cast<Match>()
                          .Select(x => x.Groups[1].Value)
                          .Distinct(StringComparer.OrdinalIgnoreCase)
                          .ToList();
        }
    }
}
