using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SGPF.ViewModel;

namespace SGPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;
        private LoginPopup _currentLoginPopUp;

        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = (MainViewModel)DataContext;
            _mainViewModel.PropertyChanged += _mainViewModel_PropertyChanged;
            Loaded += MainWindow_Loaded;
            
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _mainViewModel_PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        void _mainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_mainViewModel.CurrentSession == null)
            {
                _currentLoginPopUp = new LoginPopup();
                _currentLoginPopUp.ShowDialog();
            }
            else
            {
                if (e.PropertyName.Equals(MainViewModel.CurrentUserPropertyName))
                {
                    _currentLoginPopUp.Close();
                    _currentLoginPopUp = null;

                }
            }
        }

        private void OpenLoginPopUp()
        {
        }

        private void LoginClicked(object sender, RoutedEventArgs e)
        {
            _mainViewModel_PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        private void OnNewProjectClicked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
