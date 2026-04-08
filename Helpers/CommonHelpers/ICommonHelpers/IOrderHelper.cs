using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.CommonHelpers.ICommonHelpers
{
    public interface IOrderHelper
    {
        Task<string?> SaveCustomerOrderInDbWithRetry(Dictionary<string, object>? requestParametersRawOrder, Apiconfiguration? ApiConfiguration);
    }
}
