using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalculator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxHomePageView : ContentPage
    {
        public TaxHomePageView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ((TaxHomePageViewModel)this.BindingContext).Appearing();            
            base.OnAppearing();
        }
    }
}