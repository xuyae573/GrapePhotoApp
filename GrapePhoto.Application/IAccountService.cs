using GrapePhoto.Domain;
using GrapePhoto.Infrasturcture.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Application
{
    public interface IAccountService: IService<ApplicationUser, string>
    {

    }
}
