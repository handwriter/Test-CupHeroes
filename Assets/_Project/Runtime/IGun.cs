using System;
using UnityEngine;

namespace _Project.Runtime
{
    public interface IGun
    {
        public int Damage { get; set; }
        public int AdditionalDamage { get; set; }
        public void Initialize(int teamIndex);
        public void Shoot(Vector3 position);
    }
}