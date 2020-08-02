using MongoDB.Driver;
using RouletteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApi.Services
{
    public class BetsService
    {
        private readonly IMongoCollection<BetsDto> _bets;
        public BetsService(IDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _bets = database.GetCollection<BetsDto>(settings.BetsCollectionName);
        }

        public List<BetsDto> Get() =>
            _bets.Find(best => true).ToList();

        public List<BetsDto> GetBestInRoulette() =>
            _bets.Find(best => true).ToList();

        public List<BetsDto> GetBestInRoulette(string idRoulette) =>
           _bets.Find<BetsDto>(best => best.IdRoulette == idRoulette).ToList();

        public BetsDto Create(BetsDto bets)
        {
            _bets.InsertOne(bets);
            return bets;
        }

        public void Update(string id, BetsDto rouletteIn) =>
            _bets.ReplaceOne(roulette => roulette.Id == id, rouletteIn);

        public void Remove(BetsDto rouletteIn) =>
            _bets.DeleteOne(roulette => roulette.Id == rouletteIn.Id);

        public void Remove(string id) =>
            _bets.DeleteOne(roulette => roulette.Id == id);

        public static implicit operator BetsService(RouletteService v)
        {
            throw new NotImplementedException();
        }
    }
}
