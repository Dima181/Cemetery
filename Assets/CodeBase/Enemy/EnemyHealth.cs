using Assets.CodeBase.Logic;
using System;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        private EnemyAnimator _animator;

        [SerializeField]
        private float _current;

        [SerializeField]
        private float _max;

        public float Current { get => _current; set => _current = value; }
        public float Max { get => _max; set => _max = value; }

        public event Action HealthChanged;

        private void Awake() => 
            _animator = GetComponentInChildren<EnemyAnimator>();

        public void TakeDamage(float damage)
        {
            Current -= damage;

            _animator.PlayHit();

            HealthChanged?.Invoke();
        }
    }
}