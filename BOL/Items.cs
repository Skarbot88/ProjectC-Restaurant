using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOL
{
    public class Items
    {
        [Key]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Course { get; set; }
        public int InStock { get; set; }


        public Cart Cart { get; set; }
         public OrderDetail OrderDetail { get; set; }
    }
}
