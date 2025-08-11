using UnityEngine;

namespace _Project.Runtime
{
    public interface IBullet
    {
        public void Initialize(Vector3 targetPosition, int teamIndex, int damage);
    }
}