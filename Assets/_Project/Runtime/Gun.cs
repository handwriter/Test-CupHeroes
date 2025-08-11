using System;
using UnityEngine;

namespace _Project.Runtime
{
    public class Gun : MonoBehaviour, IGun
    {
        public int Damage { get => _damage; set => _damage = value; }
        public int AdditionalDamage { get; set; }

        [SerializeField] private int _damage;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _firePoint;
        private int _teamIndex;
        
        public void Initialize(int teamIndex)
        {
            _teamIndex = teamIndex;
        }
        
        public void Shoot(Vector3 position)
        {
            var bullet = Instantiate(_bullet, transform);
            bullet.transform.position = _firePoint.position;
            bullet.GetComponent<IBullet>().Initialize(position, _teamIndex, _damage + AdditionalDamage);
        }
    }
}