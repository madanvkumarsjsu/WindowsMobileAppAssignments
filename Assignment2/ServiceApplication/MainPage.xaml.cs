using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
namespace ServiceApplication
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PDFDownload.xaml", UriKind.Relative));
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TextDownload.xaml", UriKind.Relative));
        }
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ImageDownload.xaml", UriKind.Relative));
        }
        
    }
}