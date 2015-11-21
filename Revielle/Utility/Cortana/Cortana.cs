using Reveille.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Media.SpeechRecognition;
using Windows.Storage;

namespace Reveille.Utility.Cortana
{
    public static class Cortana
    {
        /// <summary>
        /// Note: this class and the vcd file are heavily coupled.
        /// </summary>
        private const string vcdFilename = "CortanaCommands.xml";

        public static async void Setup()
        {
            await InstallVoiceCommands();
            await InstallPhrases();
        }

        /// <summary>
        /// Converts VoiceCommandActivatedEvent into specific CortanaCommand. 2 purposes:
        /// 1) Parse the "argument" of the command (ex. filename "My Cool Script", notepad note "feed the cat", etc.) into usable format.
        /// 2) Separate the details of "capturing" a voice command (here) from the command's business logic (in it's subclass of CortanaCommand) 
        /// </summary>
        public static CortanaCommand ProcessCommand(VoiceCommandActivatedEventArgs commandArgs)
        {
            SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;

            // Get the name of the voice command and the text spoken
            string voiceCommandName = speechRecognitionResult.RulePath[0];
            string textSpoken = speechRecognitionResult.Text;

            string argument = null;
            CortanaCommand processedCommand = null;
            switch (voiceCommandName)
            {
                case CortanaCommand.Execute:
                    argument = GetPhraseArg(speechRecognitionResult, "filename"); // filename
                    processedCommand = new ExecuteCortanaCommand(argument, commandArgs);
                    break;

                case CortanaCommand.YouTube:
                    const string youtube = "YouTube";
                    argument = StripOffCommand(youtube, textSpoken); // search text
                    processedCommand = new YoutubeCortanaCommand(argument, commandArgs);
                    break;

                case CortanaCommand.Notepad:
                    const string notepad = "Notepad";
                    argument = StripOffCommand(notepad, textSpoken); // text
                    processedCommand = new NotepadCortanaCommand(argument, commandArgs);
                    break;

                case CortanaCommand.ToggleListening:
                    processedCommand = new ToggleListeningCortanaCommand(null, commandArgs); // no argument needed
                    break;

                default:
                    Debug.WriteLine("Command Name Not Found:  " + voiceCommandName);
                    break;
            }
            return processedCommand;
        }

        /// <summary>
        /// Run the command in text mode (as opposed to voice mode). 
        /// </summary>
        public static async Task RunAsTextCommand(CortanaCommand command)
        {
            // Store argument in clipboard
            string rawInput = command.ToInputString();
            ClipboardHelper.CopyToClipboard(rawInput);
            // Run the closest thing to Cortana command line I have
            const string filename = "Cortanahk.ahk";
            await FileHelper.Run(filename);
        }

        /// <summary>
        /// Updates Cortana with new commands and phrases from a VCD file
        /// </summary>
        private static async Task InstallVoiceCommands()
        {
            StorageFile file = (StorageFile)(await FileHelper.GetFile(vcdFilename));
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(file);
        }

        private static async Task InstallPhrases()
        {
            await InstallFilenamePhrase();
        }

        private static async Task InstallFilenamePhrase()
        {
            // Get the names of the files of interest (right now, the autohotkey files in the resource folder)
            string[] filenames = await FileHelper.GetFiles(FileHelper.resourceFolderName, ".ahk");

            // colloquialize (so you don't have to say the underscore(s) and extension aloud)
            for (int i = 0; i < filenames.Length; i++)
            {
                string filename = filenames[i];
                filenames[i] = FileHelper.ColloquializeFilename(filename);
            }

            const string filenamePhraseListLabel = "filename"; // must match vcd file
            AugmentPhraseList(filenamePhraseListLabel, filenames);
        }

        /// <summary>
        /// Updates the VCD file with new prhases at runtime
        /// </summary>
        private static async void AugmentPhraseList(string phraseLabel, string[] newList)
        {
            try {
                VoiceCommandDefinition commandDefinitions;
                const string nameTag = "CortanaCommandSet_en-us"; // must match vcd file
                bool commandDefFound = VoiceCommandDefinitionManager.InstalledCommandDefinitions.TryGetValue(nameTag, out commandDefinitions);
                if (commandDefFound) {
                    await commandDefinitions.SetPhraseListAsync(phraseLabel, newList);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Updating Phrase list for VCDs: " + ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves the best match from phraseName's list of options (declared in VCD file)
        /// </summary>
        private static string GetPhraseArg(SpeechRecognitionResult speechRecognitionResult, string phraseName)
        {
            string phraseArg = speechRecognitionResult.SemanticInterpretation.Properties[phraseName][0];
            return phraseArg;
        }

        /// <summary>
        /// Removes the "Execute" from "Execute Notepad", for instance
        /// </summary>
        private static string StripOffCommand(string command, string textSpoken)
        {
            string[] words = textSpoken.Split(null); // split based on spaces
            string commandArg = "";
            foreach(string word in words)
            {
                if ( !word.Equals(command) ) {
                    commandArg += word + " ";
                }
            }
            commandArg = commandArg.TrimEnd(' '); // eliminate extra space
            return commandArg;
        }

    }
}
