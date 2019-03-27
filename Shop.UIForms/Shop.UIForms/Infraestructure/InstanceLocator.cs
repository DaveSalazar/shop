
namespace Shop.UIForms.Infraestructure
{
	using ViewModels;
	class InstanceLocator
	{
		public MainViewModel Main { get; set; }

		public InstanceLocator()
		{
			this.Main = new MainViewModel();
		}
	}
}
