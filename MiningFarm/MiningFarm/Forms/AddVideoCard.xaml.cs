using MahApps.Metro.Controls;
using MiningFarm.Model;
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
    /// Interaction logic for AddVideoCard.xaml
    /// </summary>
    public partial class AddVideoCard : MetroWindow, IDataErrorInfo, INotifyPropertyChanged
    {
        public string CardTitle { get; set; }
        public string Bus { get; set; }
        public string DDR { get; set; }

        bool titleAvi;
        bool busAvi;
        bool ddrAvi;

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

        public AddVideoCard()
        {
            InitializeComponent();
        }


        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CardTitle);
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
                    case nameof(CardTitle):
                        if (CardTitle.Length < 1)
                        {
                            error = "Caard Model is to short!";
                            titleAvi = false;
                        }
                        else
                            titleAvi = true;
                        break;
                    case nameof(Bus):
                        if(Bus.Length == 0)
                        {
                            error = "No bus!";
                            busAvi = false;
                            break;
                        }
                        Regex regex = new Regex(@"^[0-9]*$");
                        Match match = regex.Match(Bus);
                        if (!match.Success)
                        {
                            error = "Bus width must be a number!";
                            busAvi = false;
                        }
                        else
                            busAvi = true;
                        break;
                    case nameof(DDR):
                        if (DDR.Length == 0)
                        {
                            error = "No ddr!";
                            ddrAvi = false;
                            break;
                        }
                        Regex regex2 = new Regex(@"^[0-9]*$");
                        Match match2 = regex2.Match(DDR);
                        if (!match2.Success)
                        {
                            error = "Bus width must be a number!";
                            ddrAvi = false;
                        }
                        else
                            ddrAvi = true;
                        break;
                }
                AccessTrigger();
                return error;
            }
        }

        void AccessTrigger()
        {
            if (titleAvi && busAvi && ddrAvi)
                IsAvialable = true;
            else
                IsAvialable = false;
        }
        
        public string Error => throw new NotImplementedException();
    }
}
