using Assets.CodeBase.Infrastructure.Factory;
using Assets.CodeBase.Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.CodeBase.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1;

        public NavMeshAgent Agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += HeroCreated;
        }

        private void Update()
        {
            if (Initialized() && HeroNotReached())
                Agent.destination = _heroTransform.position;
        }


        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private void HeroCreated() => 
            InitializeHeroTransform();

        private bool Initialized() =>
            _gameFactory.HeroGameObject != null;

        private bool HeroNotReached() =>
            Vector3.Distance(Agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}