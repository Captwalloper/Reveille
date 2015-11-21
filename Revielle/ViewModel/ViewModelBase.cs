using System.ComponentModel;

namespace Reveille.ViewModel
{
    /// <summary>
    /// Extremely generic ViewModelBase; for copy-pasting into almost any MVVM project
    /// </summary>
    public class ViewModelBase
    {

        /*Events*/

        public event PropertyChangedEventHandler PropertyChanged;

        /*Methods*/

        /// Fires PropertyChangedEventHandler, for bindables
        protected virtual void OnPropertyChanged(string name)
        {
            var ev = PropertyChanged;
            if (ev != null)
            {
                ev(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
