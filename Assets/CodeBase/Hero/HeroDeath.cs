using UnityEngine;

namespace Assets.CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth Health;
        public HeroAttack Attack;
        public HeroMove Move;
        public HeroAnimator Animator;
        public GameObject DeathFX;

        private bool _isDeath;

        private void Start()
        {
            Health.HealthChanged += HealthChanget;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= HealthChanget;
        }

        private void HealthChanget()
        {
            if (DontAlive())
                Die();
        }

        private void Die()
        {
            _isDeath = true;
            Move.enabled = false;
            Attack.enabled = false;
            Animator.PlayDeath();

            Instantiate(DeathFX, transform.position, Quaternion.identity);
        }
        
        private bool DontAlive()
        {
            return !_isDeath && Health.Current <= 0;
        }
    }
}