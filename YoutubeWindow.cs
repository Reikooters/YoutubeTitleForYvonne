using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace YoutubeTitleForYvonne
{
    class YoutubeWindow
    {
        public IntPtr Hwnd { get; set; }
        public string TabName { get; set; }
        public AutomationElement elemTabStrip { get; set; }

        public YoutubeWindow Clone()
        {
            return (YoutubeWindow)this.MemberwiseClone();
        }
    }
}
