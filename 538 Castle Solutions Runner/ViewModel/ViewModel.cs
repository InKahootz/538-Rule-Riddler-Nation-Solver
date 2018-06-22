namespace _538_Rule_Riddler_Nation_Solver
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class ViewModel : PropertiesChangedBase
    {
        private readonly Model model;

        private Commander player;

        private ICommand runCommand;

        private ICommand runSimCommand;

        public ViewModel()
        {
            this.TopFive = new ObservableCollection<Commander>();
            this.AllCommanders = new ObservableCollection<Commander>();
            this.model = new Model();
            MultiHistogramVM = new MultiHistogramViewModel();
        }

        public int CastleEight { get; set; }

        public int CastleFive { get; set; }

        public int CastleFour { get; set; }

        public int CastleNine { get; set; }

        public int CastleOne { get; set; }

        public int CastleSeven { get; set; }

        public int CastleSix { get; set; }

        public int CastleTen { get; set; }

        public int CastleThree { get; set; }

        public int CastleTwo { get; set; }

        public string Comment { get; set; }

        public Commander BestWins { get; set; }

        public Commander Player
        {
            get
            {
                return this.player;
            }

            set
            {
                this.player = value;
                this.OnPropertyChanged();
            }
        }

        public MultiHistogramViewModel MultiHistogramVM { get; set; }

        public double SimProgress { get; set; }

        public ICommand RunCommand => this.runCommand ?? new DelegateCommand(o => this.Run());

        public ICommand RunSimCommand => this.runSimCommand ?? new DelegateCommand(o => this.RunSims());

        public ObservableCollection<Commander> TopFive { get; set; }

        public ObservableCollection<Commander> AllCommanders { get; set; }

        private void Run()
        {
            this.Player =
                new Commander(
                    new[]
                        {
                            this.CastleOne, this.CastleTwo, this.CastleThree, this.CastleFour, this.CastleFive,
                            this.CastleSix, this.CastleSeven, this.CastleEight, this.CastleNine, this.CastleTen
                        },
                    this.Comment);
            this.model.AddCommander(this.Player);
            this.model.Fight();
            this.model.GetTopFive();

            this.TopFive.Clear();
            foreach (var comm in this.model.TopFive)
            {
                this.TopFive.Add(comm);
            }

            this.AllCommanders.Clear();
            foreach (var comm in this.model.SortedCommanders)
            {
                this.AllCommanders.Add(comm);
            }

            this.Player = this.model.Player;
        }

        private CancellationTokenSource cts;

        private void RunSims()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += delegate { UpdateProgress(); };
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);  // 2/sec
            timer.Start();

            cts = new CancellationTokenSource();
            Task.Factory.StartNew(() => Worker(), cts.Token); 
        }
        long currentI = 0;

          private static long oneMax = 3;
          private static long twoMax = 5;
          private static long threeMax = 8;
          private static long fourMax = 15;
          private static long fiveMax = 22;
          private static long sixMax = 22;
          private static long sevenMax = 32;
          private static long eightMax = 35;
          private static long nineMax = 5;
          private static long tenMax = 5;

        private long iterations = 1;

        private void Worker()
        {
            BestWins = Player;
            var test = new Commander(new int[] { });
            iterations = oneMax * twoMax * threeMax * fourMax * fiveMax * sixMax * sevenMax * eightMax * nineMax * tenMax;
            for (int one = CastleOne; one < oneMax; one++)
            {

                //{
                for (int two = CastleTwo; two < twoMax; two++)
                {
                    for (int three = CastleThree; three < threeMax; three++)
                    {
                        for (int four = CastleFour; four < fourMax; four++)
                        {
                            for (int five = CastleFive; five < fiveMax; five++)
                            {
                                for (int six = CastleSix; six < sixMax; six++)
                                {
                                    for (int seven = CastleSeven; seven < sevenMax; seven++)
                                    {
                                        for (int eight = CastleEight; eight < eightMax; eight++)
                                        {
                                            for (int nine = CastleNine; nine < nineMax; nine++)
                                            {
                                                for (int ten = CastleTen; ten < tenMax; ten++)
                                                {
                                                    if (one + two + three + four + five + six + seven + eight + nine + ten != 100) continue;
                                                    currentI = tenMax * (nineMax * (eightMax * (sevenMax * (sixMax * (fiveMax * (fourMax * (threeMax * ((twoMax * one) + two) + three) + four) + five) + six) + seven) + eight) + nine) + ten;
                                                    
                                                    test.Soldiers = new[] { one, two, three, four, five, six, seven, eight, nine, ten };
                                                    test.ResetRecord();
                                                    this.model.AddCommander(test);
                                                    this.model.Fight();

                                                    if (model.Player.Wins >= BestWins.Wins)
                                                    {
                                                        BestWins = new Commander(test.Soldiers);
                                                        BestWins.Wins = test.Wins;
                                                        BestWins.Losses = test.Losses;
                                                        BestWins.Ties = test.Ties;
                                                        continue;
                                                    }
                                                    if (model.Player.Wins == BestWins.Wins && model.Player.Losses < BestWins.Losses)
                                                    {
                                                        BestWins = new Commander(test.Soldiers);
                                                        BestWins.Wins = test.Wins;
                                                        BestWins.Losses = test.Losses;
                                                        BestWins.Ties = test.Ties;
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateProgress()
        {
            SimProgress = (double)(currentI) / iterations;
            OnPropertyChanged("SimProgress");
            OnPropertyChanged("BestWins");
        }
    }
}