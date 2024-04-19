using System;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public Follow Follow;

        public GameObject DeathFx;

        public event Action Happened;

        private EnemyAnimator _animator;
        private Attack _attack;

        private void Awake()
        {
            _animator = GetComponentInChildren<EnemyAnimator>();
            _attack = GetComponentInChildren<Attack>();
        }

        private void Start() => 
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Health.HealthChanged -= HealthChanged;
            Follow.enabled = false;
            _attack.enabled = false;

            _animator.PlayDeath();
            SpawnDeathFx();
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(DeathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}