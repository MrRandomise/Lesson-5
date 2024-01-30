using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoadFactory : ISaveLoadFactory
    {
        private DiContainer diContainer;

        public SaveLoadFactory(DiContainer container)
        {
            diContainer = container;
        }

        public GameObject Creat(GameObject prefab, Transform transform)
        {
            var obj = diContainer.InstantiatePrefab(prefab, transform);
            return obj;
        }
    }
}