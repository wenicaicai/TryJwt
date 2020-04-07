using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Server.Services
{
    public class PetEventArgs : EventArgs
    {
        public string Name { get; set; }

        public int ID { get; set; }
    }
}
