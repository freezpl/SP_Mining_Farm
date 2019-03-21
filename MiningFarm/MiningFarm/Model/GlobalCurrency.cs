using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MiningFarm.Model
{
    public class GlobalCurrency : Currency
    {
        public static object locker = new object();

        double diff;
        Dictionary<VideoCard, Thread> treads;

        public int ThreadCount
        {
            get
            {
                return treads.Count;
            }
        }

        public GlobalCurrency(string title, double diff = 0) : base(title)
        {
            this.diff = diff;
            Id = Guid.NewGuid();
            treads = new Dictionary<VideoCard, Thread>();
        }

        public void Mining(VideoCard card, LocalCurrency lc, bool flag)
        {
            if (flag)
            {
                if (treads.ContainsKey(card))
                    return;
                Thread th = new Thread(() => Work(card, lc));
                th.IsBackground = true;
                th.Start();
                treads.Add(card, th);
                card.ActiveTasks++;
            }
            else
            {
                if (!treads.ContainsKey(card))
                    return;
                Thread th = treads[card];
                treads.Remove(card);
                th.Abort();
                card.ActiveTasks--;
            }
        }

        void Work(VideoCard card, LocalCurrency lc)
        {
            while (true)
            {
                Thread.Sleep(1000);
                double v = card.Power/card.ActiveTasks/diff/100000000;
                lock (locker)
                {
                    Val += v;
                }
                lc.Val += v;
            }
        }

        public void AbortAllTasks()
        {
            foreach (KeyValuePair<VideoCard, Thread> item in treads)
                item.Value.Abort();
            treads.Clear();
        }
    }
}
