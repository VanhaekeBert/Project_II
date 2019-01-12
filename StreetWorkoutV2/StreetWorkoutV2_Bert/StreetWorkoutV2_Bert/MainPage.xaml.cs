using StreetWorkoutV2_Bert.Model;
using StreetWorkoutV2_Bert.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StreetWorkoutV2_Bert
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            var navAcccount = new NavigationPage(new AccountPage());
            navAcccount.Icon = "account.png";
            navAcccount.Title = "Account";

            var navDashboard = new NavigationPage(new DashboardPage());
            navDashboard.Icon = "dashboard.png";
            navDashboard.Title = "Dashboard";

            Children.Add(new SettingsPage());
            Children.Add(navDashboard);
            Children.Add(navAcccount);
       
        }
    }
}
