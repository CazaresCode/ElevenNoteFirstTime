﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryListItem
    {
        public int CategoryId { get; set; }


        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
