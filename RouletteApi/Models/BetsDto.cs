using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RouletteApi.Common;
using System;
using System.ComponentModel.DataAnnotations;


namespace RouletteApi.Models
{
    public class BetsDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdRoulette { get; set; }
        public string IdUser { get; set; }
        public BetTypeEnumerable BetType { get; set; }
        /// <summary>
        /// 1: red,
        /// 0: black 
        /// </summary>
        [Range(0, 1)]
        public short? Color { get; set; }
        [Range(0, 36)]
        public int? Position { get; set; }
        [Range(0.1, maximum: 10000)]
        public double BetValue { get; set; }
        public DateTime? BetDate { get; set; }
    }
}
