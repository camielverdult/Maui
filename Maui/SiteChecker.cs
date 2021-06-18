using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Maui
{
    public class SiteChecker
    {
        ObservableCollection<DataEntry> DataEntries { get; set; }


       
        public SiteChecker(ObservableCollection<DataEntry> Entries)
        {
            DataEntries = Entries;
        }

        static async Task<ObservableCollection<DataEntry>> CheckIfAvailable()
        {
            return new ObservableCollection<DataEntry>();
        }
    }
}
