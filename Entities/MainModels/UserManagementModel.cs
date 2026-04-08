using Entities.CommonModels;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;

namespace Entities.MainModels
{
    public class UserManagementModel
    {
        public List<AddressTypeEntity>? AddressTypeList { get; set; }
        public List<CountryEntity>? CountriesList { get; set; }
        public List<StateProvinceEntity>? StateProvinceList { get; set; }
        public List<CityEntity>? CitiesList { get; set; }
        public List<UserEntity>? UsersList { get; set; }

        public UserEntity? UserEntityObj { get; set; }
        public List<UserTypesEntity>? UserTypesList { get; set; }
        public List<RolesEntity>? RolesList { get; set; }
        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
