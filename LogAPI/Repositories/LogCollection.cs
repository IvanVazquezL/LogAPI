using LogAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogAPI.Repositories
{
    public class LogCollection : ILogCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Log> Collection;

        public LogCollection()
        {
            Collection = _repository.db.GetCollection<Log>("Logs");
        }

        public async Task InsertLog(Log log)
        {
            await Collection.InsertOneAsync(log);
        }

        public async Task UpdateLog(Log log)
        {
            var filter = Builders<Log>.Filter.Eq(s => s.Id, log.Id);
            await Collection.ReplaceOneAsync(filter, log);
        }

        public async Task DeleteLog(string id)
        {
            var filter = Builders<Log>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<Log>> GetAllLogs()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<List<Log>> GetLogsByPageId(string pageId)
        {
            var filter = Builders<Log>.Filter.Eq(log => log.PageId, pageId);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<List<Log>> GetLogsByUserId(string userId)
        {
            var filter = Builders<Log>.Filter.Eq(log => log.UserId, userId);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<List<Log>> GetLogsByDate(DateTime date)
        {
            DateTime startDate = date.Date;
            DateTime endDate = startDate.AddDays(1).AddTicks(-1);

            var filter = Builders<Log>.Filter.Gte(log => log.Timestamp, startDate) &
                         Builders<Log>.Filter.Lt(log => log.Timestamp, endDate);

            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<List<string>> GetLoyalCustomers(DateTime dateOne, DateTime dateTwo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetUniqueViewedPagesByDateAndUser(string userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
