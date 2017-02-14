namespace _538_Rule_Riddler_Nation_Solver
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel vm;

        public MainWindow()
        {
            this.InitializeComponent();

            this.DataContext = this.vm = new ViewModel();
        }
    }
}
