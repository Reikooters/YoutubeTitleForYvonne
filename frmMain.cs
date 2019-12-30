using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        List<YoutubeWindow> youtubeWindows { get; set; } = new List<YoutubeWindow>();
        BindingSource bindingSource { get; set; } = new BindingSource();

        AutomationElement selectedTabStrip { get; set; }
        string lastPlayingTitle { get; set; }
        string outputFileName { get; set; }

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

                selectedTabStrip = youtubeWindow.elemTabStrip;

                ThreadHelperClass.SetEnabled(this, btnStartStop, true);

                MessageBox.Show("Selected: " + youtubeWindow.Hwnd + " - " + youtubeWindow.TabName, "Selected Chrome Window");
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (selectedTabStrip != null)
            {
                tmrUpdateCurrentlyPlaying.Enabled = !tmrUpdateCurrentlyPlaying.Enabled;

                if (tmrUpdateCurrentlyPlaying.Enabled && btnStartStop.Text == "3. Start")
                {
                    btnStartStop.Text = "3. Stop";
                    lblCurrentlyPlaying.Text = "Starting...";
                    System.IO.File.WriteAllText(outputFileName, "");
                    lastPlayingTitle = null;
                }
                else
                {
                    btnStartStop.Text = "3. Start";
                    lblCurrentlyPlaying.Text = "Stopped";
                    System.IO.File.WriteAllText(outputFileName, "");
                    lastPlayingTitle = null;
                }
            }
            else
            {
                MessageBox.Show("Please select Chrome window first.", "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("Could not find open Google Chrome window.", "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //btnShowTabTitles.Enabled = false;
                return;
            }

            bgwFindYouTubeWindows.RunWorkerAsync();
        }

        private void showTabTitles()
        {
            //Clear our array of tab titles
            youtubeWindows.Clear();

            // Kick off our search for chrome tab titles
            EnumWindowsCallback callBackFn = new EnumWindowsCallback(Enumerator);
            EnumWindows(callBackFn, 0);

            //Add to our listbox
            //listBox1.Items.AddRange(youtubeWindows.ToArray());
        }

        //Enums through all visible windows - gets each chrome handle
        private bool Enumerator(IntPtr hwnd, int lParam)
        {
            if (IsWindowVisible(hwnd))
            {
                StringBuilder sClassName = new StringBuilder(256);
                uint processID = 0;
                GetWindowThreadProcessId(hwnd, out processID);
                Process processFromID = Process.GetProcessById((int)processID);
                GetClassName(hwnd, sClassName, sClassName.Capacity);

                //Only want visible chrome windows (not any electron type apps that have chrome embedded!)
                if (((sClassName.ToString() == "Chrome_WidgetWin_1") && (processFromID.ProcessName == "chrome")))
                {
                    FindChromeTabs(hwnd);
                }

            }

            return true;
        }

        //Takes chrome window handle, searches for tabstrip then gets tab titles
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
                    //tabTitles.Add(tabItem.Current.Name.ToString());
                    youtubeWindows.Add(new YoutubeWindow { Hwnd = hwnd, TabName = tabItem.Current.Name, elemTabStrip = elemTabStrip });
                    break;
                }
            }
        }

        private string GetUpdatedChromeTabTitle(AutomationElement elemTabStrip)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            TreeWalker tWalker = TreeWalker.ControlViewWalker;

            // Get the first tab and see if its a YouTube tab
            AutomationElement firstTab = TreeWalker.RawViewWalker.GetFirstChild(elemTabStrip);

            int youtubeIndex = 0;
            int startIndex = 0;
            bool looksLikeNotificationNumber = false;
            string name = firstTab.Current.Name;

            if (!String.IsNullOrEmpty(name))
            {
                youtubeIndex = name.IndexOf(" - YouTube");

                if (youtubeIndex != -1)
                {
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
                                startIndex = i + 2;
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

            // If not then walk through the siblings until we find the first YouTube tab
            AutomationElement tabSibling = firstTab;
            while ((tabSibling = TreeWalker.RawViewWalker.GetNextSibling(tabSibling)) != null)
            {
                youtubeIndex = 0;
                startIndex = 0;
                looksLikeNotificationNumber = false;
                name = tabSibling.Current.Name;

                if (!String.IsNullOrEmpty(name))
                {
                    youtubeIndex = name.IndexOf(" - YouTube");

                    if (youtubeIndex != -1)
                    {
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
                                    startIndex = i + 2;
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
                MessageBox.Show("Google Chrome is open but could not find any non-minimized windows with YouTube tabs.", "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tmrUpdateCurrentlyPlaying_Tick(object sender, EventArgs e)
        {
            tmrUpdateCurrentlyPlaying.Enabled = false;

            try
            {
                bgwUpdateCurrentlyPlaying.RunWorkerAsync();
            }
            catch (InvalidOperationException ex)
            {
                lastPlayingTitle = null;
            }
        }

        private void bgwUpdateCurrentlyPlaying_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ThreadHelperClass.SetVisible(this, progressBar, true);

                string updatedTabName = GetUpdatedChromeTabTitle(selectedTabStrip);

                if (ThreadHelperClass.GetText(this, btnStartStop) == "3. Stop")
                {
                    if (String.IsNullOrEmpty(updatedTabName))
                    {
                        ThreadHelperClass.SetText(this, lblCurrentlyPlaying, "Could not get YouTube title. Is selected window/tab closed or minimized?");
                        System.IO.File.WriteAllText(outputFileName, "");
                        lastPlayingTitle = null;
                    }
                    else if (lastPlayingTitle != updatedTabName || ThreadHelperClass.GetText(this, lblCurrentlyPlaying) == "Starting...")
                    {
                        lastPlayingTitle = updatedTabName;
                        ThreadHelperClass.SetText(this, lblCurrentlyPlaying, updatedTabName);
                        System.IO.File.WriteAllText(outputFileName, updatedTabName);
                    }
                }
                else
                {
                    ThreadHelperClass.SetVisible(this, progressBar, false);
                }
            }
            catch
            {
                ThreadHelperClass.SetText(this, lblCurrentlyPlaying, "Could not get YouTube title. Is selected window/tab closed or minimized?");
                System.IO.File.WriteAllText(outputFileName, "");
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
    }
}
