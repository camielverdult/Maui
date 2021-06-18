using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maui
{
    public class DataEntry
    {
        public string Address { get; set; }
        public string Port { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseTime { get; set; }
        public string Progress { get; set; }
    }


    public partial class MainPage : ContentPage
    {

        public ObservableCollection<DataEntry> DataEntrys { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void NewSiteEntry(Object sender,
                           EventArgs e)
        {

        }


        StackLayout MakeNewEntry()
        {
            StackLayout stack = new();

            return stack;
        }
    }
}
