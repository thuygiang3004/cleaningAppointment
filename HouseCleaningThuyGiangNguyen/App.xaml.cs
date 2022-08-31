using System.Windows;

namespace HouseCleaningThuyGiangNguyen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool isEdit = false;
            MainWindow mw = new MainWindow(isEdit);
            mw.Show();
        }
    }
}
