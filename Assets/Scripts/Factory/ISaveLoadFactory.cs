using UnityEngine;

namespace SaveLoadCore
{
    public interface ISaveLoadFactory
    {
        public GameObject Creator(GameObject prefab, Transform transform);
    }
}