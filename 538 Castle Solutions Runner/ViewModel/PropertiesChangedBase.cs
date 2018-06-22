using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _538_Rule_Riddler_Nation_Solver
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using _538_Rule_Riddler_Nation_Solver.Annotations;

    public class PropertiesChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
