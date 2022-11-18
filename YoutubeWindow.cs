using Interop.UIAutomationClient;
using System;

namespace YoutubeTitleForYvonne
{
    class YoutubeWindow
    {
        public IntPtr Hwnd { get; set; }
        public string TabName { get; set; }
        public IUIAutomationElement elemTabStrip { get; set; }
        public IUIAutomationElement elemTab { get; set; }

        public YoutubeWindow Clone()
        {
            return (YoutubeWindow)this.MemberwiseClone();
        }
    }
}
