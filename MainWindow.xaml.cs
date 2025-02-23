using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.UI;
using System.Collections.ObjectModel;
using App1.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();
        public MainWindow()
        {
            this.InitializeComponent();
            this.setWindowSize(800, 600);
            textList.ItemsSource = Notes;
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            addTextToList();
        }

        private void addTextToList()
        {
            if (!String.IsNullOrEmpty(textInput.Text)) {
                Notes.Add(
                    new Note
                        { 
                            title = textInput.Text, 
                            contents = string.Empty
                    }
                  );
                textInput.Text = "";
            }
        }
        private void setWindowSize(int width, int height)
        {
            var hwnd = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            var appwindow = AppWindow.GetFromWindowId(windowId);
            appwindow.Resize(new Windows.Graphics.SizeInt32(width, height));
        }


        private void textList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem !=null)
            {
                //do some type casting
                Note clickedNote = (Note)e.ClickedItem;
                // Create a new instance of BlankPage1 and assign the selected Note
                BlankPage1 detailPage = new BlankPage1();
                detailPage.selectedNote = clickedNote;

                // Replace the entire window content with the new page
                this.Content = detailPage;
            }
        }
    }
}
