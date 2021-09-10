using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CutCode
{
    /// <summary>
    /// Interaction logic for NotificationDialogView.xaml
    /// </summary>
    public partial class NotificationDialogView : Window
    {
        public NotificationDialogView()
        {
            InitializeComponent();

            var mainView = System.Windows.Application.Current.Windows[0] as MainView;
            
            if(mainView.WindowState == WindowState.Maximized)
            {
                var currentScreen = Screen.FromHandle(new WindowInteropHelper(mainView).Handle).Bounds;
                Top = currentScreen.Bottom - (Height + 50);
                Left = currentScreen.Right - (Width + 10);
            }
            else
            {
                Top = mainView.Top + mainView.Height - (Height + 10);
                Left = mainView.Left + mainView.Width - (Width + 10);
            }

            var closeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3),
                IsEnabled = true
            };
            closeTimer.Tick += exitBtnClick;
        }

        private void exitBtnClick(object sender, EventArgs e) => Close();
    }
}
