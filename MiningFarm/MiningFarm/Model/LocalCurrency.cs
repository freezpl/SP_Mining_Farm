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
        Action<LocalCurrency, bool> onOff;

        public LocalCurrency(Guid id, string title, 
            Action<LocalCurrency, bool> onOff) : base(title)
        {
            Id = id;
            this.onOff = onOff;
        }

        FarmCommand startComm;
        public FarmCommand StartComm
        {
            get
            {
                return startComm ?? (startComm = new FarmCommand((curr) => {
                    if (Convert.ToBoolean(curr))
                        onOff(this, true);
                    else
                        onOff(this, false);
                }));
            }
        }
    }
}
