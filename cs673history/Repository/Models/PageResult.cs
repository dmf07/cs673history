using System.Collections.Generic;

namespace cs673history.Repository.Models
{
    public class PageResult<T>
    {
        public long Total { get; set; }
        public List<T> Results { get; set; } = new List<T>();
    }
}
