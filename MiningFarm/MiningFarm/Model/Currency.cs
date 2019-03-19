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
        public Guid Id { get; set; }

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

        public Currency(string title)
        {
            Title = title;
            Val = 0;
        }
        //calculating
        // Power / difficulty / 100000000

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
