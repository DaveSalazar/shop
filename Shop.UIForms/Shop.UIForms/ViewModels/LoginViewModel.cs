

namespace Shop.UIForms.ViewModels
{
	using GalaSoft.MvvmLight.Command;
	using Shop.UIForms.Views;
	using System;
	using System.Windows.Input;
	using Xamarin.Forms;

	public class LoginViewModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public ICommand LoginCommand => new RelayCommand(Login);

		public LoginViewModel()
		{
			this.Email = "aaa@gmail.com";
			this.Password = "1234";
		}
		private async void Login()
		{
			if(string.IsNullOrEmpty(this.Email))
			{
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					"You must enter an email",
					"Accept");
				return;
			}
			if (string.IsNullOrEmpty(this.Password))
			{
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					"You must enter a password",
					"Accept");
				return;
			}
			MainViewModel.GetInstance().Products = new ProductsViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
		}
	}
}
