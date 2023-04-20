using MemberService.Models;
using MongoDB.Driver;

namespace MemberService.Repo
{
    public class MemberRepo : IMemberRepo
    {
        private readonly ILogger<MemberRepo> logger;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<MemberRecord> collection;

        public MemberRepo(ILogger<MemberRepo> log)
        {
            logger = log;
            client = new MongoClient("connection string needs to go here");
            database = client.GetDatabase("dojo_records");
            collection = database.GetCollection<MemberRecord>("members");
        }

        public async Task<IEnumerable<MemberRecord>> GetAll()
        {
            logger.LogInformation("retrieving all members");
            var members = await collection.Find(d => true).ToListAsync();

            if(members == null)
            {
                logger.LogInformation("no members are present in the results");
                return new List<MemberRecord>();
            }

            logger.LogInformation("data found.  returning members");
            return members;
        }

        public async Task<MemberRecord> Get(string id)
        {
            logger.LogInformation("searching for matching member record");
            var member = await collection.Find(d => d.Id == id).FirstOrDefaultAsync();

            if(member == null)
            {
                logger.LogInformation("there was not member found matching the id");
                return new MemberRecord();
            }

            logger.LogInformation("member record found.  returning data");
            return member;
        }

        public async Task<string> Create(MemberRecord member)
        {
            logger.LogInformation("creating a new member");
            await collection.InsertOneAsync(member);

            return member.Id;
        }

        public async Task<bool> Update(MemberRecord member)
        {
            logger.LogInformation("updating member record");
            var result = await collection.ReplaceOneAsync(d => d.Id == member.Id, member);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}