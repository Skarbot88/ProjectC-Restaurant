﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.ViewModels
{
    public class CartVM
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public string Course { get; set; }

        [Display(Name ="Item")]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [Display(Name ="Total Price")]
        public decimal TotalPrice { get; set; }




    }
}
