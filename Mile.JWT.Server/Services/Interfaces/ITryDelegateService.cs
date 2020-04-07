using JWT.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Server.Services.Interfaces
{
    public interface ITryDelegateService
    {
        string RunDelegate(string name);

        IEnumerable<Pet> AllSameCompanyPet();

        Task<string> RunThread();
    }
}
