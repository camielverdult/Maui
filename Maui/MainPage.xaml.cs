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
        }

        void NewSiteEntry(Object sender,
                           EventArgs e)
        {

            var NewGrid = new Grid
            {
                WidthRequest = 400,
                HeightRequest = 100
            };
            // NewGrid.HorizontalAlignment = HorizontalAlignment.Left;
            // NewGrid.VerticalAlignment = VerticalAlignment.Top;

            ColumnDefinition AddressColumn = new();
            ColumnDefinition ResponseTimeColumn = new();
            ColumnDefinition ResponseCodeColumn = new();
            ColumnDefinition ActivityColumn = new();
            NewGrid.ColumnDefinitions.Add(AddressColumn);
            NewGrid.ColumnDefinitions.Add(ResponseTimeColumn);
            NewGrid.ColumnDefinitions.Add(ResponseCodeColumn);
            NewGrid.ColumnDefinitions.Add(ActivityColumn);

            RowDefinition Row = new();
            NewGrid.RowDefinitions.Add(Row);

            Entry SiteAdress = new();
            SiteAdress.Placeholder = "https://github.com";
            SiteAdress.HeightRequest = NewGrid.Height - 10;
            SiteAdress.WidthRequest = NewGrid.Width * 3 / 5;
            Grid.SetRow(SiteAdress, 1);
            Grid.SetColumn(SiteAdress, 1);
            NewGrid.Children.Add(SiteAdress);


            Label ReturnCode = new();
            ReturnCode.Text = "-";
            ReturnCode.TextColor = Color.DarkGray;
            Grid.SetRow(SiteAdress, 1);
            Grid.SetColumn(SiteAdress, 2);
            NewGrid.Children.Add(ReturnCode);


            Label ResponseTime = new();
            ResponseTime.Text = "-";
            ResponseTime.TextColor = Color.DarkGray;
            Grid.SetRow(SiteAdress, 1);
            Grid.SetColumn(SiteAdress, 3);
            NewGrid.Children.Add(ResponseTime);

            ActivityIndicator LoadingActivity = new();
            LoadingActivity.IsRunning = false;
            LoadingActivity.Color = Color.DarkGray;
            Grid.SetRow(SiteAdress, 1);
            Grid.SetColumn(SiteAdress, 4);
            NewGrid.Children.Add(ResponseTime);

            //SiteCollection.C.Add(NewGrid);
        }


        StackLayout MakeNewEntry()
        {
            StackLayout stack = new();

            return stack;
        }
    }
}
