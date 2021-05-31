using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Currency_Converter
{

    public sealed partial class CurrencyChoose : UserControl
    {
        private Scripts.Currency targetCurrency;
        public Scripts.Currency TargetCurrency
        {
            get => targetCurrency;
            set
            {
                if (targetCurrency != value)
                {
                    targetCurrency = value;
                    TargetChanged(value);
                }
            }
        }

        public delegate void CurrencyChanged(Scripts.Currency a);

        //Событие OnCount c типом делегата MethodContainer.
        public event CurrencyChanged TargetChanged;

        public ObservableCollection<Scripts.Currency> Currencies { get; set; }
        public CurrencyChoose()
        {
            this.InitializeComponent();
        }

        public void DataIsLoaded(Scripts.Data data)
        {
            Currencies = new ObservableCollection<Scripts.Currency>();
            foreach (Scripts.Currency a in data.Valute)
                Currencies.Add(a);
            TargetCurrency = Currencies[0];
        }

        private void ScrollViewer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TargetCurrency = (Scripts.Currency)CurrencyList.SelectedItem;
            this.Visibility = Visibility.Collapsed;
        }
    }
}
