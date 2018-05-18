using GrapePhoto.Domain;
using GrapePhoto.Infrasturcture.Repoistory;
using GrapePhoto.Infrasturcture.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Application
{
    public class AccountService : ServiceBase<ApplicationUser, string>, IAccountService
    {
        public AccountService()
        {
        }

        protected AccountService(IRepository<ApplicationUser, string> repository) : base(repository)
        {
        }
    }
}
