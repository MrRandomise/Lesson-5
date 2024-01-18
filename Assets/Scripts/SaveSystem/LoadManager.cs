using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class LoadManager
    {
        private List<GameObject> _loadObjectListName;
        private List<string> _objectListName = new List<string>();
        private SaveLoadFactory _loadFactory;
        private SaveComponentsService _saveComponentsService;

        [Inject]
        private void Construct(SaveComponentsService saveComponentsService, SaveLoadFactory saveLoadFactory)
        {
            _loadObjectListName = saveComponentsService.SaveObjectList;
            _saveComponentsService = saveComponentsService;
            _loadFactory = saveLoadFactory;
            GetNameObjectList();
        }

        private void GetNameObjectList()
        {
            foreach (GameObject item in _loadObjectListName)
            {
                _objectListName.Add(item.name);
            }
        }

        private void ClearScene()
        {
            foreach (GameObject item in _loadObjectListName)
            {
                Object.Destroy(item.gameObject);
            }
        }

        public void LoadNewObjectToScene(List<List<GameObject>> items)
        {
            ClearScene();
            for (int i = 0; i < items.Count; i++) 
            {
                var parent = _loadFactory.Creator(_saveComponentsService.ObjectContainerPrefab, _saveComponentsService.ObjectContainer.transform);
                parent.name = _objectListName[i];
                for (int j = 0; j < items[i].Count; j++) 
                {
                    var child = _loadFactory.Creator(items[i][j], parent.transform);
                    child.transform.rotation = items[i][j].transform.rotation;
                }
            }
        }
    }
}
