using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.IO;
using UnityEngine.UIElements;
using System.ComponentModel;

namespace SaveLoadCore.UIView
{
    public sealed class SaveLoadMenuInitializeManager
    {
        private GameObject _saveContainer;
        private List<GameObject> _contetList = new List<GameObject>();
        private GameObject _saveLoadItem;
        private DiContainer _diContainer;
        private SaveLoad _saveLoad;
        private ScreenCamera _screenCamera;

        [Inject]
        private void Construct(ViewService viewService, DiContainer diContainer, SaveLoad saveLoad, ScreenCamera screenCamera)
        {
            _saveContainer = viewService.SaveLoadMenu.SaveLoadContainer;
            _saveLoadItem = viewService.SaveLoadItemPrefab;
            _diContainer = diContainer;
            _saveLoad = saveLoad;
            _screenCamera = screenCamera;
            ClearSaveContainer();
        }

        public void ClearSaveContainer()
        {
            _contetList.Clear();
            for (int i = 0; i < _saveContainer.transform.GetChild(0).childCount; i++)
            {
                _contetList.Add(_saveContainer.transform.GetChild(0).GetChild(i).gameObject);
                _saveContainer.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }

        public void PreViewSave()
        {
            ClearSaveContainer();
            var Files = Directory.GetFiles(Application.dataPath + "/Saves", "*", SearchOption.AllDirectories);
            AddNewLoadItem(Files);
            for(int i = 0; i < _contetList.Count; i++)
            {
                var item = _contetList[i].GetComponent<SaveLoadContent>();
                var data = _saveLoad.Load(Files[i]);
                _screenCamera.TryLoadCameraView(data.SaveScreen, out var screen);
                item.Screen.sprite = screen;
                item.SaveName.text = data.SaveName;
            }
        }

        private void AddNewLoadItem(string[] files)
        {
            
            if (files.Length > _contetList.Count)
            {
                var newCount = files.Length - _contetList.Count;
                for (int i = 0; i < newCount; i++)
                {
                    var item = _diContainer.InstantiatePrefab(_saveLoadItem, _saveLoadItem.transform);
                    item.transform.SetParent(_saveContainer.transform);
                    _contetList.Add(item);
                }
            }
        }
    }
}

