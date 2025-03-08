using Dogs.Models;
using Newtonsoft.Json.Linq;

namespace Dogs.Services
{
    public class DogService
    {
        private readonly HttpClient _httpClient;

        public DogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(List<Dog>, int)> GetBreedsAsync(int page, int limit)
        {
            var response = await _httpClient.GetStringAsync($"https://dogapi.dog/api/v2/breeds?page[number]={page}&page[size]={limit}");
            var data = JObject.Parse(response)["data"];
            var totalRecords = int.Parse(JObject.Parse(response)["meta"]["pagination"]["records"].ToString());
            var breeds = data.Select(b => new Dog
            {
                Id = b["id"].ToString(),
                Name = b["attributes"]["name"].ToString()
            }).ToList();
            return (breeds ?? new List<Dog>(), totalRecords);
        }

        public async Task<Dog> GetDogDetailsAsync(string id)
        {
            var response = await _httpClient.GetStringAsync($"https://dogapi.dog/api/v2/breeds/{id}");
            var breed = JObject.Parse(response)["data"];
            var dog = new Dog
            {
                Id = breed["id"].ToString(),
                Name = breed["attributes"]["name"].ToString(),
                Description = breed["attributes"]["description"].ToString(),
                Hypoallergenic = bool.Parse(breed["attributes"]["hypoallergenic"].ToString()),
                LifeSpanMin = int.Parse(breed["attributes"]["life"]["min"].ToString()),
                LifeSpanMax = int.Parse(breed["attributes"]["life"]["max"].ToString()),
                MaleWeightMin = int.Parse(breed["attributes"]["male_weight"]["min"].ToString()),
                MaleWeightMax = int.Parse(breed["attributes"]["male_weight"]["max"].ToString()),
                FemaleWeightMin = int.Parse(breed["attributes"]["female_weight"]["min"].ToString()),
                FemaleWeightMax = int.Parse(breed["attributes"]["female_weight"]["max"].ToString())
            };
            return dog ?? new Dog();
        }
    }
}
