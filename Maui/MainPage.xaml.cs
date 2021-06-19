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
        public int Port { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseTime { get; set; }
        public int Progress { get; set; }
        public int ID { get; set; }

        public void Print()
        {
            Console.WriteLine("DataEntry");
            Console.WriteLine("{");
            Console.WriteLine("Address: {0},", Address);
            Console.WriteLine("Port: {0},", Port);
            Console.WriteLine("ResponseCode: {0},", ResponseCode);
            Console.WriteLine("ResponseTime: {0},", ResponseTime);
            Console.WriteLine("Progress: {0},", Progress);
            Console.WriteLine("ID: {0},", ID);
            Console.WriteLine("};");
        }

        public Command RemoveEntry => new Command(() =>
        {

        });

    }


    public partial class MainPage : ContentPage
    {

        public ObservableCollection<DataEntry> DataEntrys { get; set; } = new ObservableCollection<DataEntry>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command AddEntry => new Command(() =>
        {
            Console.WriteLine("Adding new DataEntry!");

            // Update IDs (might have changed because of removal of entry
            for (int i = 0; i < DataEntrys.Count; i++)
            {
                DataEntrys[i].ID = i;
                
            }

            DataEntrys.Add(new DataEntry
            {
                Address = "https://github.com",
                Port = 443,
                ResponseCode = "-",
                ResponseTime = "-",
                Progress = 0,
                ID = DataEntrys.Count
            });
        });

        public Command RemoveEntry => new Command(() =>
        {
            Console.WriteLine("ayo PA!");
        });
    }
}
