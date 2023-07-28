using System.Configuration;
using System.Windows;

namespace EvernoteClone.ViewModel.Helpers
{
    public static class AppSecretsHelper
    {
        public static string Read(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(key) ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error reading app settings");
                throw;
            }
        }
    }
}