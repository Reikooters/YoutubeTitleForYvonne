using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeTitleForYvonne
{
    /// <summary>
    /// Helper class based on code by Thunder of StackOverflow.
    /// https://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe/15831292#15831292
    /// </summary>
    public static class ThreadHelperClass
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);

        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        delegate string GetTextCallback(Form f, Control ctrl);

        /// <summary>
        /// Get text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        public static string GetText(Form form, Control ctrl)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                GetTextCallback d = new GetTextCallback(GetText);
                return (string)form.Invoke(d, new object[] { form, ctrl });
            }
            else
            {
                return ctrl.Text;
            }
        }

        delegate void SetEnabledCallback(Form f, Control ctrl, bool enabled);

        /// <summary>
        /// Set enabled property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="enabled"></param>
        public static void SetEnabled(Form form, Control ctrl, bool enabled)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetEnabledCallback d = new SetEnabledCallback(SetEnabled);
                form.Invoke(d, new object[] { form, ctrl, enabled });
            }
            else
            {
                ctrl.Enabled = enabled;
            }
        }

        delegate void SetVisibleCallback(Form f, Control ctrl, bool visible);

        /// <summary>
        /// Set enabled property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="visible"></param>
        public static void SetVisible(Form form, Control ctrl, bool visible)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetVisibleCallback d = new SetVisibleCallback(SetVisible);
                form.Invoke(d, new object[] { form, ctrl, visible });
            }
            else
            {
                ctrl.Visible = visible;
            }
        }
    }
}
