using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.PersistenProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        
        List<ISavedProgress> ProgressWriters { get; }

        GameObject HeroGameObject { get; }

        event Action HeroCreated;

        GameObject CreateHero(GameObject at);

        GameObject CreateHud();

        void Register(ISavedProgressReader savedProgress);

        void CleanUp();
    }
}