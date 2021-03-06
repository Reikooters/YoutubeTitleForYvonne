﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace YoutubeTitleForYvonne
{
    public partial class frmMain : Form
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsCallback Address, int y);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32")]
        private static extern int GetWindowLong(IntPtr hWnd, int index);
        [DllImport("user32")]
        private static extern int SetWindowLong(IntPtr hWnd, int index, int dwNewLong);
        [DllImport("user32")]
        private static extern int SetLayeredWindowAttributes(IntPtr hWnd, byte crey, byte alpha, int flags);

        private enum ShowWindowEnum { Hide = 0, ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3, Maximize = 3, ShowNormalNoActivate = 4, Show = 5, Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8, Restore = 9, ShowDefault = 10, ForceMinimized = 11 };
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        private static extern uint SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

        bool debug = false;

        List<YoutubeWindow> youtubeWindows { get; set; } = new List<YoutubeWindow>();
        BindingSource bindingSource { get; set; } = new BindingSource();

        YoutubeWindow selectedYoutubeWindow { get; set; }
        string lastPlayingTitle { get; set; }
        string outputFileName { get; set; }

        public const int DefaultRefreshInterval = 5;

        private const uint WM_MOUSELEAVE = 0x02A3;

        private const int GWL_STYLE = 16;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int LWA_ALPHA = 0x2;

        private static int winLongStyle;
        private static int winLongExStyle;
        private static bool minAnimateChanged = false;

        public frmMain()
        {
            InitializeComponent();

            bindingSource.DataSource = youtubeWindows;

            lstYouTubeWindows.DataSource = bindingSource;
            lstYouTubeWindows.DisplayMember = "TabName";
            lstYouTubeWindows.ValueMember = "Hwnd";

            outputFileName = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\nowplaying.txt";
        }

        private void btnSelectChromeWindow_Click(object sender, EventArgs e)
        {
            if (lstYouTubeWindows.SelectedItem != null)
            {
                YoutubeWindow youtubeWindow = (YoutubeWindow)lstYouTubeWindows.SelectedItem;

                selectedYoutubeWindow = youtubeWindow.Clone();

                ThreadHelperClass.SetEnabled(this, btnStartStop, true);

                MessageBox.Show("Selected: " + youtubeWindow.TabName + Environment.NewLine + Environment.NewLine + "If you close the selected window or move the YouTube tab into a different window, come back and start again from button #1.", "Selected Chrome Window", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (selectedYoutubeWindow.elemTabStrip != null)
            {
                if (!tmrUpdateCurrentlyPlaying.Enabled && btnStartStop.Text == "3. Start")
                {
                    try
                    {
                        System.IO.File.WriteAllText(outputFileName, "");
                    }
                    catch
                    {
                        MessageBox.Show("The selected output file/folder:" + Environment.NewLine + outputFileName + Environment.NewLine + "is not writeable. Please change output filename under Options.", "Invalid Output Filename", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    tmrUpdateCurrentlyPlaying.Enabled = !tmrUpdateCurrentlyPlaying.Enabled;

                    btnStartStop.Text = "3. Stop";
                    lblCurrentlyPlaying.Text = "Starting...";
                    lastPlayingTitle = null;
                }
                else
                {
                    try
                    {
                        System.IO.File.WriteAllText(outputFileName, "");
                    }
                    catch { }

                    tmrUpdateCurrentlyPlaying.Enabled = !tmrUpdateCurrentlyPlaying.Enabled;

                    btnStartStop.Text = "3. Start";
                    lblCurrentlyPlaying.Text = "Stopped";
                    lastPlayingTitle = null;
                }
            }
            else
            {
                MessageBox.Show("Please select Chrome window from the list first and click button #2.", "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnFindYouTubeWindows_Click(object sender, EventArgs e)
        {
            youtubeWindows.Clear();
            bindingSource.ResetBindings(false);

            //Grab all the Chrome processes
            Process[] chromeProcesses = Process.GetProcessesByName("chrome");

            //Chrome process not found
            if ((chromeProcesses.Length == 0))
            {
                MessageBox.Show(@"Google Chrome doesn't seem to be running.

Make sure Google Chrome is running with the YouTube tab open and that the Chrome window is not minimized, then try again.

It is OK to minimize the Chrome window after this step is completed.", "No Google Chrome not found.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bgwFindYouTubeWindows.RunWorkerAsync();
        }

        /// <summary>
        /// This function is based on jasonfah/chrome-tab-titles on GitHub
        /// https://github.com/jasonfah/chrome-tab-titles/blob/master/c%23/ChromeTabTitles/Form1.cs
        /// </summary>
        private void showTabTitles()
        {
            //Clear our array of tab titles
            youtubeWindows.Clear();

            // Kick off our search for chrome tab titles
            EnumWindowsCallback callBackFn = new EnumWindowsCallback(Enumerator);
            EnumWindows(callBackFn, 0);
        }

        /// <summary>
        /// <para>Enums through all visible windows - gets each chrome handle</para>
        /// <para>This function is based on jasonfah/chrome-tab-titles on GitHub
        /// https://github.com/jasonfah/chrome-tab-titles/blob/master/c%23/ChromeTabTitles/Form1.cs
        /// </para>
        /// </summary>
        private bool Enumerator(IntPtr hwnd, int lParam)
        {
            if (IsWindowVisible(hwnd))
            {
                StringBuilder sClassName = new StringBuilder(256);
                uint processId = 0;
                GetWindowThreadProcessId(hwnd, out processId);
                Process processFromID = Process.GetProcessById((int)processId);
                GetClassName(hwnd, sClassName, sClassName.Capacity);

                //Only want visible chrome windows (not any electron type apps that have chrome embedded!)
                if (((sClassName.ToString() == "Chrome_WidgetWin_1") && (processFromID.ProcessName == "chrome")))
                {
                    FindChromeTabs(hwnd);
                }
            }

            return true;
        }

        /// <summary>
        /// <para>Takes chrome window handle, searches for tabstrip by finding the parent of the "New Tab" button, then gets tab titles.</para>
        /// <para>This function is based on jasonfah/chrome-tab-titles on GitHub
        /// https://github.com/jasonfah/chrome-tab-titles/blob/master/c%23/ChromeTabTitles/Form1.cs
        /// </para>
        /// </summary>
        private void FindChromeTabs(IntPtr hwnd)
        {
            //To find the tabs we first need to locate something reliable - the 'New Tab' button
            AutomationElement rootElement = AutomationElement.FromHandle(hwnd);
            Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, "New Tab");

            //Find the 'new tab' button
            AutomationElement elemNewTab = rootElement.FindFirst(TreeScope.Descendants, condNewTab);

            //No tabstrip found
            if ((elemNewTab == null))
            {
                return;
            }

            //Get the tabstrip by getting the parent of the 'new tab' button
            TreeWalker tWalker = TreeWalker.ControlViewWalker;
            AutomationElement elemTabStrip = tWalker.GetParent(elemNewTab);

            //Loop through all the tabs and get the names which is the page title
            Condition tabItemCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
            foreach (AutomationElement tabItem in elemTabStrip.FindAll(TreeScope.Children, tabItemCondition))
            {
                if (!String.IsNullOrEmpty(tabItem.Current.Name) && tabItem.Current.Name.Contains("YouTube"))
                {
                    youtubeWindows.Add(new YoutubeWindow { TabName = tabItem.Current.Name, elemTabStrip = elemTabStrip, Hwnd = hwnd, elemTab = tabItem });
                    break;
                }
            }
        }

        private string GetUpdatedChromeTabTitleFromTab(AutomationElement elemTab)
        {
            string name = elemTab.Current.Name;

            if (!String.IsNullOrEmpty(name) && name.IndexOf(" - YouTube") != -1)
            {
                return CleanYoutubeTitle(name);
            }

            return null;
        }

        private string GetUpdatedChromeTabTitleFromTabStrip(AutomationElement elemTabStrip)
        {
            TreeWalker tWalker = TreeWalker.ControlViewWalker;

            // Get the first tab
            AutomationElement firstTab = TreeWalker.RawViewWalker.GetFirstChild(elemTabStrip);

            // If getting the first tab fails, stop here
            if (firstTab == null)
            {
                return null;
            }

            // Once getting the first tab successfully, see if it's a YouTube tab

            string name = firstTab.Current.Name;

            if (!String.IsNullOrEmpty(name) && name.IndexOf(" - YouTube") != -1)
            {
                return CleanYoutubeTitle(name);
            }

            // If not then walk through the siblings until we find the first YouTube tab
            AutomationElement tabSibling = firstTab;
            while ((tabSibling = TreeWalker.RawViewWalker.GetNextSibling(tabSibling)) != null)
            {
                name = tabSibling.Current.Name;

                if (!String.IsNullOrEmpty(name) && name.IndexOf(" - YouTube") != -1)
                {
                    return CleanYoutubeTitle(name);
                }
            }

            return null;
        }

        private string CleanYoutubeTitle(string name)
        {
            int youtubeIndex = 0;
            int startIndex = 0;
            bool looksLikeNotificationNumber = false;

            if (!String.IsNullOrEmpty(name))
            {
                // Remove " - YouTube" from tab title
                youtubeIndex = name.IndexOf(" - YouTube");

                if (youtubeIndex != -1)
                {
                    // Remove (#) from start of tab title (occurs when you receive notifications on YouTube)
                    if (name.Length > 1 && name[0] == '(')
                    {
                        for (int i = 1; i < name.Length; ++i)
                        {
                            if (char.IsDigit(name[i]))
                            {
                                looksLikeNotificationNumber = true;
                            }
                            else if (name[i] == ')' && looksLikeNotificationNumber)
                            {
                                if (name.Length > i + 2)
                                {
                                    startIndex = i + 2;
                                }
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    return name.Substring(startIndex, youtubeIndex - startIndex);
                }
            }

            return null;
        }

        private void bgwFindYouTubeWindows_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadHelperClass.SetVisible(this, progressBar, true);

            // Stop Timer
            tmrUpdateCurrentlyPlaying.Enabled = false;

            ThreadHelperClass.SetEnabled(this, btnFindChromeWindows, false);
            ThreadHelperClass.SetText(this, btnFindChromeWindows, "Finding non-minimized Chrome windows with YouTube tabs...");
            ThreadHelperClass.SetEnabled(this, btnSelectChromeWindow, false);
            ThreadHelperClass.SetText(this, btnSelectChromeWindow, "2. Select window from list above");
            ThreadHelperClass.SetEnabled(this, btnStartStop, false);
            ThreadHelperClass.SetText(this, btnStartStop, "3. Start");

            ThreadHelperClass.SetText(this, lblCurrentlyPlaying, "Stopped");

            showTabTitles();
            ThreadHelperClass.SetEnabled(this, btnFindChromeWindows, true);
            ThreadHelperClass.SetText(this, btnFindChromeWindows, "1. Find non-minimized Chrome windows with YouTube tabs");

            if (youtubeWindows.Count > 0)
            {
                ThreadHelperClass.SetEnabled(this, btnSelectChromeWindow, true);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawIcon(global::YoutubeTitleForYvonne.Properties.Resources.appicon, 0, 0);
        }

        private void bgwFindYouTubeWindows_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bindingSource.ResetBindings(false);
            progressBar.Visible = false;

            if (youtubeWindows.Count == 0)
            {
                MessageBox.Show(@"Google Chrome is open but could not find any non-minimized Chrome windows with YouTube tabs.

Make sure the YouTube tab is open and that the Chrome window is not minimized, then try again.

It is OK to minimize the Chrome window after this step is completed.", "No YouTube tabs were found.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tmrUpdateCurrentlyPlaying_Tick(object sender, EventArgs e)
        {
            tmrUpdateCurrentlyPlaying.Enabled = false;

            try
            {
                bgwUpdateCurrentlyPlaying.RunWorkerAsync();
            }
            catch (InvalidOperationException) when (debug == false)
            {
                lastPlayingTitle = null;
            }
        }

        private void bgwUpdateCurrentlyPlaying_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ThreadHelperClass.SetVisible(this, progressBar, true);

                string updatedTabName = GetUpdatedChromeTabTitleFromWindow(selectedYoutubeWindow.Hwnd);

                if (ThreadHelperClass.GetText(this, btnStartStop) == "3. Stop")
                {
                    if (String.IsNullOrEmpty(updatedTabName))
                    {
                        ThreadHelperClass.SetText(this, lblCurrentlyPlaying, "Could not get YouTube title. Is selected window/tab closed? Try starting again from button #1.");

                        try
                        {
                            if (!debug)
                            {
                                System.IO.File.WriteAllText(outputFileName, "");
                            }
                            else
                            {
                                System.IO.File.AppendAllText(outputFileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + Environment.NewLine);
                            }
                        }
                        catch
                        {
                            ThreadHelperClass.SetText(this, lblCurrentlyPlaying, "Error: Output file is not writeable. Please change output filename under options.");
                        }
                        
                        
                        lastPlayingTitle = null;
                    }
                    else if (lastPlayingTitle != updatedTabName || ThreadHelperClass.GetText(this, lblCurrentlyPlaying) == "Starting...")
                    {
                        lastPlayingTitle = updatedTabName;
                        ThreadHelperClass.SetText(this, lblCurrentlyPlaying, updatedTabName);

                        try
                        {
                            if (!debug)
                            {
                                System.IO.File.WriteAllText(outputFileName, updatedTabName);
                            }
                            else
                            {
                                System.IO.File.AppendAllText(outputFileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + updatedTabName + Environment.NewLine);
                            }
                        }
                        catch
                        {
                            ThreadHelperClass.SetText(this, lblCurrentlyPlaying, "Error: Output file is not writeable. Please change output filename under options.");
                            lastPlayingTitle = null;
                        }
                    }
                }
                else
                {
                    ThreadHelperClass.SetVisible(this, progressBar, false);
                }
            }
            catch when (debug == false)
            {
                ThreadHelperClass.SetText(this, lblCurrentlyPlaying, "Could not get YouTube title. Is selected window/tab closed? Try starting again from button #1.");

                if (!debug)
                {
                    System.IO.File.WriteAllText(outputFileName, "");
                }
                else
                {
                    System.IO.File.AppendAllText(outputFileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + Environment.NewLine);
                }
                    
                lastPlayingTitle = null;
            }
        }

        private void bgwUpdateCurrentlyPlaying_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (btnStartStop.Text == "3. Stop" && btnStartStop.Enabled == true)
            {
                tmrUpdateCurrentlyPlaying.Enabled = true;
                progressBar.Visible = false;
                tmrUpdateCurrentlyPlaying.Start();
            }
        }

        /// <summary>
        /// <para>If window is minimized, the window will be unminimized (invisibly), the title taken and then minimized again</para>
        /// <para>Based on: https://www.codeproject.com/Articles/20651/Capturing-Minimized-Window-A-Kid-s-Trick
        /// </para>
        /// </summary>
        /// <param name="Hwnd"></param>
        /// <returns></returns>
        private string GetUpdatedChromeTabTitleFromWindow(IntPtr Hwnd)
        {
            if (Hwnd == IntPtr.Zero)
            {
                return null;
            }

            if (IsIconic(Hwnd))
            {
                if (XPAppearance.MinAnimate)
                {
                    XPAppearance.MinAnimate = false;
                    minAnimateChanged = true;
                }

                // Show main window
                winLongStyle = GetWindowLong(Hwnd, GWL_STYLE);
                winLongExStyle = GetWindowLong(Hwnd, GWL_EXSTYLE);
                SetWindowLong(Hwnd, GWL_EXSTYLE, winLongExStyle | WS_EX_LAYERED);
                SetLayeredWindowAttributes(Hwnd, 0, 1, LWA_ALPHA);

                ShowWindow(Hwnd, ShowWindowEnum.ShowNormalNoActivate);

                //string updatedTabName = GetUpdatedChromeTabTitleFromTabStrip(selectedYoutubeWindow.elemTabStrip);
                string updatedTabName = GetUpdatedChromeTabTitleFromTab(selectedYoutubeWindow.elemTab);

                // Paint the window
                SendMessage(Hwnd, WM_MOUSELEAVE, 0, 0);

                // Minimize the main window again
                ShowWindow(Hwnd, ShowWindowEnum.ShowMinNoActivate);

                SetWindowLong(Hwnd, GWL_EXSTYLE, winLongExStyle);
                SetWindowLong(Hwnd, GWL_STYLE, winLongStyle);

                if (minAnimateChanged)
                {
                    XPAppearance.MinAnimate = true;
                    minAnimateChanged = false;
                }

                return updatedTabName;
            }
            else
            {
                //return GetUpdatedChromeTabTitleFromTabStrip(selectedYoutubeWindow.elemTabStrip);
                return GetUpdatedChromeTabTitleFromTab(selectedYoutubeWindow.elemTab);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (int.TryParse(ConfigurationManager.AppSettings.Get("RefreshInterval"), out int refreshInterval))
            {
                tmrUpdateCurrentlyPlaying.Interval = refreshInterval * 1000;
            }
            else
            {
                tmrUpdateCurrentlyPlaying.Interval = DefaultRefreshInterval * 1000;
            }

            // Get output filename
            outputFileName = ConfigurationManager.AppSettings.Get("OutputFilename");

            if (String.IsNullOrEmpty(outputFileName))
            {
                outputFileName = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\nowplaying.txt";
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            using (frmOptions form = new frmOptions())
            {
                // Set form properties...

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    tmrUpdateCurrentlyPlaying.Interval = Convert.ToInt32(form.RefreshInterval) * 1000;
                    outputFileName = form.OutputFilename;
                    lastPlayingTitle = null; // Force file write on next update

                    // Open App.Config of executable
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    // Add an Application Setting.
                    config.AppSettings.Settings.Remove("RefreshInterval");
                    config.AppSettings.Settings.Add("RefreshInterval", Convert.ToInt32(form.RefreshInterval).ToString());

                    // Add an Application Setting.
                    config.AppSettings.Settings.Remove("OutputFilename");
                    config.AppSettings.Settings.Add("OutputFilename", form.OutputFilename);

                    // Save the configuration file.
                    config.Save(ConfigurationSaveMode.Modified);
                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        /*
        /// <summary>
        /// Based on https://blogs.msdn.microsoft.com/winuiautomation/2015/08/06/finding-the-handle-of-the-window-that-contains-a-ui-automation-element/
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private IntPtr GetHwndForAutomationElement(AutomationElement elem)
        {
            // Build up a condition to access elements whose NativeWindowHandle is not zero.
            int propValueWindow = 0;

            Condition conditionNativeWindowHandleZero =
                new PropertyCondition(
                    AutomationElement.NativeWindowHandleProperty, propValueWindow);

            Condition conditionNativeWindowHandleNOTZero =
                new NotCondition(conditionNativeWindowHandleZero);

            // Now create a TreeWalker based on that condition.
            TreeWalker treeWalker =
                new TreeWalker(conditionNativeWindowHandleNOTZero);

            // Set up a cache request such that when we find an ancestor of interest,
            // we don't have to make more cross-process calls to get the data we're
            // interested in. For this test, cache the name and bounding rect.
            CacheRequest cacheRequestTest =
                new CacheRequest();

            cacheRequestTest.Add(AutomationElement.NameProperty);
            cacheRequestTest.Add(AutomationElement.NativeWindowHandleProperty);

            // Given that we only need the cached data, don't have UIA keep a
            // live reference around to the ancestor.
            cacheRequestTest.AutomationElementMode =
                AutomationElementMode.Full;

            // Now get the first ancestor with a not-zero NativeWindowHandle.
            AutomationElement elementAncestorWithWindow =
                treeWalker.Normalize(
                    elem, cacheRequestTest);

            return new IntPtr(elementAncestorWithWindow.Cached.NativeWindowHandle);
        }
        */
    }
}
