using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOL
{
    public class OrderBill
    {
        [Key]
        public int InvoiceNo { get; set; }

        public decimal TotalBill { get; set; }
        public string Status { get; set; }


        public string Id { get; set; }
        public ApplicationUsers ApplicationUsers { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }

    }
}
