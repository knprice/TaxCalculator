using TaxCalculator.Ioc;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Views;
using Xamarin.Forms;

namespace TaxCalculator
{
    public partial class App : Application
    {
        public static TinyIoCContainer _container;
        public App()
        {
            InitializeComponent();
            _container = new TinyIoCContainer();
            _container.Register<ITaxService, TaxService>();
            _container.Register<ITaxJarCalculatorService, TaxJarCalculatorService>();
            MainPage = new NavigationPage(new TaxHomePageView());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
