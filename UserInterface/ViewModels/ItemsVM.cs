using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.ViewModels
{
    public class ItemsVM
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Course Name is required.")]
        public string Course { get; set; }

        public SelectList CourseList { get; set; }


        [Required(ErrorMessage = "Stock Quantity is required.")]
        public int InStock { get; set; }
    }
}
