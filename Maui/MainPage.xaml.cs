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
    public class DataEntry: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string _Address;

        public string Address {
            get {
                return this._Address;
            }
            set {
                if (this._Address != value)
                {
                    this._Address = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
                }
            }
        }

        private int _Port;

        public int Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                if (this._Port != value)
                {
                    this._Port = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Port)));
                }
            }
        }

        private string _ResponseCode;

        public string ResponseCode
        {
            get
            {
                return this._ResponseCode;
            }
            set
            {
                if (this._ResponseCode != value)
                {
                    this._ResponseCode = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResponseCode)));
                }
            }
        }

        private Color _ResponseColor;

        public Color ResponseColor
        {
            get
            {
                return this._ResponseColor;
            }
            set
            {
                if (this._ResponseColor != value)
                {
                    this._ResponseColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResponseColor)));
                }
            }
        }

        private string _ResponseTime;

        public string ResponseTime
        {
            get
            {
                return this._ResponseTime;
            }
            set
            {
                if (this._ResponseTime != value)
                {
                    this._ResponseTime = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResponseTime)));
                }
            }
        }

        private int _Progress;

        public int Progress
        {
            get
            {
                return this._Progress;
            }
            set
            {
                if (this._Progress != value)
                {
                    this._Progress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
                }
            }
        }

        private int _ID;

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if (this._ID != value)
                {
                    this._ID = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
                }
            }
        }

        private bool _ActivityRunning;

        public bool ActivityRunning
        {
            get
            {
                return this._ActivityRunning;
            }
            set
            {
                if (this._ActivityRunning != value)
                {
                    this._ActivityRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActivityRunning)));
                }
            }
        }

        private bool _ActivityVisible;

        public bool ActivityVisible
        {
            get
            {
                return this._ActivityVisible;
            }
            set
            {
                if (this._ActivityVisible != value)
                {
                    this._ActivityVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActivityVisible)));
                }
            }
        }

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

        public DataEntry()
        {

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
            });
        });

        public Command<DataEntry> DeleteEntry => new((DataEntry entry) =>
        {
            DataEntrys.Remove(entry);
        });

        async Task CheckEntryAsync(DataEntry entry)
        {
            Console.WriteLine($"Checking {entry.Address}");

            

            using (HttpClient client = new HttpClient())
            {
                entry.ActivityRunning = true;
                try
                {
                    client.BaseAddress = new Uri(@entry.Address);
                }
                catch (System.UriFormatException)
                {
                    entry.ResponseCode = "Invalid URL!";
                    entry.ResponseColor = Color.Red;
                    return;
                }
                client.Timeout = TimeSpan.FromSeconds(10);

                var stopWatch = Stopwatch.StartNew();
                try
                {
                    entry.ActivityVisible = true;
                    
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
                catch (Exception)
                {
                    entry.ResponseCode = "Invalid URL!";
                    entry.ResponseColor = Color.Red;
                    return;
                }

                entry.ActivityRunning = false;

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
