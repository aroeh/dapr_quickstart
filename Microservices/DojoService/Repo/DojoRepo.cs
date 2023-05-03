using DojoService.Models;
using MongoDB.Driver;

namespace DojoService.Repo
{
    public class DojoRepo : IDojoRepo
    {
        private readonly ILogger<DojoRepo> logger;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Dojo> collection;

        public DojoRepo(ILogger<DojoRepo> log, IConfiguration config)
        {
            logger = log;
            client = new MongoClient(config["mongodb"]);
            database = client.GetDatabase("dojo_records");
            collection = database.GetCollection<Dojo>("dojo");
        }

        public async Task<IEnumerable<Dojo>> GetAll()
        {
            logger.LogInformation("retrieving all dojos");
            var dojos = await collection.Find(d => true).ToListAsync();

            if(dojos == null)
            {
                logger.LogInformation("no dojos are present in the results");
                return new List<Dojo>();
            }

            logger.LogInformation("data found.  returning dojos");
            return dojos;
        }

        public async Task<Dojo> Get(string id)
        {
            logger.LogInformation("searching for matching dojo record");
            var dojo = await collection.Find(d => d.Id == id).FirstOrDefaultAsync();

            if(dojo == null)
            {
                logger.LogInformation("there was not dojo found matching the id");
                return new Dojo();
            }

            logger.LogInformation("dojo record found.  returning data");
            return dojo;
        }

        public async Task<string> Create(Dojo dojo)
        {
            logger.LogInformation("creating a new dojo");
            await collection.InsertOneAsync(dojo);

            return dojo.Id;
        }

        public async Task<bool> Update(Dojo dojo)
        {
            logger.LogInformation("updating dojo record");
            var result = await collection.ReplaceOneAsync(d => d.Id == dojo.Id, dojo);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}