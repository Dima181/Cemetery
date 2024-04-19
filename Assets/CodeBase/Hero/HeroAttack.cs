using Assets.CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.Input;
using Assets.CodeBase.Infrastructure.Services.PersistenProgress;
using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private const string Hittable = "Hittable";
        public HeroAnimator Animator;
        public CharacterController CharacterController;
        public float EffectiveDistanse = 0.5f;

        private IInputService _input;

        private LayerMask _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _heroStats;

        private void Awake()
        {           
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer(Hittable);
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !Animator.IsAttacking)
                Animator.PlayAttack();
        }

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_heroStats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress progress) => 
            _heroStats = progress.HeroStats;

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint(), _heroStats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, CharacterController.center.y / 2, transform.position.z) + GetEffectiveDistance();

        private Vector3 GetEffectiveDistance() =>
            transform.forward * EffectiveDistanse;

    }
}