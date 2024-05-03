using UnityEngine;

namespace SaveSystem.Data
{
    public class UnitsPrefabStorage : MonoBehaviour
    {
        [SerializeField] private GameObject[] unitPrefabs;

        public GameObject GetUnitByName(string prefabName)
        {
            foreach (var prefab in unitPrefabs)
            {
                if (prefab.name == prefabName)
                {
                    return prefab;
                }
            }
            
            throw new System.Exception($"No unit was found by name: {prefabName}");
        }
    }
}
