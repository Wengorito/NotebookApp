using EvernoteClone.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helpers
{
    public class FirebaseDatabaseHelper
    {
        private const string DB_PATH = "https://notesappwpf-edf82-default-rtdb.europe-west1.firebasedatabase.app/";

        public static async Task<bool> Insert<T>(T item)
        {
            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PostAsync($"{DB_PATH}{item.GetType().Name.ToLower()}.json", content);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public static async Task<List<T>> Read<T>() where T : HasId
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{DB_PATH}{typeof(T).Name.ToLower()}.json");

                if (result.IsSuccessStatusCode)
                {
                    var jsonResult = await result.Content.ReadAsStringAsync();
                    var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);

                    var list = new List<T>();
                    foreach (var obj in objects)
                    {
                        obj.Value.Id = obj.Key;
                        list.Add(obj.Value);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<bool> Update<T>(T item) where T : HasId
        {
            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PatchAsync($"{DB_PATH}{item.GetType().Name.ToLower()}/{item.Id}.json", content);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public static async Task<bool> Delete<T>(T item) where T : HasId
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"{DB_PATH}{item.GetType().Name.ToLower()}/{item.Id}.json");

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
