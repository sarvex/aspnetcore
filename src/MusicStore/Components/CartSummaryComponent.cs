﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MusicStore.Models;

namespace MusicStore.Components
{
    [ViewComponent(Name = "CartSummary")]
    public class CartSummaryComponent : ViewComponent
    {
        private MusicStoreContext db = new MusicStoreContext();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = ShoppingCart.GetCart(db, this.Context);

            var cartItems = cart.GetCartItems()
                .Select(a => a.Album.Title)
                .OrderBy(x => x);

            //Bug: Start using ViewBag when that's available in a ViewComponent
            ViewData.Add("CartCount", cartItems.Count());
            ViewData.Add("CartSummary", string.Join("\n", cartItems.Distinct()));

            return View();
        }
    }
}