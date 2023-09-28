using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;

namespace UniversityApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AppShell());

            LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationActionTapped;

        }
        private void OnNotificationActionTapped(NotificationActionEventArgs e)
        {
            if (e.IsDismissed)
            {
               
                return;
            }
            if (e.IsTapped)
            {
                // Dismiss Notification
                return;
            }
           
        }
    }
}