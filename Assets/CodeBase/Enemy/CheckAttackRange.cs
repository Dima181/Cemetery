using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        private Attack _attack;

        private void Awake() =>
             _attack = GetComponentInChildren<Attack>();

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            _attack.DisableAttack();
        }


        private void TriggerEnter(Collider obj)
        {
            _attack.EnableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            _attack.DisableAttack();
        }
    }
}