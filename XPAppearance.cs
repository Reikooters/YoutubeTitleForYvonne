using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace YoutubeTitleForYvonne
{
    /// <summary>
    /// Taken from: https://www.codeproject.com/Articles/20651/Capturing-Minimized-Window-A-Kid-s-Trick
    /// </summary>
    static partial class XPAppearance
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, ref ANIMATIONINFO pvParam, SPIF fWinIni);

        //When using the SPI_GETANIMATION or SPI_SETANIMATION actions, the uiParam value must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)).

        [Flags]
        enum SPIF
        {
            None = 0x00,
            SPIF_UPDATEINIFILE = 0x01,  // Writes the new system-wide parameter setting to the user profile.
            SPIF_SENDCHANGE = 0x02,  // Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
            SPIF_SENDWININICHANGE = 0x02   // Same as SPIF_SENDCHANGE.
        }

        /// <summary>
        /// ANIMATIONINFO specifies animation effects associated with user actions. 
        /// Used with SystemParametersInfo when SPI_GETANIMATION or SPI_SETANIMATION action is specified.
        /// </summary>
        /// <remark>
        /// The uiParam value must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)) when using this structure.
        /// </remark>
        [StructLayout(LayoutKind.Sequential)]
        private struct ANIMATIONINFO
        {
            /// <summary>
            /// Creates an AMINMATIONINFO structure.
            /// </summary>
            /// <param name="iMinAnimate">If non-zero and SPI_SETANIMATION is specified, enables minimize/restore animation.</param>
            public ANIMATIONINFO(bool iMinAnimate)
            {
                this.cbSize = GetSize();

                if (iMinAnimate) this.iMinAnimate = 1;
                else this.iMinAnimate = 0;
            }

            /// <summary>
            /// Always must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)).
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// If non-zero, minimize/restore animation is enabled, otherwise disabled.
            /// </summary>
            private int iMinAnimate;

            public bool IMinAnimate
            {
                get
                {
                    if (this.iMinAnimate == 0) return false;
                    else return true;
                }
                set
                {
                    if (value == true) this.iMinAnimate = 1;
                    else this.iMinAnimate = 0;
                }
            }

            public static uint GetSize()
            {
                return (uint)Marshal.SizeOf(typeof(ANIMATIONINFO));
            }

        }

        /// <summary>
        /// Gets or Sets MinAnimate Effect
        /// </summary>
        public static bool MinAnimate
        {
            get
            {
                ANIMATIONINFO animationInfo = new ANIMATIONINFO(false);

                SystemParametersInfo(SPI.SPI_GETANIMATION, ANIMATIONINFO.GetSize(), ref animationInfo, SPIF.None);
                return animationInfo.IMinAnimate;
            }
            set
            {
                ANIMATIONINFO animationInfo = new ANIMATIONINFO(value);
                SystemParametersInfo(SPI.SPI_SETANIMATION, ANIMATIONINFO.GetSize(), ref animationInfo, SPIF.SPIF_SENDCHANGE);
            }
        }
    }
}
