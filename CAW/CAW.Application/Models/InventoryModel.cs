using System;
using System.Collections.Generic;
using System.Text;

namespace CAW.Application.Models
{
    public class InventoryModel
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
