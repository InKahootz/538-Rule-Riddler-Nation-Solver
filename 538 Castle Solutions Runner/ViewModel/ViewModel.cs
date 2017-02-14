namespace _538_Rule_Riddler_Nation_Solver
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    internal class ViewModel : PropertiesChangedBase
    {
        private readonly Model model;

        private Commander player;

        private ICommand runCommand;

        public ViewModel()
        {
            this.TopFive = new ObservableCollection<Commander>();
            this.model = new Model();
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

        public ICommand RunCommand => this.runCommand ?? new DelegateCommand(o => this.Run());

        public ObservableCollection<Commander> TopFive { get; set; }

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
            this.TopFive.Clear();
            foreach (var comm in this.model.TopFive)
            {
                this.TopFive.Add(comm);
            }
            this.Player = this.model.Player;
        }
    }
}