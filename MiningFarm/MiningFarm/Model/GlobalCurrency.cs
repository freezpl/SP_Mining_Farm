using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningFarm.Model
{
    public class GlobalCurrency : Currency
    {
        double diff;

        public GlobalCurrency(string title, double diff = 0) : base(title)
        {
            this.diff = diff;
            Id = Guid.NewGuid();
        }
    }
}
