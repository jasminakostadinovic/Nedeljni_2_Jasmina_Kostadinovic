using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Healthcare_App.Interfaces
{
    interface ISaveCommand
    {
        void SaveExecute();
        bool CanSaveExecute();
        ICommand Save { get; }
    }
}
