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
        }

        //public void LoadNewObjectToScene(List<GameObject> items)
        //{
        //    for (int i = 0; i < items.Count; i++) 
        //    {
        //        var parent = _loadFactory.Creator(_saveComponentsService.ObjectContainerPrefab, _saveComponentsService.ObjectContainer.transform);
        //        parent.name = _objectListName[i];
        //        _saveComponentsService.SaveObjectList.Add(parent);
        //        for (int j = 0; j < items[i].Count; j++) 
        //        {
        //            var child = _loadFactory.Creator(items[i][j], parent.transform);
        //            child.transform.rotation = items[i][j].transform.rotation;
        //        }
        //    }
        //    _loadObjectListName = _saveComponentsService.SaveObjectList;
        //}
    }
}
