﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDestopUI.Library.Api;
using TRMDestopUI.Library.Helpers;
using TRMDestopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndpoint _productEndpoint;
        IConfigHelper _configHelper;
        
        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper)
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        private BindingList<ProductModel> _products;
        private int _itemQuantity = 1;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set { 
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            { 
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
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
                return CalculateSubTotal().ToString("C");
            }
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;

            foreach (var item in Cart)
            {
                subTotal += (item.Product.RetailPrice * item.QuantityInCart);
            }
            return subTotal;
        }
        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate()/100;

            foreach (var item in Cart)
            {
                if (item.Product.IsTaxable)
                {
                    taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
                }
            }
            return taxAmount;
        }

        public string Tax
        {
            get {return CalculateTax().ToString("C");}
        }

        public string Total
        {
            get
            {
                decimal total = CalculateSubTotal() + CalculateTax();

                return total.ToString("C");
            }
        }

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set { 
                _itemQuantity = value; 
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                //Check for selected and actual qty
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }
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
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;

                //TODO This is a hack: theres got to be a better way to refresh
                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };

                Cart.Add(item);
            }
      
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public void Checkout()
        {

        }
    }
}