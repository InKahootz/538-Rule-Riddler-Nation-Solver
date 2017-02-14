namespace _538_Rule_Riddler_Nation_Solver
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualBasic.FileIO;

    internal class Model
    {
        public Model()
        {
            this.Solutions = new List<Commander>();
            this.ParsePlans();
        }

        public Commander Player { get; set; }

        public List<Commander> Solutions { get; set; }

        public List<Commander> TopFive { get; set; }

        public void AddCommander(Commander player)
        {
            this.Player = player;
            this.Solutions.Add(player);
        }

        public void Fight()
        {
            for (var i = 0; i < this.Solutions.Count; i++)
            {
                for (var j = i + 1; j < this.Solutions.Count; j++)
                {
                    var player1 = this.Solutions[i];
                    var player2 = this.Solutions[j];
                    player1.Compare(player2);
                }
            }

            this.TopFive =
                this.Solutions.OrderByDescending(c => c.Wins)
                    .ThenBy(c => c.Losses)
                    .ThenBy(c => c.Ties)
                    .Take(5)
                    .ToList();
        }

        private void ParsePlans()
        {
            using (var sr = new StreamReader("castle-solutions.csv"))
            {
                using (var parser = new TextFieldParser(sr))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    // Read off the headers
                    parser.ReadFields();
                    while (!parser.EndOfData)
                    {
                        var fields = parser.ReadFields();
                        this.Solutions.Add(new Commander(fields));
                    }
                }
            }
        }
    }
}