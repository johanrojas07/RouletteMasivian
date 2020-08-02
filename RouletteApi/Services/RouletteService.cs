using MongoDB.Driver;
using RouletteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Services
{
    public class RouletteService
    {
        private readonly IMongoCollection<RouletteDto> _roulettes;
        public RouletteService(IDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _roulettes = database.GetCollection<RouletteDto>(settings.RouletterCollectionName);
        }

        public List<RouletteDto> Get() =>
            _roulettes.Find(roulette => true).ToList();

        public RouletteDto Get(string id) =>
           _roulettes.Find<RouletteDto>(roulette => roulette.Id == id).FirstOrDefault();

        public RouletteDto Create(RouletteDto roulette)
        {
            _roulettes.InsertOne(roulette);
            return roulette;
        }

        public void Update(string id, RouletteDto rouletteIn) =>
            _roulettes.ReplaceOne(roulette => roulette.Id == id, rouletteIn);

        public void Remove(RouletteDto rouletteIn) =>
            _roulettes.DeleteOne(roulette => roulette.Id == rouletteIn.Id);

        public void Remove(string id) =>
            _roulettes.DeleteOne(roulette => roulette.Id == id);

        public static implicit operator RouletteService(BetsService v)
        {
            throw new NotImplementedException();
        }
    }
}
