using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaxCalculator.Services;
using Xamarin.Forms;

namespace TaxCalculator.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {        
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        private Page _page;
        public Page Page
        {
            get { return _page; }
            set
            {
                _page = value;
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        public ViewModelBase()
        {

        }
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
