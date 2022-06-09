using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.UIModels;
using TaxCalculator.ViewModels.Base;
using Xamarin.Forms;

namespace TaxCalculator.ViewModels
{
    public class TaxHomePageViewModel : ViewModelBase
    {
        #region Public/Private Properties
        
        private bool _retrievingData = false;
        public bool RetrievingData
        {
            get 
            { 
                return _retrievingData; 
            }
            set 
            { 
                _retrievingData = value;
                RaisePropertyChanged();
            }
        }
        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                RaisePropertyChanged();
                LocationLoaded = _location != null;                    
            }
        }
        private string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }
        private string _country = "US";
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                RaisePropertyChanged();
            }
        }

        private string _confirmedZip;
        public string ConfirmedZip
        {
            get
            {
                return _confirmedZip;
            }
            set
            {
                _confirmedZip = value;
                RaisePropertyChanged();
            }
        }
        private bool _locationLoaded;
        public bool LocationLoaded
        {
            get
            {
                return _locationLoaded;
            }
            set
            {
                _locationLoaded = value;
                RaisePropertyChanged();
            }
        }
        private bool _taxForOrderRetrieved;
        public bool TaxForOrderRetrieved
        {
            get
            {
                return _taxForOrderRetrieved;
            }
            set
            {
                _taxForOrderRetrieved = value;
                RaisePropertyChanged();
            }
        }
        private string _cityTaxRate;
        public string CityTaxRate
        {
            get
            {
                return _cityTaxRate;
            }
            set
            {
                _cityTaxRate = value;
                RaisePropertyChanged();
            }
        }
        private string _countyTaxRate;
        public string CountyTaxRate
        {
            get
            {
                return _countyTaxRate;
            }
            set
            {
                _countyTaxRate = value;
                RaisePropertyChanged();
            }
        }
        private string _stateTaxRate;
        public string StateTaxRate
        {
            get
            {
                return _stateTaxRate;
            }
            set
            {
                _stateTaxRate = value;
                RaisePropertyChanged();
            }
        }

        private string _totalTaxRate;
        public string TotalTaxRate
        {
            get
            {
                return _totalTaxRate;
            }
            set
            {
                _totalTaxRate = value;
                RaisePropertyChanged();
            }
        }
        private string _shipping = "Ground - Free";
        public string Shipping
        {
            get
            {
                return _shipping;
            }
            set
            {
                _shipping = value;
                RaisePropertyChanged();
            }
        }
        private decimal _shippingAmount = 0;
        private decimal _subTotal = 33.33M;
        public decimal SubTotal
        {
            get
            {
                return _subTotal;
            }
            set
            {
                _subTotal = value;
                RaisePropertyChanged();
            }
        }
        private string _finalTotal;
        public string FinalTotal
        {
            get
            {
                return _finalTotal;
            }
            set
            {
                _finalTotal = value;
                RaisePropertyChanged();
            }
        }
        
        private readonly string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
        private string _zip;
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
                RaisePropertyChanged();
                if (_zip != null)
                    ((Command)LoadLocationTaxCommand).ChangeCanExecute();
            }
        }
        private decimal _total;
        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                RaisePropertyChanged();
            }
        }
        private string _shippingFromstate = "";
        public string ShippingFromState
        {
            get
            {
                return _shippingFromstate;
            }
            set
            {
                _shippingFromstate = value;
                RaisePropertyChanged();
                TaxForOrderRetrieved = false;
            }
        }
        
        private ObservableCollection<string> _states = new ObservableCollection<string>()
        {"Alabama-AL",
        "Alaska-AK",
        "Arizona-AZ",
        "Arkansas-AR",
        "California-CA",
        "Colorodo-CO",
        "Connecticut-CT",
        "Delaware-DE",
        "Florida-FL",
        "Georgia-GA",
        "Hawaii-HI",
        "Idaho-ID",
        "Illinois-IL",
        "Indiana-IN",
        "Iowa-IA",
        "Kansas-KS",
        "Kentucky-KY",
        "LouisianaLA",
        "Maine-ME",
        "Maryland-MD",
        "Massachusetts-MA",
        "Michigan-MI",
        "Minnesota-MN",
        "Mississippi-MS",
        "Missouri-MO",
        "Montana-MT",
        "Nebraska-NE",
        "Nevada-NV",
        "New Hampshire-NH",
        "New Jersey-NJ",
        "New Mexico-NM",
        "New York-NY",
        "North Carolina-NC",
        "North Dakota-ND",
        "Ohio-OH",
        "Oklahoma-OK",
        "Oregon-OR",
        "Pennsylvania-PA",
        "Rhode Island-RI",
        "South Carolina-SC",
        "South Dakota-SD",
        "Tennessee-TN",
        "Texas-TX",
        "Utah-UT",
        "Vermont-VT",
        "Virginia-VA",
        "Washington-WA",
        "West Virginia-WV",
        "Wisconsin-WI",
        "Wyoming-WY"};
        public ObservableCollection<string> States
        {
            get
            {
                return _states;
            }
            set
            {
                _states = value;
                RaisePropertyChanged();
            }
        }


        private Command _selectShippingCommand;
        public ICommand SelectShippingCommand
        {
            get
            {
                if (_selectShippingCommand == null)
                {
                    _selectShippingCommand = new Command(SelectShippingExecute);
                }
                return _selectShippingCommand;
            }
        }
        private Command _calculateTaxCommand;
        public ICommand CalculateTaxCommand
        {
            get
            {
                if (_calculateTaxCommand == null)
                {
                    _calculateTaxCommand = new Command(CalculateTaxExecute, CalculateTaxCommandCanExecute);
                }
                return _calculateTaxCommand;
            }
        }       

        private Command _loadLocationTaxCommand;
        public ICommand LoadLocationTaxCommand
        {
            get
            {
                if (_loadLocationTaxCommand == null)
                {
                    _loadLocationTaxCommand = new Command(LoadLocationTaxExecute, LocationTaxCommandCanExecute);
                }
                return _loadLocationTaxCommand;
            }
        }
        private ITaxService _taxService;
        #endregion

        #region Constructor/Loading Events
        public TaxHomePageViewModel(ITaxService taxService)
        {
            _taxService = taxService;
        }
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
        internal void Appearing()
        {
            Title = "Tax Calculator";
            LocationLoaded = false;
            TaxForOrderRetrieved = false;
        }
        #endregion

        #region Command Execution Methods
        /// <summary>
        /// loads local tax info to the UI.
        /// </summary>
        /// <param name="obj"></param>
        private async void LoadLocationTaxExecute(object obj)
        {
            var taxRequest = new TaxRateRequest();        
            taxRequest.PostalCode = Zip;
            try
            {
                SetDataRetrievalStatus(true);
                var response = await _taxService.GetTaxRateForLocation(taxRequest);
                if (response == null)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "There was an issue finding your Zipcode. Please try again later.", "OK");
                }
                else
                {
                    var selectedState = States.Where(x => x.Substring(x.Count()-2,2) == response.Rate.State).FirstOrDefault();
                    ConfirmedZip = response.Rate.Zip;
                    Location = response.Rate.City + ", " + response.Rate.State;
                    State = response.Rate.State;
                    ShippingFromState = selectedState;
                    CityTaxRate = response.Rate.CityRate;
                    CountyTaxRate = response.Rate.CountyRate;
                    StateTaxRate = response.Rate.StateRate;
                    TotalTaxRate = response.Rate.CombinedRate;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "There was an issue finding your Zipcode. Please try again later.", "OK");
                LocationLoaded = false;
            }
            finally
            {
                SetDataRetrievalStatus(false);
                TaxForOrderRetrieved = false;
            }
        }

        private async void CalculateTaxExecute(object obj)
        {
            var order = new OrderTaxRequest()
            {
                Amount = SubTotal,
                Shipping = _shippingAmount,
                ToZip = ConfirmedZip,
                FromState = ShippingFromState.Substring(ShippingFromState.Count() - 2, 2),
                ToState = State,
                ToCountry = Country,
                FromCountry = Country
            };
            try
            {
                SetDataRetrievalStatus(true);
                var finalTax = await _taxService.GetTaxForOrder(order);
                Total = finalTax.TotalAmount;
                TaxForOrderRetrieved = true;
            }
            catch (Exception ex)
            {
                TaxForOrderRetrieved = false;
                await App.Current.MainPage.DisplayAlert("Alert", "There was an issue calculating your tax. Please try again later.", "OK");
                LocationLoaded = false;
            }
            finally
            {
                SetDataRetrievalStatus(false);
            }
            
        }

        private async void SelectShippingExecute(object obj)
        {
            const string ground = "Ground - Free";
            const string twoDay = "2 Day - $5.00";
            const string overNight = "Overnight - $10.00";
            var action = await Application.Current.MainPage.DisplayActionSheet("Shipping Options", "Cancel", null, ground, twoDay, overNight);

            //We want to hide the total if the shipping is changed so that the total will not reflect an incorrect amount.
            if (action != Shipping)
                TaxForOrderRetrieved = false;

            switch (action)
            {
                case "Cancel":
                    break;
                case ground:
                    Shipping = ground;
                    _shippingAmount = 0;
                    break;
                case twoDay:
                    Shipping = twoDay;
                    _shippingAmount = 5;
                    break;
                case overNight:
                    Shipping = overNight;
                    _shippingAmount = 10;
                    break;
            }
            

        }

      

        #endregion

        #region CommandCanExecute Methods        
        /// <summary>
        /// Only Executable if Zip matches Regex for US Zip codes.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool LocationTaxCommandCanExecute(object arg)
        {
            var retVal = false;
            if (Zip != null && Regex.Match(Zip, _usZipRegEx).Success && !RetrievingData)
            {
                retVal = true;
            }
            return retVal;
        }
        /// <summary>
        /// Can only be executed if Zipcode has been confirmed as being authentic from the TaxService.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CalculateTaxCommandCanExecute(object arg)
        {
            var retVal = false;
            if (ConfirmedZip != null && !RetrievingData)
                retVal = true;
            return retVal;
        }


        #endregion

        #region Misc Functions
        /// <summary>
        /// This enables or disables buttons if data is being retrieved.
        /// </summary>
        /// <param name="status"></param>
        private void SetDataRetrievalStatus(bool status)
        {
            RetrievingData = status;
            ((Command)LoadLocationTaxCommand).ChangeCanExecute();
            ((Command)CalculateTaxCommand).ChangeCanExecute();            
        }
        #endregion

    }
}
