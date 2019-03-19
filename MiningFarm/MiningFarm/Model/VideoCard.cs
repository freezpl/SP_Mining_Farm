using MiningFarm.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiningFarm.Model
{
    public class VideoCard : INotifyPropertyChanged
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

        private int activeTasks;

        public int ActiveTasks
        {
            get { return activeTasks; }
            set
            {
                activeTasks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveTasks)));
            }
        }


        public ObservableCollection<LocalCurrency> Currencies { get; set; }

        Action<VideoCard> removeMe;
       
        public VideoCard(string title, int bus, int ddr, 
            ObservableCollection<Currency> currencies, Action<VideoCard> remove)
        {
            Title = title;

            power = bus * ddr;
            ActiveTasks = 0;
    
            Currencies = new ObservableCollection<LocalCurrency>();
            foreach (var curr in currencies)
            {
                Currencies.Add(new LocalCurrency(curr.Id, curr.Title, AddTask));
            }
            removeMe += remove;
        }

        void AddTask(LocalCurrency lc)
        {
            MessageBox.Show(lc.Title);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        //COMMANDS
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
