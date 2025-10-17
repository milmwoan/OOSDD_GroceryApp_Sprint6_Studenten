using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.App.ViewModels
{
    public partial class NewProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        [ObservableProperty]
        public string errorMessage = "";
        
        public string Name { get; set; }
        public int Stock { get; set; }
        public DateOnly ShelfLife { get; set; }
        public decimal Price { get; set; }
        public event Action? NewProductEvent;
        public NewProductViewModel(IProductService productService)
        {
            _productService = productService;
            ShelfLife = DateOnly.FromDateTime(DateTime.Today);
            
        }

        [RelayCommand]
        public void AddProduct()
        {
            if (Stock < 0)
            {
                errorMessage = "Voorraad kan niet negatief zijn.";
                return;
            }
            if (Price < 0)
            {
                errorMessage = "Prijs kan niet negatief zijn.";
                return;
            }
            if (ShelfLife < DateOnly.FromDateTime(DateTime.Today))
            {
                errorMessage = "Houdbaarheidsdatum kan niet vóór vandaag zijn.";
                return;
            }


            Product product = new Product(0, Name, Stock, ShelfLife, Price);
            _productService.Add(product);
            
            
            NewProductEvent?.Invoke();
            // Navigate back to productpage
            Shell.Current.GoToAsync("..");
        }
    }
}
