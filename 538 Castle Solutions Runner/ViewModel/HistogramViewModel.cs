using System;
using System.Collections.Generic;
using System.Linq;

namespace _538_Rule_Riddler_Nation_Solver
{
    public class HistogramViewModel
    {
  
        private Model battlePlans;

        // XAML Testing
        public HistogramViewModel()
        {
            Title = $"Test Castle";
            battlePlans = new Model();

            Random rnd = new Random();
            ChartValues = Enumerable.Range(1, 50).ToDictionary(i => i, i => rnd.Next(1,101));

        }

        public HistogramViewModel(int castle, Model m)
        {
            Title = $"Castle {castle}";
            battlePlans = m;

            ChartValues = Enumerable.Range(1, 50).ToDictionary(i => i, i => 0);

            foreach(var commander in battlePlans.Solutions)
            {
                var castleTenSoldiers = commander.Soldiers[castle - 1];

                //foreach(var soldierAmount in commander.Soldiers)
                //{
                if (ChartValues.ContainsKey(castleTenSoldiers))
                {
                    ChartValues[castleTenSoldiers]++;
                }
                //}
            }
        }

        public string Title { get; set; }

        public Dictionary<int,int> ChartValues { get; set; }
         
    }
}
