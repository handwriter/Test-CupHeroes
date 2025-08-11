using UnityEngine;

namespace _Project.Runtime
{
    [CreateAssetMenu(fileName = "UpgradesConfig", menuName = "Upgrades Config")]
    public class UpgradesConfig : ScriptableObject
    {
        public UpgradeData[] Attack;
        public UpgradeData[] Time;
        public UpgradeData[] HP;
    }
}