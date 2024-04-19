using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.CodeBase.Infrastructure.AssetManagement
{
    public class AssetsProvider : IAssets
    {
        public GameObject Instantiate(string path) =>
            Object.Instantiate(Resources.Load<GameObject>(path));

        public GameObject Instantiate(string path, Vector3 at) =>
            Object.Instantiate(Resources.Load<GameObject>(path), at, Quaternion.identity);
    }
}