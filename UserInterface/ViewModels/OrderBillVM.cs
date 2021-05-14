using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.ViewModels
{
    public class OrderBillVM
    {
        public int InvoiceNo { get; set; }
        public string Items { get; set; }

        [Display(Name ="Total Bill")]
        public decimal TotalBill { get; set; }

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Display(Name ="Contact No")]
        public string ContactNo { get; set; }

        public string Address { get; set; }

        public SelectList OrderStatusList { get; set; }
    }
}
