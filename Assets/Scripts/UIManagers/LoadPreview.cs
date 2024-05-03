using System.Collections.Generic;
using UnityEngine;
using SaveSystem.Data;
using SaveSystem.Core;
using Zenject;
using SaveLoadCore.Tools;

namespace SaveLoadCore.UIView
{
    public sealed class LoadPreview
    {
        private ISaveLoadFactory _saveLoadFactory;
        private GameObject _saveContainer;
        private GameObject _saveLoadItem;
        private ScreenCamera _screenCamera;
        private SaveLoadInfoManager _saveLoadInfoManager;
        private List<GameObject> _contetList = new List<GameObject>();
        private FileNameManager _fileNameManager;

        [Inject]
        private void Construct(SaveLoadInfoManager saveLoadInfoManager, MainFomComponents mainFomComponents, ISaveLoadFactory saveLoadFactory, ScreenCamera screenCamera, FileNameManager fileNameManager)
        {
            _saveLoadInfoManager = saveLoadInfoManager;
            _saveContainer = mainFomComponents.SaveLoadContainer;
            _saveLoadItem = mainFomComponents.SaveLoadItemPrefab;
            _saveLoadFactory = saveLoadFactory;
            _screenCamera = screenCamera;
            _fileNameManager = fileNameManager;
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

            var files = _fileNameManager.GetSaveFiles();
            ClearSaveContainer();
            AddNewLoadItem(files);

            for (int i = 0; i < files.Length; i++)
            {
                _contetList[i].gameObject.SetActive(true);
                
                var item = _contetList[i].GetComponent<SaveLoadContent>();
                var name = _fileNameManager.GetSaveName(files[i]);

                var data = _saveLoadInfoManager.LoadInfo(name);
                if (data.SaveName != item.SaveName.text || data.SaveDate.ToString() != item.SaveDate.text)
                {
                    _screenCamera.TryLoadCameraView(data.SaveScreen, out var screen);
                    item.Screen.sprite = screen;
                    item.SaveName.text = data.SaveName;
                    item.SaveDate.text = data.SaveDate.ToString();
                    item.HideField.text = name;
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
                    var item = _saveLoadFactory.Creat(_saveLoadItem, _saveContainer.transform);
                    _contetList.Add(item);
                }
            }
        }
    }
}

