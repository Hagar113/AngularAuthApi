using DataAccess.IRepo;
using Models.DTOs.Response;

namespace DataProvider.IProvider
{
    public interface IStudentProvider
    {
        public IStudentRepo StudentRepo { get; }
    }
}
