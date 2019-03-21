using MiningFarm.Forms;
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
        public ObservableCollection<GlobalCurrency> Currencies { get; set; }
        public ObservableCollection<VideoCard> Cards { get; set; }

        public GlobalCurrency ActiveCurr { get; set; }
        
        public Farm()
        {
            Currencies = new ObservableCollection<GlobalCurrency>()
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
                new VideoCard("Asus PCI-Ex GeForce GTX 1050 Ti", 128, 5, Currencies, RemoveCard,
                StartAbortMining),
                new VideoCard("MSI PCI-Ex GeForce GTX 1660 Ti", 192, 6, Currencies, RemoveCard,
                StartAbortMining),
                new VideoCard("MSI PCI-Ex GeForce RTX 2070", 265, 6, Currencies, RemoveCard,
                StartAbortMining),
                new VideoCard("Gigabyte PCI-Ex GeForce GT 1030", 64, 5, Currencies, RemoveCard,
                StartAbortMining),
            };
        }

        void RemoveCard(VideoCard card)
        {
            try
            {
                Cards.Remove(card);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void StartAbortMining(VideoCard card, LocalCurrency lc, bool flag)
        {
            GlobalCurrency activeCur = Currencies.FirstOrDefault(curr => curr.Id == lc.Id);
            if (activeCur == null)
                return;
            activeCur.Mining(card, lc, flag);
        }

        //COMMANDS

        FarmCommand addVideoCard;
        public FarmCommand AddVideoCard
        {
            get
            {
                return addVideoCard ?? (addVideoCard = new FarmCommand((o) =>
                {
                    AddVideoCard cardForm = new AddVideoCard();
                    cardForm.ShowDialog();
                    if(cardForm.DialogResult == true)
                    {
                        Cards.Add(new VideoCard(cardForm.CardTitle, Convert.ToInt32(cardForm.Bus), Convert.ToInt32(cardForm.DDR),
                            Currencies, RemoveCard, StartAbortMining));
                    }

                }));
            }
        }

        FarmCommand addCurr;
        public FarmCommand AddCurr
        {
            get
            {
                return addCurr ?? (addCurr = new FarmCommand((o) =>
                {
                    AddCurrency cardForm = new AddCurrency();
                    cardForm.ShowDialog();
                    
                    if (cardForm.DialogResult == true)
                    {
                        GlobalCurrency gc = new GlobalCurrency(cardForm.CurrTitle, Convert.ToDouble(cardForm.Kurs));
                        Currencies.Add(gc);
                        foreach(var card in Cards)
                            card.AddCurr(gc);
                    }
                }));
            }
        }

        FarmCommand removeCurrency;
        public FarmCommand RemoveCurrency
        {
            get
            {
                return removeCurrency ?? (removeCurrency = new FarmCommand((curr) =>
                {
                    if (ActiveCurr != null)
                    {
                        MessageBoxResult res;
                        if (ActiveCurr.ThreadCount > 0)
                            res = MessageBox.Show(ActiveCurr.ThreadCount + " videocards use this currency!\n Remove currency?", 
                                "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        else
                            res = MessageBox.Show("Remove currency?", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if(ActiveCurr.ThreadCount > 0)
                            ActiveCurr.AbortAllTasks();

                        foreach (var card in Cards)
                           card.RemoveCurr(ActiveCurr.Id);
                            
                        
                        Currencies.Remove(ActiveCurr);
                    }
                    else
                        MessageBox.Show("No currency selected!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }));
            }
        }
    }

}
