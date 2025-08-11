using UniRx;
using UnityEngine;

namespace _Project.Runtime
{
    public interface ITeamMember
    {
        public int TeamIndex { get; }
        public ReactiveProperty<int> Health { get; }
        public ReactiveProperty<int> MaxHealth { get; }
        public void Attack(Vector3 targetPosition);
        public void Damage(int damage);
        public Vector3 GetPosition();
        public GameObject GetObject();
    }
}