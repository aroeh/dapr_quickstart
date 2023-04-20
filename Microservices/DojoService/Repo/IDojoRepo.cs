using DojoService.Models;

namespace DojoService.Repo
{
    public interface IDojoRepo
    {
        Task<IEnumerable<Dojo>> GetAll();

        Task<Dojo> Get(string id);

        Task<string> Create(Dojo dojo);

        Task<bool> Update(Dojo dojo);
    }
}