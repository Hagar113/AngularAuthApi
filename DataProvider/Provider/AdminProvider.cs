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
    public class AdminProvider:IAdminProvider
    {
        private readonly ApplicationDBContext _dbContext;
        public IAdminRepo AdminRepo {  get; private set; }

        public AdminProvider(ApplicationDBContext Context)
        {
            _dbContext = Context;
           AdminRepo=new AdminRepo(_dbContext);
        }
    }
}


