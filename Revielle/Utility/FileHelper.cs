using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using System.Diagnostics;

namespace Reveille.Utility
{
    public static class FileHelper
    {
        public const string resourceFolderName = "ResourceFiles";
        public const string resourcePathRelative = @"..\..\Model\ResourceFiles";

        public static async Task<bool> Run(string filename)
        {
            IStorageFile file = await GetFile(filename);

            LauncherOptions options = new LauncherOptions();
            //options.DisplayApplicationPicker = true;
            //options.FallbackUri = GetUri(cole_protocol);

            bool result = await Launcher.LaunchFileAsync(file, options);
            return result;
        }

        public static async Task CloseActive()
        {
            await Run("CloseActiveFile.ahk");
        }

        public static async Task MaximizeActive()
        {
            await Run("Maximize.ahk");
        }

        public static async Task PlayFor(string filename, int msToPlayFor)
        {
            await Run(filename);
            await MaximizeActive();
            await Task.Delay(msToPlayFor);
            await CloseActive();
        }

        public static async Task<IStorageFile> GetFile(string filename)
        {
            Uri uri = GetUri(filename);
            IStorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            return file;
        }

        public static async Task<StorageFolder> GetDirectory(string name)
        {
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder dir = await installedLocation.GetFolderAsync(name);
            return dir;
        }

        public static async Task<string[]> GetFiles(string folderName)
        {
            string[] filenames = null;
            try
            {
                StorageFolder dir = await GetDirectory(@"Model\" + folderName);
                var files = await dir.GetFilesAsync();
                IList<string> names = new List<string>();
                foreach(StorageFile file in files)
                {
                    string filename = file.Name;
                    names.Add(filename);
                }

                filenames = names.ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return filenames;
        }

        public static async Task<string[]> GetFiles(string folderName, string extension)
        {
            string[] filenames = await FileHelper.GetFiles(FileHelper.resourceFolderName);

            // filter out files not of the specified extension
            IList<string> filenamesOfSpecifiedExtension = new List<string>();
            foreach (string filename in filenames)
            {
                if (filename.Contains(extension))
                {
                    filenamesOfSpecifiedExtension.Add(filename);
                }
            }
            return filenamesOfSpecifiedExtension.ToArray();
        }

        public static string ConvertToProperFilename(string spokenFilename)
        {
            const string extension = ".ahk"; // assume autohotkey extension

            string[] words = spokenFilename.Split(null); // split based on spaces
            string properFilename = "";
            foreach (string word in words)
            {
                string temp = char.ToUpper(word[0]) + word.Substring(1); // Capitalize 1st letter
                properFilename += temp + "_";
            }
            properFilename = properFilename.TrimEnd('_'); // eliminate extra underscore
            properFilename += extension;
            return properFilename;
        }

        /// <summary>
        /// Formats the filename like you would say it aloud (ex. "cortana_settings.ahk" to "cortana settings") 
        /// </summary>
        public static string ColloquializeFilename(string filename)
        {
            string colloquialFilename = "";
            string[] words = filename.Split(new char[] { '_', '.' }); // split based on underscores and the extension
            for (int i = 0; i < words.Length - 1; i++) // keep all but last word (remove extension)
            {
                colloquialFilename += words[i] + " ";
            }
            colloquialFilename = colloquialFilename.TrimEnd(' '); // eliminate extra space
            return colloquialFilename;
        }

        private static Uri GetUri(string filename)
        {
            string filepath = resourcePathRelative + @"\" + filename;

            Uri uri = new Uri(@"ms-appx:///" + filepath);
            return uri;
        }

    }
}
