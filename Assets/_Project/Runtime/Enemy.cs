using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _Project.Runtime
{
    public class Enemy : MonoBehaviour, ITeamMember
    {
        public string StartGun;
        public int TeamIndex => 1;
        public ReactiveProperty<int> Health { get; private set;  }
        public ReactiveProperty<int> MaxHealth { get; private set; }
        [HideInInspector] public ReactiveProperty<bool> IsReady = new ();
        [SerializeField] private Animator _animator;
        [SerializeField] private float _moveDuration = 2;
        [SerializeField] private HealthBarPresenter _healthBar;
        private static readonly int _attack = Animator.StringToHash("Attack");
        private IGun _gun;

        public void Initialize(int maxHealth)
        {
            Health = new ReactiveProperty<int>(maxHealth);
            MaxHealth = new ReactiveProperty<int>(maxHealth);
            Health.Subscribe(value => _healthBar.SetData((float)value / MaxHealth.Value, value));
            MaxHealth.Subscribe(value => _healthBar.SetData((float)Health.Value / value, Health.Value));
        }

        public void Setup(EnemySpawnPointData spawnPointData)
        {
            transform.position = spawnPointData.SpawnPoint.position;
            transform.parent = spawnPointData.SpawnPoint.parent;
            transform.DOMove(spawnPointData.EndPoint.position, _moveDuration).OnComplete(() => IsReady.Value = true);

            Health.Value = MaxHealth.Value;
        }

        public void SetGun(GameObject gun)
        {
            gun.transform.parent = transform;
            gun.transform.position = transform.position;
            _gun = gun.GetComponent<IGun>();
            _gun.Initialize(TeamIndex);
        }

        public void Attack(Vector3 targetPosition)
        {
            _gun.Shoot(targetPosition);
            _animator.SetTrigger(_attack);
        }

        public void Damage(int damage)
        {
            Health.Value = Mathf.Max(0, Health.Value - damage);
        }

        public Vector3 GetPosition() => transform.position;
        public GameObject GetObject() => gameObject;
    }
}