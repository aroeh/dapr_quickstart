using MemberService.Models;

namespace MemberService.Repo
{
    public interface IMemberRepo
    {
        Task<IEnumerable<MemberRecord>> GetAll();

        Task<MemberRecord> Get(string id);

        Task<string> Create(MemberRecord member);

        Task<bool> Update(MemberRecord member);
    }
}