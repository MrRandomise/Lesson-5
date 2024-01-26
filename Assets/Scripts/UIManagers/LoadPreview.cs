using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.IO;

namespace SaveLoadCore.UIView
{
    public sealed class LoadPreview
    {
        private GameObject _saveContainer;
        private List<GameObject> _contetList = new List<GameObject>();
        private GameObject _saveLoadItem;
        private SaveLoad _saveLoad;
        private ScreenCamera _screenCamera;
        private SaveLoadFactory _saveLoadFactory;

        [Inject]
        private void Construct(ViewService viewService, SaveLoad saveLoad, ScreenCamera screenCamera, SaveLoadFactory saveLoadFactory)
        {
            _saveContainer = viewService.SaveLoadMenu.SaveLoadContainer;
            _saveLoadItem = viewService.SaveLoadItemPrefab;
            _saveLoad = saveLoad;
            _screenCamera = screenCamera;
            _saveLoadFactory = saveLoadFactory;
        }

        public void ClearSaveContainer()
        {
            _contetList.Clear();
            for (int i = 0; i < _saveContainer.transform.childCount; i++)
            {
                _contetList.Add(_saveContainer.transform.GetChild(i).gameObject);
                _saveContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void PreViewSave()
        {
            var Files = Directory.GetFiles(Application.dataPath + "/Saves/", "*.sav", SearchOption.AllDirectories);

            ClearSaveContainer();
            AddNewLoadItem(Files);

            for (int i = 0; i < Files.Length; i++)
            {
                _contetList[i].gameObject.SetActive(true);
                var item = _contetList[i].GetComponent<SaveLoadContent>();
                _saveLoad.TryLoadInfo(Files[i], out var data);
                if(data.SaveName != item.SaveName.text || data.SaveDate.ToString() != item.SaveDate.text) 
                {
                    _screenCamera.TryLoadCameraView(data.SaveScreen, out var screen);
                    item.Screen.sprite = screen;
                    item.SaveName.text = data.SaveName;
                    item.SaveDate.text = data.SaveDate.ToString();
                    item.HideField.text = Files[i];
                }
            }
        }

        private void AddNewLoadItem(string[] files)
        {
            if (files.Length > _contetList.Count)
            {
                var newCount = files.Length - _contetList.Count;
                for (int i = 0; i < newCount; i++)
                {
                    var item = _saveLoadFactory.Creator(_saveLoadItem, _saveContainer.transform);
                    _contetList.Add(item);
                }
            }
        }
    }
}

