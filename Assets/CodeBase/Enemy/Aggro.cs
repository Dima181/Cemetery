using System;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public  TriggerObserver TriggerObserver;
        public Follow Follow;

        public float Coolldown;
        private Coroutine _agroCorutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;

                StopAggroCorutine();

                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if(_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _agroCorutine = StartCoroutine(SwithFollowOfAfterCooldown());
            }
        }

        private void StopAggroCorutine()
        {
            if (_agroCorutine != null)
            {
                StopCoroutine(_agroCorutine);
                _agroCorutine = null;
            }
        }


        private IEnumerator SwithFollowOfAfterCooldown()
        {
            yield return new WaitForSeconds(Coolldown);
            SwitchFollowOff();
        }

        private void SwitchFollowOn() => 
            Follow.enabled = true;


        private void SwitchFollowOff() => 
            Follow.enabled = false;
    }
}