using DataAccess.IRepo;
using DataAccess.Repo;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.IProvider;

namespace DataProvider.Provider
{
    public class StudentProvider : IStudentProvider
    {
        private readonly ApplicationDBContext _dbContext;
        public IStudentRepo StudentRepo { get; private set; }

        public StudentProvider(ApplicationDBContext context)
        {
            _dbContext = context;
            StudentRepo = new StudentRepo(_dbContext);
        }

    }
}
