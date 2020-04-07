using System.Threading.Tasks;
using cs673history.Repository.Models;

namespace cs673history.Repository
{
    public interface IHistoryRepository
    {
        Task CreateDatabase();
        Task SaveHistory(HistoryItem historyItem);
    }
}
