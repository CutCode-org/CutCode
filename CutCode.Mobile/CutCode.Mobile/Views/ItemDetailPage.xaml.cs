using CutCode.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CutCode.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}