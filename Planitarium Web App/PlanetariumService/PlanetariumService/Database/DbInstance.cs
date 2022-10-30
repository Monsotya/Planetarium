using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanetariumService.Database
{
    public class DbInstance
    {
        public static DbPlanetariumContext Instance { get; private set; }
        public static DbPlanetariumContext CreateInstance()
        {
            Instance = new DbPlanetariumContext();
            return Instance;
        }
    }
}
