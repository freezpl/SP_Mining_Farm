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
    public class VideoCard
    {
        public string Title { get; set; }
        
        double power;
        public double Power
        {
            get
            {
                return power;
            }
        }

        public ObservableCollection<Currency> Currencies { get; set; }

        Action<VideoCard> removeMe;
       
        public VideoCard(string title, int bus, int ddr, 
            ObservableCollection<Currency> currencies, Action<VideoCard> remove)
        {
            Title = title;

            power = bus * ddr;
    
            Currencies = new ObservableCollection<Currency>();
            foreach (var curr in currencies)
            {
                Currencies.Add(new Currency(curr.Title));
            }
            removeMe += remove;
        }

        FarmCommand remove;
        public FarmCommand Remove
        {
            get
            {
                return remove ?? (remove = new FarmCommand((curr) => {
                    removeMe(this);
                }));
            }
        }
    }
}
