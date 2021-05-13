using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
       
        public int Quantity { get; set; }

        public decimal Price { get; set; }


        //[ForeignKey("Items")]
        public int ItemsId { get; set; }
        public ICollection<Items> Items { get; set; }


        [ForeignKey("OrderBill")]
        public int InvoiceNo { get; set; }
        public OrderBill OrderBill { get; set; }
    }
}
