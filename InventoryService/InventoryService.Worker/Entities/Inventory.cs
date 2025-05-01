using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Worker.Entities
{
    public class Inventory
    {
        [Key]
        public Guid ProductId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
