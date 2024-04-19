using Assets.CodeBase.Infrastructure.Factory;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.States;
using Assets.CodeBase.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab), AllServices.Container);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}