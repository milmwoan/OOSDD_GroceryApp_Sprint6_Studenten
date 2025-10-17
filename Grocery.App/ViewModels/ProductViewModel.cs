using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using Grocery.App.Views;

namespace Grocery.App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        [ObservableProperty]
        Client client;
        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IProductService productService,GlobalViewModel global)
        {
            _productService = productService;
            Products = [];
            client = global.Client;
            foreach (Product p in _productService.GetAll()) Products.Add(p);
        }
        [RelayCommand]
        public async Task ShowNewProducts()
        {
            if (client.Role == Role.Admin)
            {
                NewProductViewModel newProductViewModel = new NewProductViewModel(_productService);
                newProductViewModel.NewProductEvent += RefreshProducts;
                await Shell.Current.GoToAsync(nameof(NewProductView), true);
            }
        }
        public void RefreshProducts()
        {
            Products.Clear();
            foreach (Product p in _productService.GetAll())
            {
                Products.Add(p);
            }
        }
    }
}
