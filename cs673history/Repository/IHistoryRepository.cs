using System.Threading.Tasks;

namespace cs673history.Repository
{
    public interface IHistoryRepository
    {
        Task CreateDatabase();
    }
}
