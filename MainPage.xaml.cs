using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Currency_Converter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double leftValue = 0;
        private double rightValue = 0;

        public CurrencyChoose leftCurrencyChoose;
        public CurrencyChoose rightCurrencyChoose;
        public MainPage()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(AppTitleBar);


            this.InitializeComponent();

            Scripts.webAPI request = new Scripts.webAPI();

            leftCurrencyChoose = new CurrencyChoose();
            rightCurrencyChoose = new CurrencyChoose();

            request.JsonLoaded += rightCurrencyChoose.DataIsLoaded;
            request.JsonLoaded += leftCurrencyChoose.DataIsLoaded;
            request.JsonLoaded += DataIsLoaded;

            leftCurrencyChoose.TargetChanged += leftCurrencyChanged;
            rightCurrencyChoose.TargetChanged += rightCurrencyChanged;

            request.RequestAsync();

            

            Windows.UI.ViewManagement.ApplicationView appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.SetPreferredMinSize(new Size(300, 400));

        }

        public void DataIsLoaded(Scripts.Data data) 
        {
            LoadingRing.IsActive = false;
            Area.Visibility = Visibility.Visible;
            LeftCurrency.Text = data.Valute[0].Value.ToString();
            RightCurrency.Text = data.Valute[1].Value.ToString();


            Area.Children.Add(leftCurrencyChoose);
            Area.Children.Add(rightCurrencyChoose);
            leftCurrencyChoose.Visibility = Visibility.Collapsed;
            rightCurrencyChoose.Visibility = Visibility.Collapsed;
        }

        public void leftCurrencyChanged(Scripts.Currency currency)
        {
            LeftCurrency.Text = currency.Value.ToString();
            LeftCurrencyName.Text = currency.CharCode.ToString();
        }

        public void rightCurrencyChanged(Scripts.Currency currency)
        {
            RightCurrency.Text = currency.Value.ToString();
            RightCurrencyName.Text = currency.CharCode.ToString();
        }

        private void LeftCurrencyChange_Click(object sender, RoutedEventArgs e)
        {
            leftCurrencyChoose.Visibility = Visibility.Visible;
        }

        private void RightCurrencyChange_Click(object sender, RoutedEventArgs e)
        {
            rightCurrencyChoose.Visibility = Visibility.Visible;
        }

        private void FontIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CurrencyChoose middleCurrencyChoose = leftCurrencyChoose;
            leftCurrencyChoose = rightCurrencyChoose;
            rightCurrencyChoose = middleCurrencyChoose;
        }
        private void LeftCurrency_TextChanged(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            int oneDot = 0;
            sender.Text = new String(sender.Text.Where(c => (char.IsDigit(c) || (c == ',' ? (oneDot++ < 1) : false))).ToArray());
            leftValue = Convert.ToDouble(sender.Text);
            if (leftCurrencyChoose.TargetCurrency != null && rightCurrencyChoose.TargetCurrency != null)
            {
                rightValue = leftValue / leftCurrencyChoose.TargetCurrency.Value * rightCurrencyChoose.TargetCurrency.Value;
                RightCurrency.Text = rightValue.ToString();
            }
        }

        private void RightCurrency_TextChanged(TextBox sender, TextBoxTextChangingEventArgs e)
        {
            int oneDot = 0;
            sender.Text = new String(sender.Text.Where(c => (char.IsDigit(c) || (c == ',' ? (oneDot++ < 1) : false))).ToArray());
            rightValue = Convert.ToDouble(sender.Text);
            if (leftCurrencyChoose.TargetCurrency != null && rightCurrencyChoose.TargetCurrency != null)
            {
                leftValue = rightValue * leftCurrencyChoose.TargetCurrency.Value / rightCurrencyChoose.TargetCurrency.Value;
                LeftCurrency.Text = leftValue.ToString();
            }
        }

    }
}
