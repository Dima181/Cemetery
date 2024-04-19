using Assets.CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services.PersistenProgress;
using Assets.CodeBase.StaticData;
using System;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MinsterTypeId MinsterTypeId;
        private string _id;

        public bool Slain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        void ISavedProgressReader.LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                Slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {

        }

        void ISavedProgress.UpdateProgress(PlayerProgress progress)
        {
            if (Slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}