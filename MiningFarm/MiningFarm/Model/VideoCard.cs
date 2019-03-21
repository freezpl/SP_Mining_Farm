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
        Guid id;
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
        Action<VideoCard, LocalCurrency, bool> startAbortMining;

        public VideoCard(string title, int bus, int ddr, 
            ObservableCollection<GlobalCurrency> currencies, Action<VideoCard> remove,
            Action<VideoCard, LocalCurrency, bool> startAbortMining)
        {
            id = Guid.NewGuid();
            Title = title;

            power = bus * ddr;
            ActiveTasks = 0;
            
            Currencies = new ObservableCollection<LocalCurrency>();
            foreach (var curr in currencies)
            {
                Currencies.Add(new LocalCurrency(curr.Id, curr.Title, AddRemTask));
            }

            removeMe += remove;
            this.startAbortMining = startAbortMining;
        }

        void AddRemTask(LocalCurrency lc, bool flag)
        {
            startAbortMining(this, lc, flag);
        }

        public void AddCurr(GlobalCurrency gc)
        {
            Currencies.Add(new LocalCurrency(gc.Id, gc.Title, AddRemTask));
        }

        public void RemoveCurr(Guid id)
        {
            LocalCurrency lc = Currencies.FirstOrDefault(c => c.Id == id);
            if (lc != null)
            {
                if (lc.IsAct)
                    ActiveTasks--;
                Currencies.Remove(lc);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //COMMANDS
        FarmCommand remove;
        public FarmCommand Remove
        {
            get
            {
                return remove ?? (remove = new FarmCommand((curr) => {
                    
                    if (ActiveTasks > 0)
                    {
                        MessageBox.Show("This card has "+ ActiveTasks +" active tasks!\n Remove active tasks please.",
                            "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    MessageBoxResult res = MessageBox.Show("Are you sure? Remove this card?",
                            "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if(res == MessageBoxResult.OK)
                        removeMe(this);
                }));
            }
        }
        
    }
}
