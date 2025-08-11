using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Runtime
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] private float _moveDuration;

        private int _damage;
        private int _teamIndex;

        public void Initialize(Vector3 targetPosition, int teamIndex, int damage)
        {
            _teamIndex = teamIndex;
            _damage = damage;

            transform.DOMove(targetPosition, _moveDuration).OnComplete((() =>
            {
                if (gameObject) Destroy(gameObject);
            }));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<ITeamMember>(out var member))
            {
                if (member.TeamIndex != _teamIndex)
                {
                    member.Damage(_damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}