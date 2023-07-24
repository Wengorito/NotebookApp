using EvernoteClone.Model;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteClone.ViewModel.Helpers
{
    public class FirebaseResult
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string email { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public string localId { get; set; }
    }

    public class ErrorDetails
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Error
    {
        public ErrorDetails error { get; set; }
    }

    public class FirebaseAuthHelper
    {
        private static string ReadSetting(string key)
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

        public static async Task<bool> Register(User user)
        {


            using (HttpClient client = new HttpClient())
            {
                var uri = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + ReadSetting("FirebaseApiKey");

                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                var jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
                    App.UserId = result.localId;

                    return true;
                }
                else
                {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);


                    MessageBox.Show(error.error.message);
                    return false;
                }
            }

        }
    }


}
