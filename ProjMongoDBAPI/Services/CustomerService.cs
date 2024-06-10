using MongoDB.Driver;
using ProjMongoDBAPI.Models;
using ProjMongoDBAPI.Utils;

namespace ProjMongoDBAPI.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customer;
        public CustomerService(IProjMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _customer = database.GetCollection<Customer>(settings.CustomerCollectionName);
        }

        public List<Customer> Get() =>_customer.Find(customer => true).ToList();

        public Customer Get(string Id) => _customer.Find<Customer>(customer => customer.Id == Id).FirstOrDefault();

        public Customer Create(Customer customer)
        {
            _customer.InsertOne(customer);
            return customer;
        }
        
        public Customer Update(Customer customer)
        {
            _customer.ReplaceOne(customer => customer.Id == customer.Id, customer);
            return customer;
        }

        public void Delete(Customer customer) 
        {
            _customer.DeleteOne(_customer => customer.Id == _customer.Id);
        }

        
        
    }
}
