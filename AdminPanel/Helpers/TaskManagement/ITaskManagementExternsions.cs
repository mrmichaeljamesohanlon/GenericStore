namespace AdminPanel.Helpers.TaskManagement
{
    public interface ITaskManagementExternsions
    {
        Task<string> PersistVendorRequest(int TaskId);
        Task<string> PersistOrderRefundRequest(int TaskId);
    }
}
