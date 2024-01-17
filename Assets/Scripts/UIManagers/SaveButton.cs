using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveButton : IDisposable
    {
        private Camera _camera;
        private ScreenCamera _screenCamer;
        private ViewService _viewService;
        private SaveLoad _saveLoad;
        private List<GameObject> _saveObjectList;

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveComponentsService saveComponentsService, ViewService viewService, SaveLoad saveLoad)
        {
            _screenCamer = screenCamer;
            _camera = saveComponentsService.ScreenCamera;
            _viewService = viewService;
            _saveLoad = saveLoad;
            _viewService.SaveLoadMenu.SaveButton.onClick.AddListener(clickSaveButton);
            _saveObjectList = saveComponentsService.SaveObjectList;
        }
        
        private void clickSaveButton()
        {
            var data = new SaveDataStruct();

            data.SaveName = UnityEngine.Random.Range(0, 1000).ToString();
            data.SaveDate = DateTime.Now;
            data.SaveScreen = _screenCamer.TrySaveCameraView(_camera);
            data.SaveObjects = new List<GameObject>();


            foreach (GameObject components in _saveObjectList)
            {
                for(int i = 0; i < components.transform.childCount; i++)
                {
                    data.SaveObjects.Add(components.transform.GetChild(i).gameObject);
                }
            }

            _saveLoad.Save(data);
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.SaveButton.onClick.RemoveListener(clickSaveButton);
        }
    }  
}

