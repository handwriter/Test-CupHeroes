using UnityEngine;

namespace _Project.Runtime
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
    public class GameConfig : ScriptableObject
    {
        public int MaxEnemiesCount;
        public float DelayBetweenTeamAttacks;
        public int PlayerStartHealth;
        public int EnemyHealth;
        public int EnemyDamage;
        public UpgradesConfig UpgradesConfig;
        public int RewardForEnemies;
        public float PlayerAttacksDelay;
        public int PlayerDamage;
    }
}