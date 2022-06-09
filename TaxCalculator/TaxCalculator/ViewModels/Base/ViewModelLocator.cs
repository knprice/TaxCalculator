using System;
using System.Globalization;
using System.Reflection;
using TaxCalculator.Ioc;
using TaxCalculator.Services;
using Xamarin.Forms;

namespace TaxCalculator.ViewModels.Base
{
    public static class ViewModelLocator
    {

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }
      
        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            App._container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return App._container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Element view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = App._container.Resolve(viewModelType);
            ((ViewModelBase)viewModel).Page = view as Page;
            view.BindingContext = viewModel;
        }
    }
}
