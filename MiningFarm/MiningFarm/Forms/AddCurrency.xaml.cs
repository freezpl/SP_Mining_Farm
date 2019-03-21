using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiningFarm.Forms
{
    /// <summary>
    /// Interaction logic for AddCurrency.xaml
    /// </summary>
    public partial class AddCurrency : MetroWindow, IDataErrorInfo, INotifyPropertyChanged
    {
        public string CurrTitle { get; set; }
        public string Kurs { get; set; }

        bool titleAvi;
        bool kursAvi;

        bool isAvialable;
        public bool IsAvialable
        {
            get
            {
                return isAvialable;
            }
            set
            {
                isAvialable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAvialable)));
            }
        }

        public AddCurrency()
        {
            InitializeComponent();
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case nameof(CurrTitle):
                        if (CurrTitle.Length < 1)
                        {
                            error = "Currency title to short!";
                            titleAvi = false;
                        }
                        else
                            titleAvi = true;
                        break;
                    case nameof(Kurs):
                        if (Kurs.Length == 0)
                        {
                            error = "No bus!";
                            kursAvi = false;
                            break;
                        }
                        Regex regex = new Regex(@"^[0-9]+(\,[0-9]+)?$");
                        Match match = regex.Match(Kurs);
                        if (!match.Success)
                        {
                            error = "Bus width must be a number!";
                            kursAvi = false;
                        }
                        else
                            kursAvi = true;
                        break;
                }
                AccessTrigger();
                return error;
            }
        }

        void AccessTrigger()
        {
            if (titleAvi && kursAvi)
                IsAvialable = true;
            else
                IsAvialable = false;
        }

        public string Error => throw new NotImplementedException();
    }
}
