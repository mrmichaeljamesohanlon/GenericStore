using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class DataContextHelper : IDataContextHelper
    {
        private readonly IConfiguration _configuration;


        public DataContextHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            ConnetionString = _configuration.GetConnectionString("DBConnection");
            providerName = "System.Data.SqlClient";
        }
        public string ConnetionString { get; }
        public string providerName { get; }

        public StorePortalConnDB GetDataContextHelper(bool enableAutoSelect = true)
        {

            return (GetNewDataContext(ConnetionString, providerName, enableAutoSelect));
        }

        private static StorePortalConnDB GetNewDataContext(string ConnetionString, string providerName, bool enableAutoSelect)
        {
            StorePortalConnDB repository = new StorePortalConnDB(ConnetionString, providerName);
            repository.EnableAutoSelect = enableAutoSelect;
            //repository.ELHelperInstance = elHelperInstance;



            return (repository);
        }
    }
}
