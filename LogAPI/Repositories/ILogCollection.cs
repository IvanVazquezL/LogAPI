using LogAPI.Models;

namespace LogAPI.Repositories
{
    public interface ILogCollection
    {
        Task InsertLog(Log log);
        Task UpdateLog(Log log);
        Task DeleteLog(string id);

        Task<List<Log>> GetAllLogs();
        Task<List<Log>> GetLogsByDate(DateTime date);
        Task<List<Log>> GetLogsByPageId(string pageId);
        Task<List<Log>> GetLogsByUserId(string userId);
        Task<List<string>> GetUniqueViewedPagesByDateAndUser(string userId, DateTime startDate, DateTime endDate);
        Task<List<string>> GetLoyalCustomers(DateTime dateOne, DateTime dateTwo);
    }
}
