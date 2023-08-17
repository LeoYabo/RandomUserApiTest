using RandomUserApi.Models;
using Newtonsoft.Json;

namespace RandomUserApi.Services
{
    public class UserService
    {

        private readonly IHttpClientFactory _httpClientFactory;


        //gets random 10 users by the gender specified
        public async Task<IEnumerable<User>> GetUsersData(string gender, int amount = 10)
        {
            var client = _httpClientFactory.CreateClient("randomUser");
            var response = await client.GetAsync($"?gender={gender}&results={amount}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var resultJson = JsonConvert.DeserializeObject<RandomUserResponse>(result);

                return resultJson.users;
            }

            return Enumerable.Empty<User>();
        }

        //gets list of 30 emails from random users
        public async Task<IEnumerable<string>> GetListOfMails()
        {
            var client = _httpClientFactory.CreateClient("randomUser");
            var response = await client.GetAsync($"?results=30");


            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<RandomUserResponse>(result);

                return results.users.Select(g => g.email);
            }

            return null;
        }

        //gets the most popular country out of a random sample of 5000 users
        public async Task<string> GetMostPopularCountry()
        {
            var client = _httpClientFactory.CreateClient("randomUser");
            var response = await client.GetAsync($"?results=5000");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<RandomUserResponse>(result);

                return results.users.GroupBy(x => x.location.country).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
            }

            return null;
        }
        //gets the oldest user's name and age out of a random sample of 100 users
        public async Task<string> GetOldestUser()
        {
            var client = _httpClientFactory.CreateClient("randomUser");
            var response = await client.GetAsync($"?results=100");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<RandomUserResponse>(result);

                return results.users.OrderByDescending(g => g.dob.age).FirstOrDefault();
            }

            return null;
        }

}