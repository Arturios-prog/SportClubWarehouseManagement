using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubWMS.Shared
{
    public class CustomerSportGood
    {
        public int Id { get; set; }

        [ForeignKey("Customers")]
        public int CustomerId { get; set; }
        public int SportGoodId { get; set; }
        public string? SportGoodName { get; set; }
        public uint Quantity { get; set; } = 0;

    }
}
