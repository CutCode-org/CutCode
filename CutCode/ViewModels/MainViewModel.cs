using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CutCode
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<Button> leftBarBtns;
        private List<Object> Pages;
        public MainViewModel(List<Button> leftBarBtns)
        {
            this.leftBarBtns = leftBarBtns;
            Pages = new List<Object>() { new HomePage(), new AddPage(), new FavPage(), new SettingPage()};
            currentPage = Pages[0];
        }

        private Object _currentPage { get; set; }
        public Object currentPage
        {
            get => _currentPage;
            set
            {
                if(value != _currentPage)
                {
                    _currentPage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand ChangePageCommand => new RelayCommand<object>(onChangePage);
        void onChangePage(object sender)
        {
            var btn = sender as Button;
            btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#36393F");

            int ind = 0;
            foreach (var b in leftBarBtns)
            {
                if (b != btn) b.Background = Brushes.Transparent;
                else ind = leftBarBtns.IndexOf(b);
            }
            if(currentPage != Pages[ind]) currentPage = Pages[ind];
        }
    }
}
