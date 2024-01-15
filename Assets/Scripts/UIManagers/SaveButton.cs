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
            var dataInf = new SaveDataStructInfo();
            var dataImage = new SaveDataStructImage();

            dataInf.SaveName = UnityEngine.Random.Range(0, 1000).ToString();
            dataInf.SaveDate = DateTime.Now;
            dataImage.SaveScreen = _screenCamer.TrySaveCameraView(_camera);

            data.SaveInfo = dataInf;
            data.Screen = dataImage;

            _saveLoad.SaveAll(data);
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.SaveButton.onClick.RemoveListener(clickSaveButton);
        }
    }  
}

