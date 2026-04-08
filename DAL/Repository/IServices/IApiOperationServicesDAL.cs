using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface IApiOperationServicesDAL
    {
        Task<Apiconfiguration?> GetAPIConfiguration(string UrlName);
        Task<string?> GetApiData(Dictionary<string, object>? requestParameters, Apiconfiguration? apiConfiguration);
    }
}
