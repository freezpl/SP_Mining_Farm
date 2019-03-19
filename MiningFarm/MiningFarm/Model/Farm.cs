using MiningFarm.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiningFarm.Model
{
    public class Farm
    {
        public ObservableCollection<Currency> Currencies { get; set; }
        public ObservableCollection<VideoCard> Cards { get; set; }

        public Farm()
        {
            Currencies = new ObservableCollection<Currency>()
            {
                new GlobalCurrency("BTC", 3989),
                new GlobalCurrency("ETH", 138),
                new GlobalCurrency("LTC", 59),
                new GlobalCurrency("EOS", 3.71),
                new GlobalCurrency("BCH", 160),
                new GlobalCurrency("ETC", 4.37),
                new GlobalCurrency("XMR", 51)
            };

            Cards = new ObservableCollection<VideoCard>()
            {
                new VideoCard("Asus PCI-Ex GeForce GTX 1050 Ti", 128, 5, Currencies, myFunc),
                new VideoCard("MSI PCI-Ex GeForce GTX 1660 Ti", 192, 6, Currencies, myFunc),
                new VideoCard("MSI PCI-Ex GeForce RTX 2070", 265, 6, Currencies, myFunc),
                new VideoCard("Gigabyte PCI-Ex GeForce GT 1030", 64, 5, Currencies, myFunc),
            };
        }

        void myFunc(VideoCard card)
        {
            try
            {
                Cards.Remove(card);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //COMMANDS
        FarmCommand addCurrency;
        public FarmCommand AddCurrency
        {
            get
            {
                return addCurrency ?? (addCurrency = new FarmCommand((curr) =>
                {
                    Currencies.Add(new Currency("New "));
                }));
            }
        }
    }

}
