using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public Color ResponseColor { get; set; }
        public string ResponseTime { get; set; }
        public int Progress { get; set; }
        public int ID { get; set; }
        public bool ActivityRunning { get; set; }
        public bool ActivityVisible { get; set; }

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
    }


    public partial class MainPage : ContentPage
    {

        public ObservableCollection<DataEntry> DataEntrys { get; set; } = new ObservableCollection<DataEntry>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command AddEntry => new(() =>
        {
            Console.WriteLine("Adding new DataEntry!");

            // Update IDs (might have changed because of removal of entry
            for (int i = 0; i < DataEntrys.Count; i++)
            {
                DataEntrys[i].ID = i;
                
            }

            DataEntrys.Add(new DataEntry
            {
                Address = "https://youtube.com",
                Port = 443,
                ResponseCode = "-",
                ResponseTime = "-",
                Progress = 0,
                ID = DataEntrys.Count,
                ActivityRunning = false,
                ActivityVisible = false,
                ResponseColor = Color.WhiteSmoke
            }); ;
        });

        async Task CheckEntryAsync(DataEntry entry)
        {
            Console.WriteLine($"Checking {entry.Address}");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(@entry.Address);

                // You don't need await here
                var stopWatch = Stopwatch.StartNew();
                HttpResponseMessage response = await client.GetAsync($"{entry.Address}:{entry.Port}");

                entry.ResponseTime = stopWatch.ElapsedMilliseconds.ToString();
                entry.ResponseCode = response.StatusCode.ToString();

                if (response.IsSuccessStatusCode)
                {
                    entry.ResponseColor = Color.Green;
                    Console.WriteLine($"{entry.Address} : {entry.Port} gelukt");
                }
                else
                {
                    entry.ResponseColor = Color.DarkRed;
                    Console.WriteLine($"{entry.Address} : {entry.Port} mislukt");
                }
            }
        }
            
   

        public Command Run => new(async () =>
        {
            Console.WriteLine("Checking things...");
            foreach (DataEntry entry in DataEntrys)
            {
                await CheckEntryAsync(entry);
            }
        });


    }
}
