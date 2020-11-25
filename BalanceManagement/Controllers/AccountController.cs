using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.DataAccess;
using AccountManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private AccountManagementDbContext _dbContext;

        public AccountController(AccountManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _dbContext.Accounts.ToList();
        }

        [HttpPost]
        public int Post(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            return account.Id;
        }
    }
}
