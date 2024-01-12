using System;
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

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveComponentsService saveComponentsService, ViewService viewService, SaveLoad saveLoad)
        {
            _screenCamer = screenCamer;
            _camera = saveComponentsService.ScreenCamera;
            _viewService = viewService;
            _saveLoad = saveLoad;
            _viewService.SaveLoadMenu.SaveButton.onClick.AddListener(clickSaveButton);
            
        }
        
        private void clickSaveButton()
        {
            var data = new SaveDataStruct();
            data.SaveName = UnityEngine.Random.Range(0, 1000).ToString();
            data.SaveDate = DateTime.Now;
            data.SaveScreen = _screenCamer.TrySaveCameraView(_camera);
            _saveLoad.Save(data);
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.SaveButton.onClick.RemoveListener(clickSaveButton);
        }
    }  
}

