using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneAppTest.Resources;

namespace PhoneAppTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Check();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public void InitializeButton(Button x,string name,string content)
        {
            x.Name = name;
            x.Content = content;
            x.Height = 100;
            x.Width = 100;
            x.Margin = new Thickness(0, 0, 0, 0);
            x.HorizontalAlignment = HorizontalAlignment.Left;
            x.VerticalAlignment = VerticalAlignment.Top;
        }
        public void Check()
        {
            Button x = new Button();
            InitializeButton(x,"button1","test1");

            Button y = new Button();
            InitializeButton(y, "button2", "test2");
            y.Margin = new Thickness(x.Width, 0, 0, 0);

            Button z = new Button();
            InitializeButton(z, "button3", "test3");
            z.Margin = new Thickness(x.Width + y.Width,0,0,0);

            

            ContentPanel.Children.Add(x);
            ContentPanel.Children.Add(y);
            ContentPanel.Children.Add(z);

            


            //y.Margin = new Thickness(x.Width, 0, 0, 0);

            /*Button z = new Button();
            InitializeButton(z, "button3", "test3");
            //z.Margin = new Thickness(x.Width + y.Width, 0, 0, 0);*/

            //Grid.SetColumn(y, (int)x.Width);
            
            
            
            /*Button y = new Button();
            y.Name = "button2";
            y.Content = "test2";
            y.Size = new Size(50, 50);
            y.Height = 50;
            */

            
            //ContentPanel.Children.Add(z);
            //ContentPanel.Children.Add(y);
            
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}