namespace _538_Rule_Riddler_Nation_Solver
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualBasic.FileIO;

    public class Model
    {
        private List<Commander> pristineList;

        public Model()
        {
            this.Solutions = new List<Commander>();
            this.ParsePlans();
            PreFight();
        }

        public Commander Player { get; set; }

        public List<Commander> Solutions { get; set; }

        public List<Commander> TopFive { get; set; }

        public List<Commander> SortedCommanders { get; set; }

        public void AddCommander(Commander player)
        {
            this.Player = player;
            //this.Solutions.Add(player);
        }

        private void PreFight()
        {
            foreach(var com in Solutions)
            {
                com.ResetRecord();
            }
            for (var i = 0; i < this.Solutions.Count; i++)
            {
                for (var j = i + 1; j < this.Solutions.Count; j++)
                {
                    var player1 = this.Solutions[i];
                    var player2 = this.Solutions[j];
                    player1.Compare(player2);
                }
            }

            pristineList = new List<Commander>(Solutions);
        }

        public void Fight()
        {
            if (pristineList != null)
            {
                Solutions = new List<Commander>(pristineList).OrderByDescending(c => c.Wins).Take(pristineList.Count / 2).ToList();
                PreFight();
            }

            for (var i = 0; i < this.Solutions.Count; i++)
            {
                    var player1 = this.Solutions[i];
                    Player.Compare(player1);
            }
        }

        public void GetTopFive()
        {
            this.SortedCommanders =
                this.Solutions.OrderByDescending(c => c.Wins)
                    .ThenBy(c => c.Losses)
                    .ThenBy(c => c.Ties)
                    .ToList();
            this.TopFive = this.SortedCommanders.Take(10).ToList();
        }

        private void ParsePlans()
        {
            using (var sr = new StringReader(Properties.Resources.castle_solutions))
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