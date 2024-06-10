using MongoDB.Driver;
using ProjMongoDBAPI.Models;
using ProjMongoDBAPI.Utils;

namespace ProjMongoDBAPI.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _address;

        public AddressService(IProjMongoDBAPIDataBaseSettings settings)
        {
            var client= new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);

        }

        public List<Address> GetAddresses() => _address.Find(address => true).ToList();

        public Address Get(string Id) => _address.Find<Address>(address => address.Id == Id).FirstOrDefault();
        public Address Create(Address address)
        {
            _address.InsertOne(address);
            return address;
        }




        
            
            
        
    }
}
