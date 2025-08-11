using TMPro;
using UniRx;
using UnityEngine;

namespace _Project.Runtime
{
    public class Player : MonoBehaviour, ITeamMember
    {
        public string StartGun;
        public int TeamIndex => 0;
        public ReactiveProperty<int> Health { get; private set; }
        public ReactiveProperty<int> MaxHealth { get; private set; }

        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBarPresenter _healthBar;
        private IGun _gun;
        private GameConfig _gameConfig;
        private static readonly int _walk = Animator.StringToHash("Walk");
        private static readonly int _attack = Animator.StringToHash("Attack");

        public void Initialize(int maxHealth, SaveLoadManager saveLoadManager, GameConfig gameConfig)
        {
            MaxHealth = new ReactiveProperty<int>(maxHealth);
            Health = new ReactiveProperty<int>(maxHealth);
            Health.Subscribe(value => _healthBar.SetData((float)value / MaxHealth.Value, value));
            MaxHealth.Subscribe(value => _healthBar.SetData((float)Health.Value / value, Health.Value));
            MaxHealth.Subscribe(value => Health.Value = value);

            saveLoadManager.UnlockedHpUpgrade.Subscribe(value =>
            {
                if (value > 0)
                {
                    var data = gameConfig.UpgradesConfig.HP[Mathf.Min(value - 1, gameConfig.UpgradesConfig.HP.Length - 1)];
                    MaxHealth.Value = gameConfig.PlayerStartHealth + data.Value;
                }
            });

            saveLoadManager.UnlockedAttackUpgrade.Subscribe((value) =>
            {
                if (value > 0)
                {
                    var data = gameConfig.UpgradesConfig.Attack[Mathf.Min(gameConfig.UpgradesConfig.Attack.Length - 1, value - 1)];
                    _gun.AdditionalDamage = data.Value;
                }
            });
            
            _gameConfig = gameConfig;
        }

        public void SetGun(GameObject gun)
        {
            gun.transform.parent = transform;
            gun.transform.position = transform.position;
            _gun = gun.GetComponent<IGun>();
            _gun.Damage = _gameConfig.PlayerDamage;
            _gun.Initialize(TeamIndex);
        }
        
        public void SetWalkState(bool value)
        {
            _animator.SetBool(_walk, value);
        }
        
        public void Attack(Vector3 targetPosition)
        {
            _gun.Shoot(targetPosition);
            _animator.SetTrigger(_attack);
        }

        public void Damage(int damage)
        {
            Health.Value = Mathf.Max(Health.Value - damage, 0);
        }

        public Vector3 GetPosition() => transform.position;
        public GameObject GetObject() => gameObject;
    }
}