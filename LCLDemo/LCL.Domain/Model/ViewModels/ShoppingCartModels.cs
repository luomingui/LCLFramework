using LCL.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCL.Domain.Model.ViewModels
{
    public class ShoppingCartModels
    {
        public ShoppingCartModels()
        {
            Items = new List<ShoppingCartItem>();
        }
        public List<ShoppingCartItem> Items { get; set; }

        public decimal Subtotal { get; set; }
    }
}