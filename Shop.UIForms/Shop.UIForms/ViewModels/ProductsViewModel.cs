﻿

namespace Shop.UIForms.ViewModels
{
	using Common.Models;
	using Common.Services;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Xamarin.Forms;

	public class ProductsViewModel : BaseViewModel
	{
		private readonly ApiService apiService;
		private ObservableCollection<Product> products;
		private bool isRefreshing;
		public ObservableCollection<Product> Products
		{
			get => this.products;
			set => this.SetValue(ref this.products, value);
		}

		public bool IsRefreshing
		{
			get => this.isRefreshing;
			set => this.SetValue(ref this.isRefreshing, value);
		}

		public ProductsViewModel()
		{
			this.apiService = new ApiService();
			this.LoadProducts();
		}

		private async void LoadProducts()
		{
			this.IsRefreshing = true;
			var response = await this.apiService.GetListAsync<Product>(
							"https://shopweb20190325084710.azurewebsites.net",
							"/api",
							"/Products");
			this.IsRefreshing = false;
			if (!response.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert(
							"Error",
							response.Message,
							"Accept");
			}

			var myProducts = (List<Product>)response.Result;
			this.Products = new ObservableCollection<Product>(myProducts);
		}
	}
}
