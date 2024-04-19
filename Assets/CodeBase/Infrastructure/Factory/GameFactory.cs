using Assets.CodeBase.Infrastructure.AssetManagement;
using Assets.CodeBase.Infrastructure.Services.PersistenProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;


        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        public event Action HeroCreated;

        public GameFactory(IAssets assets) => 
            _assets = assets;

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroCreated?.Invoke();
            return HeroGameObject;
        }

        public GameObject CreateHud() =>
            InstantiateRegistered(AssetPath.HudPath);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
        private GameObject InstantiateRegistered(string prefabHath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabHath, at: at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(string prefabHath)
        {
            GameObject gameObject = _assets.Instantiate(prefabHath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            

            ProgressReaders.Add(progressReader);
        }

    }
}