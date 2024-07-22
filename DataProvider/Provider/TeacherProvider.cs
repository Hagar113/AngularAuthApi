using DataAccess.IRepo;
using DataAccess.Repo;
using DataAccess;
using DataProvider.IProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Provider
{
    public class TeacherProvider : ITeacherProvider
    {
        private readonly ApplicationDBContext _dbContext;
        public ITeacherRepo TeacherRepo { get; private set; }

        public TeacherProvider(ApplicationDBContext context)
        {
            _dbContext = context;
            TeacherRepo = new TeacherRepo(_dbContext);
        }
        ITeacherRepo ITeacherProvider.teacherRepo => TeacherRepo;
    }
}
