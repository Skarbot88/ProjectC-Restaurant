using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }


        public int ItemsId { get; set; }
        public virtual IEnumerable<Items> Items { get; set; }

        public string Id { get; set; }
        public virtual ApplicationUsers ApplicationUsers { get;set;}
    }
}
