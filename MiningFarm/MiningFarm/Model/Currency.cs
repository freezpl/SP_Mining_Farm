using MiningFarm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MiningFarm.Model
{
    public class Currency : INotifyPropertyChanged
    {
        public string Title { get; set; }

        double val;
        public double Val
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Val"));
            }
        }
        
        double diff;

        public Currency(string title, double diff = 0)
        {
            Title = title;
            Val = 0;
            this.diff = diff;
        }

        //calculating
        // Power / difficulty / 100000000

        public event PropertyChangedEventHandler PropertyChanged;

        FarmCommand start;
        public FarmCommand Start
        {
            get
            {
                return start ?? (start = new FarmCommand((curr) => {
                    MessageBox.Show(curr.ToString());
                }));
            }
        }
    }
}
