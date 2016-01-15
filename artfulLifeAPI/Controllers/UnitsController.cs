﻿using artfulLifeAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace artfulLifeAPI.Controllers
{
    public class UnitsController : ApiController
    {
        public string dbuser = "pcj02b";
        public string dbpassword = "cloakd";

        // GET api/Units
        public async Task<IEnumerable<Models.Units>> Get()
        {
            var client = new MongoClient("mongodb://" + dbuser + ":" + dbpassword + "@ds036698.mongolab.com:36698/artful-life");
            var db = client.GetDatabase("artful-life");
            var units = db.GetCollection<Models.Units>("Units");
            var filter = new BsonDocument();
            var projection = Builders<Models.Units>.Projection.Exclude("_id");
            var output = await units.Find(filter).Project<Models.Units>(projection).ToListAsync();
            return output;
        }

        // GET api/Units/5
        public async Task<Models.Units> Get(string unit)
        {
            var client = new MongoClient("mongodb://" + dbuser + ":" + dbpassword + "@ds036698.mongolab.com:36698/artful-life");
            var db = client.GetDatabase("artful-life");
            var units = db.GetCollection<Models.Units>("Units");
            return (from recipe in await units.Find(r => r.unit == unit).ToListAsync()
                    select recipe).FirstOrDefault();
        }

        // POST api/Units
        public async void Post([FromBody]Models.Units value)
        {
            var client = new MongoClient("mongodb://" + dbuser + ":" + dbpassword + "@ds036698.mongolab.com:36698/artful-life");
            var db = client.GetDatabase("artful-life");
            var units = db.GetCollection<Models.Units>("Units");
            await units.InsertOneAsync(value);
        }

        // PUT api/Units/5
        public async void Put([FromBody]Models.Units value)
        {
            var client = new MongoClient("mongodb://" + dbuser + ":" + dbpassword + "@ds036698.mongolab.com:36698/artful-life");
            var db = client.GetDatabase("artful-life");
            var recipes = db.GetCollection<Models.Units>("Units");
            await recipes.ReplaceOneAsync(
                filter: new BsonDocument("unit", value.unit),
                replacement: value);
        }

        // DELETE api/Units/5
        public async void Delete(string unit)
        {
            var client = new MongoClient("mongodb://" + dbuser + ":" + dbpassword + "@ds036698.mongolab.com:36698/artful-life");
            var db = client.GetDatabase("artful-life");
            var recipes = db.GetCollection<Models.Units>("Units");
            await recipes.DeleteOneAsync(
                filter: new BsonDocument("unit", unit)
                );
        }
    }
}
