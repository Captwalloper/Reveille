using Reveille.Utility;
using System.Threading.Tasks;
using System.Windows.Input;
using Reveille.Utility.Cortana;
using Reveille.View;

namespace Reveille.ViewModel
{
    public class MainViewModel : AppViewModel<Reveille.View.MainPage>
    {
        private string rawText = "";
        public string RawText
        {
            get { return rawText; }
            set
            {
                if (value != rawText) {
                    rawText = Sanitize(value);
                    OnPropertyChanged("RawText");
                }
            }
        }

        private string commandName = "";
        public string CommandName
        {
            get { return commandName; }
            set
            {
                if (value != commandName) {
                    commandName = Sanitize(value);
                    OnPropertyChanged("CommandName");
                }
            }
        }

        private string commandArg = "";
        public string CommandArg
        {
            get { return commandArg; }
            set
            {
                if (value != commandArg) {
                    commandArg = Sanitize(value);
                    OnPropertyChanged("CommandArg");
                }
            }
        }

        private string commandMode = "";
        public string CommandMode
        {
            get { return commandMode; }
            set
            {
                if (value != commandMode) {
                    commandMode = Sanitize(value);
                    OnPropertyChanged("CommandMode");
                }
            }
        }

        //Commands

        private ICommand _launchCommand;
        public ICommand LaunchCommand
        {
            get
            {
                return _launchCommand ?? ( _launchCommand = new Command( Unseal_The_Hushed_Casket ) );
            }
            protected set
            {
                _launchCommand = value;
            }
        }


        public MainViewModel(MainPage page)
        {
            ViewPage = page;
        }


        public override async Task RespondToVoice(CortanaCommand command)
        {
            await command.Perform();
            // update UI
            RawText = command.RawText;
            CommandMode = command.Mode;
            CommandName = command.Name;
            CommandArg = command.Argument;
        }

        private static string Sanitize(string inputString)
        {
            return inputString.Trim();
        }

        private async Task Launch(string filename)
        {
            await FileHelper.Run(filename);
        }

        /// <summary>
        /// Hook for tutorial. Currently demo for Cole Protocol text command, sandwiched by cutscenes. Satisfies programmer's need for Halo references. 
        /// </summary>
        private async void Unseal_The_Hushed_Casket()
        {
            // Yes, the timings are terrible hacks... but this was only meant to be a neat hook.
            const string cutscene1 = "Unseal_The_Hushed_Casket_p1.mp4";
            const int cutscene1_ms = 28000;
            await FileHelper.PlayFor(cutscene1, cutscene1_ms);

            /*Run the Demo*/
            await RunDemo();

            const string cutscene2 = "Unseal_The_Hushed_Casket_p2.mp4";
            const int cutscene2_ms = 28000;
            await FileHelper.PlayFor(cutscene2, cutscene2_ms);
        }

        private async Task RunDemo()
        {
            var execute_command = new ExecuteCortanaCommand("Cole Protocol");
            await Cortana.RunAsTextCommand(execute_command);
            await Task.Delay(10000);
        }


    }
}
