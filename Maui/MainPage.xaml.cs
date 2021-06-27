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

            var random = new Random();

            var websites = new List<string> {
                "https://youtube.com",
                "https://github.com",
                "https://google.com",
                "https://saxion.nl",
                "https://microsoft.com",
                "https://netflix.com",
                "https://camiel.pw"
            };

            int random_i = random.Next(websites.Count);

            DataEntrys.Add(new DataEntry
            {
                Address = websites[random_i],
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

        bool checking_task;

        public Command Run => new(() =>
        {
            if (!checking_task)
            {
                checking_task = true;
                foreach (DataEntry entry in DataEntrys)
                {
                    Task.Run(() =>
                    {
                        Console.WriteLine($"Checking {entry.Address}");

                        using (HttpClient client = new HttpClient())
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                entry.ActivityRunning = true;
                            });
                            try
                            {
                                client.BaseAddress = new Uri(@entry.Address);
                            }
                            catch (UriFormatException)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    entry.ResponseCode = "Invalid URL!";
                                    entry.ResponseColor = Color.Red;
                                });
                                return;
                            }
                            client.Timeout = TimeSpan.FromSeconds(10);

                            var stopWatch = Stopwatch.StartNew();
                            try
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    entry.ActivityVisible = true;
                                });

                                HttpResponseMessage response = client.GetAsync($"{entry.Address}:{entry.Port}").Result;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    entry.ResponseTime = stopWatch.ElapsedMilliseconds.ToString() + "ms";
                                    entry.ResponseCode = response.StatusCode.ToString();
                                });

                                if (response.IsSuccessStatusCode)
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        entry.ResponseColor = Color.Green;
                                        Console.WriteLine($"{entry.Address} : {entry.Port} gelukt");
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        entry.ResponseColor = Color.DarkRed;
                                        Console.WriteLine($"{entry.Address} : {entry.Port} mislukt");
                                    });
                                }
                            }
                            catch (Exception)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    entry.ResponseCode = "Invalid URL!";
                                    entry.ResponseColor = Color.Red;
                                });
                                return;
                            }
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                entry.ActivityRunning = false;
                                entry.ActivityVisible = false;
                            });
                        }
                    });
                }

                checking_task = false;
            }
        });


    }
}
