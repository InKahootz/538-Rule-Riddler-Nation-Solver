using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace _538_Rule_Riddler_Nation_Solver
{
    public class MultiHistogramViewModel
    {
        public MultiHistogramViewModel()
        {
            //HistogramVMs = new List<HistogramViewModel>();
            Model m = new Model();
            for (int i = 1; i <= 10; i++)
            {
                HistogramVMs.Add(new HistogramViewModel(i, m));
            }
        }
        public ObservableCollection<HistogramViewModel> HistogramVMs { get; set; } = new ObservableCollection<HistogramViewModel>();
    }
}

