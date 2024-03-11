using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordTool
{

    internal class Program
    {
        static async Task Main(string[] args) 
        {
            Console.Write("Mail Gir:");
            string email = Console.ReadLine();
            Console.Write("Sifre Gir:");
            string pass = Console.ReadLine();
            await getToken(email, pass);   
        }
        async static Task getToken(string email, string pass)
        {
          string url = "https://discord.com/api/v9/auth/login";
            var payload = new Dictionary<string, string>()
            {
                { "login", email },{"password" , pass}
            };
            string jsonPayload = JsonConvert.SerializeObject(payload);  
            HttpClient client = new HttpClient();
            StringContent stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nGiris Basarili!");
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseContent);
                Console.WriteLine($"Kullanici Benzersiz Kimlik: {json["user_id"]}");
                Console.WriteLine($"Kullanici Token: {json["token"]}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
            }
        }

    }






}