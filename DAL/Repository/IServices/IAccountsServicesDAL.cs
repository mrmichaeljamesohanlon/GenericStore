using Entities.CommonModels.AccountsModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface IAccountsServicesDAL
    {
        Task<List<BankMasterEntity>> GetBankMasterDAL(BankMasterEntity FormData);
        Task<List<BankIndustryTypeEntity>> GetBankIndustryTypeDAL(BankIndustryTypeEntity FormData);
        Task<List<BankStatusEntity>> GetBankStatusesDAL(BankStatusEntity FormData);
        Task<string> SaveUpdateBankMasterDAL(BankMasterEntity FormData, int DataOperationType);
        Task<List<BankAccountEntity>> GetUserBankAccountsDAL(BankAccountEntity FormData);
        Task<List<BankAccountType>> GetBankAccountTypesDAL(BankAccountType FormData);
        Task<string> CreateUpdateUserBankAccount(BankAccountEntity FormData);
        Task<BankAccountEntity?> GetBankAccountDetailByIdDAL(int BankAccountId);
        Task<List<BankAccountAttachmentEntity>> GetBankAccountAttachmentListDAL(BankAccountAttachmentEntity FormData);
        Task<List<VendorsCommissionSetupEntity>> GetVendorsCommissionSetupDAL(VendorsCommissionSetupEntity FormData);
        Task<string> UpdateVendorCommissionDAL(VendorsCommissionSetupEntity FormData, int DataOperationType);
        Task<List<VendorsPayments>> GetVendorsPaymentsListDAL(VendorsPayments FormData);
        Task<List<AccountTransactionsDetail>> GetAccountTransDetailByVendorIdDAL(AccountTransactionsDetail FormData);
        Task<List<BankTransEventEntity>> GetBankTransEventListDAL(BankTransEventEntity FormData);
        Task<string> CreateUpdateBankAccountTransDAL(AccountTransactionsDetail FormData);
        Task<AccountTransactionsDetail?> GetAccountTransEditFormDataByIdDAL(int TransId);
        Task<List<AccountTransAttachmentEntity>> GetAccountTransEditFormAttachmentsDAL(AccountTransAttachmentEntity FormData);
        Task<List<VendorsOrdersTotalReceivedBalance>> GetVendorsOrdersTotalReceivedBalanceDAL(string CommaSeperatedVendorsIds);
    }
}
