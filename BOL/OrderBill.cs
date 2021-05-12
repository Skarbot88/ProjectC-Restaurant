﻿using System;
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

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
