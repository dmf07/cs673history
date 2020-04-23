using System;
using System.ComponentModel.DataAnnotations;

namespace cs673history.Repository.Models
{
    public class DeleteHistoryItem
    {
        [Required]
        public Guid Id { get; set; }
    }
}
