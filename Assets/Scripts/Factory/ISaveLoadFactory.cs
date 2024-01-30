using UnityEngine;

namespace SaveLoadCore
{
    public interface ISaveLoadFactory
    {
        public GameObject Creat(GameObject prefab, Transform transform);
    }
}