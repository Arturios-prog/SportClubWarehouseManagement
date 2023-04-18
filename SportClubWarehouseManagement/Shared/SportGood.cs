using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubWMS.Shared
{
    public class SportGood
    {
        public int SportGoodId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The Name is too long.")]
        public string Name { get; set; } = string.Empty;
        [StringLength(400, ErrorMessage = "Description is too long")]
        public string Description { get; set; } = string.Empty;
        public SportCategory Category { get; set; }
        public ICollection<Customer> Customers { get; set; }
        = new List<Customer>();
        [Required]
        public uint Quantity { get; set; }
    }
}
