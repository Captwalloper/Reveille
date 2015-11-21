using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Media.SpeechRecognition;

namespace Reveille.Utility.Cortana
{
    public abstract class CortanaCommand
    {
        public const string Execute = "execute";
        public const string Notepad = "notepad";
        public const string YouTube = "youtube";
        public const string ToggleListening = "toggle_listening";

        public string Name { get; set;  }
        public string Argument { get; set; }
        //
        public string RawText { get; private set; }
        public string Mode { get; private set; }

        protected CortanaCommand(string name, string argument, VoiceCommandActivatedEventArgs commandArgs=null)
        {
            Name = name;
            Argument = argument;

            OrganizeFeedback(commandArgs);
        }

        public abstract Task Perform();

        protected string ProvideLaunchFeedback(string filename)
        {
            return "Launched " + filename + " !!!";
        }

        private void OrganizeFeedback(VoiceCommandActivatedEventArgs commandArgs)
        {
            if (commandArgs != null) {
                SpeechRecognitionResult speech = commandArgs.Result;
                RawText = speech.Text;
                Mode = SemanticInterpretation("commandMode", speech);

                SpeechRecognitionConfidence confidence = speech.Confidence;
                double rawConfidence = speech.RawConfidence;
                TimeSpan duration = speech.PhraseDuration;
                DateTimeOffset start = speech.PhraseStartTime;
                SpeechRecognitionResultStatus status = speech.Status;
            }
            else {
                RawText = Mode = null;
            }
        }

        public string ToInputString()
        {
            string inputString = "cortana" + " " + Name + " " + Argument;
            return inputString;
        }

        /// <summary> Returns the semantic interpretation of a speech result. Returns null if there is no interpretation for that key. </summary>
        /// <param name="interpretationKey">The interpretation key.</param>
        /// <param name="speechRecognitionResult">The result to get an interpretation from.</param>
        private static string SemanticInterpretation(string interpretationKey, SpeechRecognitionResult speechRecognitionResult)
        {
            return speechRecognitionResult.SemanticInterpretation.Properties[interpretationKey].FirstOrDefault();
        }

    }

    public class ExecuteCortanaCommand : CortanaCommand
    {
        public ExecuteCortanaCommand(string argument, VoiceCommandActivatedEventArgs commandArgs = null) : base(Execute, argument, commandArgs) { /*Super constructor does everything*/ }

        public override async Task Perform()
        {
            string filename = Argument;
            string properFilename = FileHelper.ConvertToProperFilename(filename);
            await FileHelper.Run(properFilename);
        }
    }

    public class NotepadCortanaCommand : CortanaCommand
    {
        public NotepadCortanaCommand(string argument, VoiceCommandActivatedEventArgs commandArgs = null) : base(Notepad, argument, commandArgs) { /*Super constructor does everything*/ }

        public override async Task Perform()
        {
            string text = Argument;
            const string notepad = "Notepad";
            string properFilename = FileHelper.ConvertToProperFilename(notepad);
            ClipboardHelper.CopyToClipboard(text);
            await FileHelper.Run(properFilename);
        }
    }

    public class YoutubeCortanaCommand : CortanaCommand
    {
        public YoutubeCortanaCommand(string argument, VoiceCommandActivatedEventArgs commandArgs = null) : base(YouTube, argument, commandArgs) { /*Super constructor does everything*/ }

        public override async Task Perform()
        {
            string searchTarget = Argument;
            const string youtube = "YouTube";
            string properFilename = FileHelper.ConvertToProperFilename(youtube);
            ClipboardHelper.CopyToClipboard(searchTarget);
            await FileHelper.Run(properFilename);
        }
    }

    public class ToggleListeningCortanaCommand : CortanaCommand
    {
        public ToggleListeningCortanaCommand(string argument, VoiceCommandActivatedEventArgs commandArgs = null) : base(ToggleListening, argument, commandArgs) { /*Super constructor does everything*/ }

        public override async Task Perform()
        {
            string properFilename = "Cortana_Settings.ahk";
            await FileHelper.Run(properFilename);
        }
    }

}
