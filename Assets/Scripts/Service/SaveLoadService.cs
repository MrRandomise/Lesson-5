using GameEngine;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public class SaveLoadService : ISaveLoadService
    {
        private List<GameObject> _saveLoadObjects;

        [Inject]
        private void construct(List<GameObject> saveLoadObjects)
        {
            _saveLoadObjects = saveLoadObjects;
        }

        public SaveDataStruct GetData()
        {
            var data = new SaveDataStruct();
            for (int i = 0; i < _saveLoadObjects.Count; i++)
            {
                for (int j = 0; j < _saveLoadObjects[i].transform.childCount; j++)
                {
                    data.Objects.Add(_saveLoadObjects[i].transform.GetChild(j).gameObject);
                    if (_saveLoadObjects[i].transform.GetChild(j).TryGetComponent(out Unit unit))
                    {
                        data.ObjectsValue.Add(unit);
                    }
                    else if(_saveLoadObjects[i].transform.GetChild(j).TryGetComponent(out Resource res))
                    {
                        data.ObjectsValue.Add(res);
                    }
                }
            }
            return data;
        }
    }
}
