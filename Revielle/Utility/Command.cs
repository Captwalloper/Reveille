using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reveille.Utility
{
    class Command : ICommand
    {
        #region Properties
        public Action Act { get; set; }
        #endregion Properties

        #region Events
        /// <summary> Occurs when the target of the Command should reevaluate whether or not the Command can be executed. </summary>
        public event EventHandler CanExecuteChanged;
        #endregion Events

        #region Constructors
        public Command(Action act)
        {
            Act = act;
        }

        //public Command(Action act, Func<bool> func)
        //{

        //}

        //public Command(Action<object> act, Func<object, bool> fun)
        //{

        //}
        #endregion Constructors

        /// <summary> Returns a bool indicating if the Command can be exectued with the given parameter </summary>
        /// <param name="obj"></param>
        public bool CanExecute(object obj)
        {
            return true;
        }

        /// <summary> Send a ICommand.CanExecuteChanged </summary>
        public void ChangeCanExecute()
        {
            object sender = this;
            EventArgs eventArgs = null;
            CanExecuteChanged(sender, eventArgs);
        }

        /// <summary> Invokes the execute Action </summary>
        public void Execute(object obj)
        {
            Act();
        }
    }
}
