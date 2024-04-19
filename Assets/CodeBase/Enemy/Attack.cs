using Assets.CodeBase.Hero;
using Assets.CodeBase.Infrastructure.Factory;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Logic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator Animator;
        public float AttackCooldown = 3;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;

        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _layerMask = 1 << LayerMask.NameToLayer("Player");
            _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }


        private void OnAttack()
        {
            if(Hit(out Collider hit))
            {
                PhisicsDebug.DrawDebug(StartPoint(), Cleavage, 1f);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnted()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        internal void DisableAttack() => 
            _attackIsActive = false;

        internal void EnableAttack() => 
            _attackIsActive = true;

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            return GetVector() + GetForwardDistance();
        }
        private Vector3 GetVector()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }

        private Vector3 GetForwardDistance()
        {
            return transform.forward * EffectiveDistance;
        }


        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() =>
            _attackCooldown <= 0;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            Animator.PlayAttack();

            _isAttacking = true;
        }


        private void OnHeroCreated() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

    }
}