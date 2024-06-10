﻿using MongoDB.Driver;
using ProjMongoDBAPI.Models;
using ProjMongoDBAPI.Utils;
using System.Collections.Generic;

namespace ProjMongoDB20220714.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _address;

        public AddressService(IProjMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
        }

        public List<Address> Get() =>
            _address.Find(address => true).ToList();


        public Address Get(string id) =>
            _address.Find<Address>(address => address.Id == id).FirstOrDefault();


        public Address Create(Address address)
        {
            _address.InsertOne(address);
            return address;
        }
    }
}