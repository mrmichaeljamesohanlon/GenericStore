using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;

namespace DAL.Repository.IServices
{
    public interface IUserManagementServicesDAL
    {
        Task<List<AddressTypeEntity>> GetAddressTypesListDAL(AddressTypeEntity FormData);
         Task<List<CountryEntity>> GetCountriesListDAL(CountryEntity FormData);
        Task<string> SaveUpdateCountryDAL(CountryEntity FormData, int DataOperationType);
        Task<List<StateProvinceEntity>> GetStateProvinceListDAL(StateProvinceEntity FormData);
        Task<string> SaveUpdateStateProvinceDAL(StateProvinceEntity FormData, int DataOperationType);
        Task<List<CityEntity>> GetCitiesListDAL(CityEntity FormData);
        Task<string> SaveUpdateCityDAL(CityEntity FormData, int DataOperationType);
        Task<List<UserEntity>> GetUsersListDAL(UserEntity FormData);
        Task<List<UserTypesEntity>> GetUserTypesListDAL(UserTypesEntity FormData);
        Task<string> CreateUpdateUserDAL(UserEntity FormData);
        Task<string> CheckIfUserAlreadyExistsDAL(string? UserName, string? EmailAddress);
        Task<UserEntity?> GetUserCompleteDataByIdDAL(int UserId);
        Task<UserEntity?> GetUserByEmailAddressDAL(string EmailAddress);
        Task<string> SaveOTPLogInformationDAL(int StatusCode, string? StatusMsg, string? CustomMsg,int OTP, string? PhoneNo, string? EmailAddress, int? UserID,
            int? NoOfAttempts, bool AllowMultipleAttemps, string? JsonResponse);
        Task<Otplog?> ValidateOTPByEmailDAL(string EmailAddress , int OTP);
        Task<string> ResetUserPasswordDAL(string EmailAddress, string Password);
        Task<string> UpdateOTPAttemptsByEmailDAL(string EmailAddress);
        Task<string> DeActivateOTPsByEmail(string EmailAddress);
        Task<List<UserEntity>> GetUsersListByUserNameDAL(UserEntity FormData);
    }
}
