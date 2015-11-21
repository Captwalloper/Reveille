using Reveille.Utility.Cortana;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Reveille.ViewModel
{
    /// <summary>
    /// Application specific ViewModel base class. 
    /// </summary>
    /// <typeparam name="PG"></typeparam>
    public class AppViewModel<PG> : ViewModelBase, INotifyPropertyChanged where PG : Page
    {

        /*Properties*/

        /// Relevant ViewPage for this ViewModel
        /// Allows requests to be forwarded to view elements
        public PG ViewPage
        {
            get;
            set;
        }

        /*Methods*/

        /// <summary>
        /// Default app behavior for Cortana commands
        /// </summary>
        public virtual async Task RespondToVoice(CortanaCommand command)
        {
            string name = command.Name;
            string argument = command.Argument;
            switch(name)
            {
                case CortanaCommand.Execute:
                    break;

                case CortanaCommand.Notepad:
                    break;

                // ...
            }
        }

    }
}
