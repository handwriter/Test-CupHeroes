using System;
using UnityEngine;

namespace _Project.Runtime
{
    [Serializable]
    public struct EnemySpawnPointData
    {
        public Transform SpawnPoint;
        public Transform EndPoint;
    }
}