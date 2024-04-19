using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadServices;

        public BoxCollider Collider;

        private void Awake()
        {
            _saveLoadServices = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadServices.SaveProgress();

            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if(!Collider)
                return;

            Gizmos.color = new Color32(30, 200, 30, 110);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}
