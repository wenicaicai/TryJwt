using JWT.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Server.Services
{
    public class DogHairLengthComparer : IEqualityComparer<Pet>
    {
        bool IEqualityComparer<Pet>.Equals(Pet a, Pet b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if ((a == null && b != null) ||
                     (a != null && b == null))
            {
                return false;
            }
            else
            {
                return a.Name == b.Name;
            }
        }

        int IEqualityComparer<Pet>.GetHashCode(Pet obj)
        {
            return obj.GetHashCode();
        }
    }
}
