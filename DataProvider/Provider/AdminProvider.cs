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
    public class AdminProvider:IAdminProvider
    {

        private readonly ApplicationDBContext _dbContext;
        public IAdminRepo AdminRepo { get; private set; }

        public AdminProvider(ApplicationDBContext context)
        {
            _dbContext = context;
            AdminRepo = new AdminRepo(_dbContext);
        }
    }
}
