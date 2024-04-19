using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    /*[RequireComponent(typeof(EnemyAnimator))]*/
    internal class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalDistanse = 1f;
        private const float MinimalVelocity = 0.1f;

        public NavMeshAgent Agent;
        public Follow Follow;

        private EnemyAnimator _animator;

        private void Awake() =>
             _animator = GetComponentInChildren<EnemyAnimator>();

        private void Update()
        {
            if (ShouldMove())
                _animator.PlayIsMoving();
            else
                _animator.StopIsMoving();
        }

        private bool ShouldMove() => 
            Agent.remainingDistance > MinimalDistanse && Agent.velocity.magnitude > MinimalVelocity;
    }
}
