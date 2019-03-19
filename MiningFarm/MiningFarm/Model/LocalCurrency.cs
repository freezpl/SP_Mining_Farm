using MiningFarm.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiningFarm.Model
{
    public class LocalCurrency : Currency
    {
        Action<LocalCurrency> on;

        public LocalCurrency(Guid id, string title, Action<LocalCurrency> on) : base(title)
        {
            Id = id;
            this.on = on;
        }

        FarmCommand startComm;
        public FarmCommand StartComm
        {
            get
            {
                return startComm ?? (startComm = new FarmCommand((curr) => {
                    if(Convert.ToBoolean(curr))
                        on(this);
                    else
                    {

                    }
                }));
            }
        }
    }
}
