namespace _538_Rule_Riddler_Nation_Solver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Commander
    {
        public Commander(string[] fields)
        {
            this.Soldiers = Array.ConvertAll(fields.Take(10).ToArray(), int.Parse).ToList();
            this.Comment = fields[10];
        }

        public Commander(int[] castles, string comment)
        {
            this.Soldiers = castles.ToList();
            this.Comment = comment;
        }

        public string Comment { get; set; }

        public int Losses { get; set; }

        public List<int> Soldiers { get; set; }

        public int Ties { get; set; }

        public int Wins { get; set; }

        public void Compare(Commander enemy)
        {
            double playerTotal = 0;
            double enemyTotal = 0;
            for (var i = 0; i < 10; i++)
            {
                var castlePlayer = this.Soldiers[i];
                var castleEnemy = enemy.Soldiers[i];
                var castleBattle = castlePlayer.CompareTo(castleEnemy);

                switch (castleBattle)
                {
                    // Instance = Player
                    // value = Enemy
                    case -1:

                        // Instance less than value
                        // Player Loss
                        enemyTotal += i + 1;
                        break;
                    case 0:

                        // Instance equal to value
                        // Tie
                        enemyTotal += (double)(i + 1) / 2;
                        playerTotal += (double)(i + 1) / 2;
                        break;
                    case 1:

                        // Instance greater than value
                        // Player Win
                        playerTotal += i + 1;
                        break;
                }
            }

            var winLossTie = playerTotal.CompareTo(enemyTotal);
            switch (winLossTie)
            {
                case -1:
                    enemy.Wins++;
                    this.Losses++;
                    break;
                case 0:
                    enemy.Ties++;
                    this.Ties++;
                    break;
                case 1:
                    enemy.Losses++;
                    this.Wins++;
                    break;
            }
        }
    }
}