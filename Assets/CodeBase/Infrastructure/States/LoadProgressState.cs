using Assets.CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services.PersistenProgress;
using Assets.CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;

namespace Assets.CodeBase.Infrastructure.States
{
    internal class LoadProgressState : IState
    {
        private const string Main = "Main";
        private readonly GameStateMachine _gameStateMashine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMashine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMashine = gameStateMashine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMashine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel._level);
        }


        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() => 
            _progressService.Progress = 
            _saveLoadService.LoadProgress() 
            ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            /*return new PlayerProgress(initialLevel: Main);*/
            PlayerProgress progress = new PlayerProgress(initialLevel: Main);

            progress.HeroState.MaxHP = 50;
            progress.HeroStats.Damage = 1;
            progress.HeroStats.DamageRadius = 2f;
            progress.HeroState.ResteHP();

            return progress;
        }
    }
}