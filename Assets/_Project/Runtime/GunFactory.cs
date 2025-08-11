using System.Linq;
using UnityEngine;

namespace _Project.Runtime
{
    public class GunFactory
    {
        private GunsConfig _config;
        
        public GunFactory(GunsConfig gunsConfig)
        {
            _config = gunsConfig;    
        }

        public GameObject GetGun(string name)
        {
            var gunData = _config.Guns.First((x) => x.ID == name);
            var gun = Object.Instantiate(gunData.Object);
            return gun;
        }
    }
}