﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<string> _products;
        private int _itemQuantity;

        public BindingList<string> Products
        {
            get { return _products; }
            set { 
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set { 
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        

        public string SubTotal
        {
            get 
            {
                //TODO replace with calculation
                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                //TODO replace with calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                //TODO replace with calculation
                return "$0.00";
            }
        }



        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set { 
                _itemQuantity = value; 
                NotifyOfPropertyChange(() => ItemQuantity); 
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                //Check for selected and actual qty
                return output;
            }

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                //Check for selected 
                return output;
            }

        }
        
        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                //Check the cart has items 
                return output;
            }
        }

        public void AddToCart()
        {

        }

        public void Checkout()
        {

        }
    }
}
