using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repository.Services
{
    public class UserManagementServicesDAL : IUserManagementServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;


        //--Constructor of the class
        public UserManagementServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }

        public async Task<List<AddressTypeEntity>> GetAddressTypesListDAL(AddressTypeEntity FormData)
        {

            List<AddressTypeEntity> result = new List<AddressTypeEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.AddressTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AddressTypeId =  @0 ", FormData.AddressTypeId);
                    }


                    if (!String.IsNullOrEmpty(FormData.AddressTypeName))
                    {
                        SearchParameters.Append("AND MTBL.AddressTypeName LIKE  @0", "%" + FormData.AddressTypeName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" AddressTypes MTBL")
                      .Where("MTBL.AddressTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CreatedOn DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AddressTypeEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<List<CountryEntity>> GetCountriesListDAL(CountryEntity FormData)
        {

            List<CountryEntity> result = new List<CountryEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.CountryId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CountryId =  @0 ", FormData.CountryId);
                    }


                    if (!String.IsNullOrEmpty(FormData.CountryName))
                    {
                        SearchParameters.Append("AND MTBL.CountryName LIKE  @0", "%" + FormData.CountryName + "%");
                    }
                    if (!String.IsNullOrEmpty(FormData.CountryCode))
                    {
                        SearchParameters.Append("AND MTBL.CountryCode LIKE  @0", "%" + FormData.CountryCode + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Countries MTBL")
                      .Where("MTBL.CountryId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CreatedOn DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<CountryEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }
        public async Task<string> SaveUpdateCountryDAL(CountryEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"IF NOT EXISTS(SELECT TOP 1 EXC.CountryID FROM Countries EXC WHERE EXC.CountryName=@CountryName)
                            BEGIN
                                INSERT INTO Countries(CountryName,	CountryCode,	IsActive,	DisplaySeqNo,	CreatedOn,	CreatedBy)
                                VALUES(@CountryName,	@CountryCode,	@IsActive,	@DisplaySeqNo,	getdate(),	@UserId)
                            END",
                        new
                        {
                            CountryName = FormData.CountryName,
                            CountryCode = FormData.CountryCode,
                            IsActive = FormData.IsActive,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) Countries SET CountryName = @CountryName ,CountryCode = @CountryCode  ,IsActive = @IsActive ,DisplaySeqNo = @DisplaySeqNo, ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE CountryId = @CountryId;",
                       new
                       {
                           CountryId = FormData.CountryId,
                           CountryName = FormData.CountryName,
                           CountryCode = FormData.CountryCode,
                           IsActive = FormData.IsActive,
                           DisplaySeqNo = FormData.DisplaySeqNo,
                           UserId = FormData.UserId,

                       });
                        result = "Saved Successfully!";
                    }
                    else
                    {
                        result = "Please define data operation type!";
                    }


                    //context.EnableAutoSelect = false;

                    //var QueryResponse = context.Execute(@";EXEC [dbo].[SP_LogRunTimeException] @ExceptionMessage,@StackTrace , @Source",
                    //    new { ExceptionMessage = ExceptionMessage, StackTrace = StackTrace, Source = Source });

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public async Task<List<StateProvinceEntity>> GetStateProvinceListDAL(StateProvinceEntity FormData)
        {

            List<StateProvinceEntity> result = new List<StateProvinceEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.StateProvinceId > 0)
                    {
                        SearchParameters.Append("AND MTBL.StateProvinceId =  @0 ", FormData.StateProvinceId);
                    }

                    if (FormData.CountryId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CountryId =  @0 ", FormData.CountryId);
                    }


                    if (!String.IsNullOrEmpty(FormData.StateName))
                    {
                        SearchParameters.Append("AND MTBL.StateName LIKE  @0", "%" + FormData.StateName + "%");
                    }


                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , c.CountryName")
                      .From(" StateProvinces MTBL")
                      .LeftJoin("Countries c").On("MTBL.CountryID = c.CountryID")
                      .Where("MTBL.StateProvinceId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CreatedOn DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<StateProvinceEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<string> SaveUpdateStateProvinceDAL(StateProvinceEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"IF NOT EXISTS(SELECT TOP 1 EXC.StateProvinceId FROM StateProvinces EXC WHERE EXC.StateName=@StateName AND EXC.CountryId=@CountryId)
                            BEGIN
                                INSERT INTO StateProvinces(StateName,	CountryId,	IsActive,	CreatedOn,	CreatedBy)
                                VALUES(@StateName,	@CountryId,	@IsActive,	getdate(),	@UserId)
                            END",
                        new
                        {

                            StateName = FormData.StateName,
                            CountryId = FormData.CountryId,
                            IsActive = FormData.IsActive,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) StateProvinces SET StateName = @StateName ,CountryId = @CountryId  ,IsActive = @IsActive , ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE StateProvinceId = @StateProvinceId;",
                       new
                       {
                           StateProvinceId = FormData.StateProvinceId,
                           StateName = FormData.StateName,
                           CountryId = FormData.CountryId,
                           IsActive = FormData.IsActive,
                           UserId = FormData.UserId,

                       });
                        result = "Saved Successfully!";
                    }
                    else
                    {
                        result = "Please define data operation type!";
                    }




                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public async Task<List<CityEntity>> GetCitiesListDAL(CityEntity FormData)
        {

            List<CityEntity> result = new List<CityEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.CityId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CityId =  @0 ", FormData.CityId);
                    }

                    if (FormData.CountryId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CountryId =  @0 ", FormData.CountryId);
                    }

                    if (FormData.StateProvinceId > 0 || FormData.StateProvinceId == -999) //-- "-999" is in case when there is "other" option to select
                    {
                        SearchParameters.Append("AND MTBL.StateProvinceId =  @0 ", FormData.StateProvinceId);
                    }


                    if (!String.IsNullOrEmpty(FormData.CityName))
                    {
                        SearchParameters.Append("AND MTBL.CityName LIKE  @0", "%" + FormData.CityName + "%");
                    }


                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    if (FormData.CreatedBy != null && FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , c.CountryName , sp.StateName")
                      .From(" Cities MTBL")
                      .LeftJoin("Countries c").On("MTBL.CountryID = c.CountryID")
                      .LeftJoin("StateProvinces sp").On("MTBL.StateProvinceID = sp.StateProvinceID")
                      .Where("MTBL.CityId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CreatedOn DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<CityEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<string> SaveUpdateCityDAL(CityEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"IF NOT EXISTS(SELECT TOP 1 EXC.StateProvinceId FROM Cities EXC WHERE EXC.CityName=@CityName AND EXC.CountryId=@CountryId)
                            BEGIN
                                INSERT INTO Cities(CityName,	CountryId,StateProvinceId  ,	IsActive,	CreatedOn,	CreatedBy)
                                VALUES(@CityName,	@CountryId, @StateProvinceId  ,	@IsActive,	getdate(),	@UserId)
                            END",
                        new
                        {

                            CityName = FormData.CityName,
                            CountryId = FormData.CountryId,
                            StateProvinceId = FormData.StateProvinceId,
                            IsActive = FormData.IsActive,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) Cities SET CityName = @CityName ,CountryId = @CountryId, StateProvinceId=@StateProvinceId  ,IsActive = @IsActive , ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE CityId = @CityId;",
                       new
                       {
                           CityId = FormData.CityId,
                           CityName = FormData.CityName,
                           CountryId = FormData.CountryId,
                           StateProvinceId = FormData.StateProvinceId,
                           IsActive = FormData.IsActive,
                           UserId = FormData.UserId,

                       });
                        result = "Saved Successfully!";
                    }
                    else
                    {
                        result = "Please define data operation type!";
                    }



                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public async Task<List<UserEntity>> GetUsersListDAL(UserEntity FormData)
        {

            List<UserEntity> result = new List<UserEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.UserId > 0)
                    {
                        SearchParameters.Append("AND MTBL.UserID =  @0 ", FormData.UserId);
                    }

                    if (FormData.UserTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.UserTypeId =  @0 ", FormData.UserTypeId);
                    }

                    if (FormData.IsActive != null)
                    {
                        SearchParameters.Append("AND MTBL.IsActive =  @0 ", FormData.IsActive);
                    }

                    if (!String.IsNullOrEmpty(FormData.FirstName))
                    {
                        SearchParameters.Append("AND MTBL.FirstName LIKE  @0", "%" + FormData.FirstName + "%");
                    }
                    if (!String.IsNullOrEmpty(FormData.LastName))
                    {
                        SearchParameters.Append("AND MTBL.LastName LIKE  @0", "%" + FormData.LastName + "%");
                    }
                    if (!String.IsNullOrEmpty(FormData.UserName))
                    {
                        SearchParameters.Append("AND MTBL.UserName LIKE  @0", "%" + FormData.UserName + "%");
                    }
                    if (!String.IsNullOrEmpty(FormData.EmailAddress))
                    {
                        SearchParameters.Append("AND MTBL.EmailAddress LIKE  @0", "%" + FormData.EmailAddress + "%");
                    }
                    if (!String.IsNullOrEmpty(FormData.MobileNo))
                    {
                        SearchParameters.Append("AND MTBL.MobileNo LIKE  @0", "%" + FormData.MobileNo + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , UST.UserTypeName")
                      .From(" Users MTBL")
                      .InnerJoin("UserTypes UST").On("UST.UserTypeId=MTBL.UserTypeId")
                      .Where("MTBL.UserId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.UserId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<UserEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<List<UserTypesEntity>> GetUserTypesListDAL(UserTypesEntity FormData)
        {

            List<UserTypesEntity> result = new List<UserTypesEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.UserTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.UserTypeId =  @0 ", FormData.UserTypeId);
                    }


                    if (!String.IsNullOrEmpty(FormData.UserTypeName))
                    {
                        SearchParameters.Append("AND MTBL.UserTypeName LIKE  @0", "%" + FormData.UserTypeName + "%");
                    }


                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" UserTypes MTBL")
                      .Where("MTBL.UserTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.UserTypeId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<UserTypesEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<string> CreateUpdateUserDAL(UserEntity FormData)
        {
            string result = "";


            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute("SP_AdmPanel_CreateUpdateUser",
                        new
                        {
                            UserIDNewPrimaryKey = FormData.UserId,
                            FirstName = FormData.FirstName,
                            MiddleName = FormData.MiddleName,
                            LastName = FormData.LastName,
                            UserName = "",
                            EmailAddress = FormData.EmailAddress,
                            UserTypeID = FormData.UserTypeId,
                            RoleId = FormData.RoleId,
                            PhoneNo = FormData.PhoneNo,
                            MobileNo = FormData.MobileNo,
                            DateOfBirth = FormData.DateOfBirth,
                            Gender = FormData.Gender,
                            Password = FormData.Password,
                            IsActive = FormData.IsActive,
                            IsVerified = FormData.IsVerified,
                            CountryId = FormData.CountryId,
                            StateProvinceId = FormData.StateProvinceId,
                            CityId = FormData.CityId,
                            AddressLineOne = FormData.AddressLineOne,
                            AddressLineTwo = FormData.AddressLineTwo,
                            PostalCode = FormData.PostalCode,
                            ProfilePictureUrl = FormData.ProfilePictureUrl,
                            DataOperationType = FormData.DataOperationType,

                            UserId = FormData.CreatedBy,
                        }
                        , commandType: CommandType.StoredProcedure);
                    dbConnection.Close();

                    result = "Saved Successfully!";

                    await Task.FromResult(result);
                    return result;

                }




            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<string> CheckIfUserAlreadyExistsDAL(string? UserName, string? EmailAddress)
        {
            string IsExist = "User not exists";

            using (var repo = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    //--if both params are null
                    if (String.IsNullOrWhiteSpace(UserName) && String.IsNullOrWhiteSpace(EmailAddress))
                    {
                        IsExist = "User name and email address both are null";

                        await Task.FromResult(IsExist);
                        return IsExist;
                    }



                    if (!String.IsNullOrWhiteSpace(UserName))
                    {
                        var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 *")
                        .From("Users usr")
                        .Where("USR.UserName=@0", UserName);
                        var user = repo.Query<User>(ppSql).FirstOrDefault();

                        IsExist = user != null && user.UserId > 0 ? "UserName already exists" : "User not exists";
                    }

                    //--Now check if email if user name is ok
                    if (IsExist== "User not exists" && !String.IsNullOrWhiteSpace(EmailAddress))
                    {
                        var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 *")
                        .From("Users usr")
                        .Where("USR.EmailAddress=@0", EmailAddress);
                        var user = repo.Query<User>(ppSql).FirstOrDefault();

                        IsExist = user != null && user.UserId > 0 ? "Email already exists" : "User not exists";
                    }


                    await Task.FromResult(IsExist);
                    return IsExist;

                }
                catch (Exception)
                {

                    throw;
                }


            }


        }


        public async Task<UserEntity?> GetUserCompleteDataByIdDAL(int UserId)
        {
            UserEntity? result = new UserEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;

                    result = context.Fetch<UserEntity>(@";EXEC [dbo].[SP_AdmPanel_GetUserCompleteData] @UserId",
                          new
                          {
                              UserId = UserId

                          }).FirstOrDefault();



                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {

                    throw;
                }


            }


        }

        public async Task<UserEntity?> GetUserByEmailAddressDAL(string EmailAddress)
        {
            UserEntity? result = new UserEntity();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {

                    var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 *")
                      .From("Users usr")
                      .Where("USR.EmailAddress=@0", EmailAddress);
                    result = context.Query<UserEntity>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;

                }
            }
            catch (Exception)
            {
                throw;
            }


        }


        public async Task<string> SaveOTPLogInformationDAL(int StatusCode, string? StatusMsg, string? CustomMsg, int OTP, string? PhoneNo, string? EmailAddress, int? UserID,
            int? NoOfAttempts, bool AllowMultipleAttemps, string? JsonResponse)
        {
            string result = "";


            try
            {

                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute(@"
                    insert into otplogs(StatusCode,	StatusMsg,	CustomMsg, OTP, IsActive,	PhoneNo,	EmailAddress,	UserID,	NoOfAttempts,	AllowMultipleAttemps,	JsonResponse,	OTPCreatedOn)
                    values(@StatusCode	,@StatusMsg,	@CustomMsg,  @OTP , 1 ,	@PhoneNo,	@EmailAddress,	@UserID,	@NoOfAttempts,	@AllowMultipleAttemps,	@JsonResponse,	getdate())",
                        new
                        {
                            StatusCode = StatusCode,
                            StatusMsg = StatusMsg,
                            CustomMsg = CustomMsg,
                            OTP = OTP,
                            PhoneNo = PhoneNo,
                            EmailAddress = EmailAddress,
                            UserID = UserID,
                            NoOfAttempts = NoOfAttempts,
                            AllowMultipleAttemps = AllowMultipleAttemps,
                            JsonResponse = JsonResponse,
                        }
                        , commandType: CommandType.Text);
                    dbConnection.Close();

                    result = "Saved Successfully!";

                    await Task.FromResult(result);
                    return result;

                }

            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<Otplog?> ValidateOTPByEmailDAL(string EmailAddress, int OTP)
        {
            Otplog? result = new Otplog();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {

                    var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 OTPL.EmailAddress")
                      .From("OTPLogs OTPL")
                      .Where("OTPL.IsActive=1 AND OTPL.EmailAddress = @0 AND OTPL.OTP = @1", EmailAddress, OTP)
                      .Append("AND CAST(OTPL.OTPCreatedOn AS DATE) = CAST(GETDATE() AS DATE)")
                      .Append("AND ISNULL(OTPL.NoOfAttempts , 0) = IIF(OTPL.AllowMultipleAttemps = 1  , ISNULL(OTPL.NoOfAttempts , 0) ,  (0)  )")
                      .OrderBy("OTPL.OTPLogID DESC");
                     result = context.Query<Otplog>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;

                }
            }
            catch (Exception)
            {
                throw;
            }


        }


        public async Task<string> ResetUserPasswordDAL(string EmailAddress, string Password)
        {
            string result = "";


            try
            {

                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute(@"UPDATE TOP(10) Users SET Password=@Password WHERE EmailAddress=@EmailAddress",
                        new
                        {
                            EmailAddress = EmailAddress,
                            Password = Password

                        }
                        , commandType: CommandType.Text);
                    dbConnection.Close();

                    result = "Saved Successfully!";

                    await Task.FromResult(result);
                    return result;

                }

            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<string> UpdateOTPAttemptsByEmailDAL(string EmailAddress)
        {
            string result = "";


            try
            {

                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute(@"
                    UPDATE OTPLogs SET NoOfAttempts =((select TOP 1 isnull(NoOfAttempts , 0) from OTPLogs where EmailAddress = @EmailAddress AND IsActive=1) + 1)
                    WHERE EmailAddress = @EmailAddress AND IsActive=1;",
                        new
                        {
                            EmailAddress = EmailAddress
                           
                        }
                        , commandType: CommandType.Text);
                    dbConnection.Close();

                    result = "Saved Successfully!";

                    await Task.FromResult(result);
                    return result;

                }

            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<string> DeActivateOTPsByEmail(string EmailAddress)
        {
            string result = "";


            try
            {

                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute(@" UPDATE OTPLogs SET IsActive= 0 WHERE EmailAddress = @EmailAddress ; ",
                        new
                        {
                            EmailAddress = EmailAddress
                          

                        }
                        , commandType: CommandType.Text);
                    dbConnection.Close();

                    result = "Saved Successfully!";

                    await Task.FromResult(result);
                    return result;

                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<List<UserEntity>> GetUsersListByUserNameDAL(UserEntity FormData)
        {

            List<UserEntity> result = new List<UserEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                
                    if (FormData.IsActive != null)
                    {
                        SearchParameters.Append("AND MTBL.IsActive =  @0 ", FormData.IsActive);
                    }

                   
                    if (!String.IsNullOrEmpty(FormData.UserName))
                    {
                        SearchParameters.Append("AND ( MTBL.UserName LIKE  @0 OR MTBL.FirstName LIKE  @0 OR MTBL.LastName LIKE  @0)", "%" + FormData.UserName + "%");
                    }
                   

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , UST.UserTypeName")
                      .From(" Users MTBL")
                      .InnerJoin("UserTypes UST").On("UST.UserTypeId=MTBL.UserTypeId")
                      .Where("MTBL.UserId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.FirstName ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<UserEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

    }
}
