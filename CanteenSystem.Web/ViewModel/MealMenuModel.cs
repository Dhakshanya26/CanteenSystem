﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CanteenSystem.Web.ViewModel
{
    public class MealMenuModel
    {
        public string Name { get; set; }
        public int Id { get;set; } 
        public decimal? WasPrice { get; set; }
        public decimal Price { get; set; }
        public string DiscountName { get; set; }
        public DateTime AvailableDate { get; set; } 
        public string MealType { get; set; }
        public int AvailabililtyDateId { get; set; }
    }

    public class MealMenuCollectionModel
    {
        [DisplayFormat(DataFormatString = @"{dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SelectedAvailableDate { get; set; }
        public int? SelectedMealType { get; set; }
        public SelectList AvailableMealTypes { get; set; }
        public List<MealMenuModel> MealMenuModels { get; set; }
    }
}