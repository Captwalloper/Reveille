using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace Reveille.Utility
{
    public class ClipboardHelper
    {
        public static void CopyToClipboard(string s)
        {
            DataPackage content = new DataPackage();
            content.SetText(s);
            Clipboard.SetContent(content);
        }
    }
}
