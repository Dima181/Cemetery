using Assets.CodeBase.Logic;
using System;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack1");
        private readonly int _walkStateHash = Animator.StringToHash("Walk");
        private readonly int _deathStateHash = Animator.StringToHash("Death");
        private readonly int _hitStateHash = Animator.StringToHash("TakeDamage.002");

        private Animator _animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        
        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => 
            StateExited?.Invoke(State);


        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayDeath() => 
            _animator.SetTrigger(Die);

        public void PlayHit() =>
            _animator.SetTrigger(Hit);

        public void PlayAttack() => 
            _animator.SetTrigger(Attack);

        public void PlayIsMoving() => 
            _animator.SetBool(IsMoving, true);

        public void StopIsMoving() => 
            _animator.SetBool(IsMoving, false);


        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else if (stateHash == _hitStateHash)
                state = AnimatorState.Hit;
            else
                state = AnimatorState.Unknown;

            return state;
        }

    }
}
