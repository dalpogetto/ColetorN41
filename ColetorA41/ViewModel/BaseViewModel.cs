using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.ViewModel
{
    public partial class BaseViewModel:ObservableObject
    {
        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        bool isError = false;

        [ObservableProperty]
        bool isCalculated = false;

        [ObservableProperty]
        bool title = false;

    }
}
