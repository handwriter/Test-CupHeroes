using UniRx;

namespace _Project.Runtime
{
    public class SaveLoadManager
    {
        public ReactiveProperty<int> Money = new();
        public ReactiveProperty<int> UnlockedAttackUpgrade = new();
        public ReactiveProperty<int> UnlockedTimeUpgrade = new();
        public ReactiveProperty<int> UnlockedHpUpgrade = new();
    }
}