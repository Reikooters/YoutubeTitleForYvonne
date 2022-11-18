# YoutubeTitleForYvonne

A Windows application written in C# using .NET Framework 4.5. Created for my friend Yvonne.

This application is used to monitor a Google Chrome tab which has a YouTube video open, and writes the title of the video to a .txt file on your computer at an interval - by default, this is done every 5 seconds. The interval and output path and filename are configurable.

You can then create an overlay in a streaming program such as Streamlabs OBS which shows the contents of the text file. This allows you to have music playing on YouTube in the background on your computer, and have the title of the video shown on-screen on your Twitch, YouTube, etc stream. Instructions on how to configure StreamLabs OBS to do this are below if needed.

## Download

Visit the [Releases](https://github.com/Reikooters/YoutubeTitleForYvonne/releases/latest) page to download the latest version.

## Installation

All you need to do is simply unzip the application into its own folder and run `YoutubeTitleForYvvone.exe`. No installation process is required.

The application requires the .NET Framework 4.5 runtime to be installed on your computer. You should already have this installed on your computer from Windows Updates if you are using Windows 8 or above. In case you do need to install it manually, you can download it here: https://www.microsoft.com/en-gb/download/details.aspx?id=30653

## How to use

The application is straight forward to use. Instructions are also provided within the application, but the steps are provided here:

1. First, open Google Chrome and navigate to YouTube. Do not minimize or make the Google Chrome window full screen, as the application will not be able to scan the tabs in the window otherwise.
2. In the application, click the "Find YouTube tabs in non-minimized Chrome windows" button. This will scan all of your Google Chrome windows to find all of your open YouTube tabs. This may take a few moments (depending on how many Chrome windows and tabs you have open in total).
3. Once scanning is finished, a list of the YouTube tabs that were found will be displayed. Select the tab from the list which you intend to use for playing music.
4. Click the "Select tab from list above" button. The application will remember your choice and disable the list.
5. Finally, click the "Start monitoring tab title" button.  The title of the YouTube tab will now be saved to a text file on a default interval of every 5 seconds. The video title is written to the file `nowplaying.txt` within the application's directory by default.

#### Notes

Click the "Options" button in the application to configure the interval and output filename.

The tab title is automatically cleaned to get just the video title. It will attempt to remove:

- " - YouTube" from the end of the tab title
- YouTube notifications, e.g. "(1)" at the start of the tab title.
- Unnecessary text commonly included in music video titles that are not part of the song name, e.g. "Official Video", "Lyrics Video", "Music Video", "MV", "HD Video", etc.

#### Once tab is selected, you can now minimize/full screen the window if desired

Once you have selected the tab as per step #4 above, you can now minimize or make the Google Chrome window full screen and the application will still work. This means that you could start a YouTube playlist and minimize the window - the application will still pick up the currently playing video title when YouTube progresses to the next video in the playlist.

This is possible using some magic where at each refresh interval, the Chrome window with the selected YouTube tab will be restored from being minimized, but it will be invisible and won't catch keys or clicks. The current title will then be retrieved and the window will be minimized again until the next refresh interval. During testing, this did not cause any issues while playing a game in full screen, so there should not be any side effects to this. If you do experience issues, you can leave the Chrome window unminimized to prevent this behaviour.

#### Reselect a new tab if you close or drag the selected tab to another window

If you close the selected YouTube tab or move it to a different window, come back to the application and start again by searching for YouTube tabs using button #1, then follow the process from the beginning. Moving the tab around within the same Chrome window by dragging it is OK and won't require reselecting the tab.

## Configuration

To configure the application, click the "Options" button in the top right.

You can specify the Refresh Interval in seconds, as well as the output path and filename of the .txt file which will be written to.

These settings are stored in `YoutubeTitleForYvvone.exe.config` in the application's folder.

## Showing a text overlay on your stream using StreamLabs OBS

These instructions explain how to add a text file overlay to your stream using StreamLabs OBS (https://streamlabs.com/). The instructions are provided for information purposes and have nothing to do with this YoutubeTitleForYvonne application.

1. In StreamLabs OBS, under the "Sources" panel, click the + button.
2. Select "Text (GDI+)" from the list of sources.
3. Tick the "Add a new source instead" checkbox.
4. Give the source a name, e.g. "Now Playing" and click the "Add Source" button.
5. Next to the "Text" field, tick the "Read from file" checkbox.
6. Click the "Browse" button that appears.
7. Choose the text file which is written to by YoutubeTitleForYvonne - by default this is `nowplaying.txt` in the application folder, but you can also configure this in the Options screen.
8. Configure the font, color and styles to your liking.
9. Click the "Done" button.

#### Scrolling/ticker effect

To improve the visual appearance, follow these steps to setup scrolling. This makes the text scroll within a constrained area like a ticker, similar to the old HTML marquee effect from the 1990s/2000s.

1. Rght click on the text source you created in the "Sources" panel.
2. Choose Filters > Edit Filters.
3. Click the "+ Add Filter" button.
4. In the Filter Types list, select "Scroll" and click the "Add" button.
5. Now tick the "Limit Width" checkbox. Set the desired maximum width in pixels.
6. Drag the "Horizontal Speed" slider to your liking (positive values will scroll left, while negative values will scroll right)
7. Click the "Close" button to close the filters window.
8. Click and drag to position the text on the screen as you wish.

## Credits

1. The Chrome window process and classname, used to locate visible Chrome windows, was discovered from the code in this project: https://github.com/jasonfah/chrome-tab-titles/blob/master/c%23/ChromeTabTitles/Form1.cs
2. The trick for obtaining the YouTube video title from minimized Chrome windows was inspired by this post: https://www.codeproject.com/Articles/20651/Capturing-Minimized-Window-A-Kid-s-Trick
