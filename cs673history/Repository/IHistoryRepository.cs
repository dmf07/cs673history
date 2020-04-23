using System.Collections.Generic;
using System.Threading.Tasks;
using cs673history.Repository.Models;

namespace cs673history.Repository
{
    public interface IHistoryRepository
    {
        Task CreateDatabase();
        Task SaveHistory(HistoryItem historyItem);
        Task<IEnumerable<HistoryItem>> GetHistory(string user);
        Task DeleteHistoryItem(string id, string user);
    }
}
