using System.IO;
using Zenject;
using UnityEngine;
using System;

namespace SaveLoadCore.UIView
{
    public sealed class SaveForm: IDisposable
    {
        private ViewService _viewService;
        private SaveLoad _saveLoad;
        private LoadPreview _menuInitializeManager;

        [Inject]
        private void Construct(ViewService viewService, SaveLoad saveLoad, LoadPreview saveLoadMenuInitialize)
        {
            _saveLoad = saveLoad;
            _viewService = viewService;
            _menuInitializeManager = saveLoadMenuInitialize;
            _viewService.SaveLoadMenu.SaveFormSaveBtn.onClick.AddListener(ClickSaveButton);
            _viewService.SaveLoadMenu.SaveFormCancelBtn.onClick.AddListener(ClickCancelButton);
        }

        private void ClickSaveButton()
        { 
            if(File.Exists($"{Application.dataPath}/Saves/{_viewService.SaveLoadMenu.SaveFormInputName.text}.sav"))
            {
                _viewService.SaveLoadMenu.AcceptForm.SetActive(true);
                _viewService.SaveLoadMenu.SaveForm.SetActive(false);
            }
            else
            {
                _saveLoad.TrySaveFile(_viewService.SaveLoadMenu.SaveFormInputName.text);
                _menuInitializeManager.PreViewSave();
                _viewService.SaveLoadMenu.SaveFormInputName.text = string.Empty;
                _viewService.SaveLoadMenu.SaveForm.SetActive(false);
            }
        }

        private void ClickCancelButton()
        {
            _viewService.SaveLoadMenu.SaveForm.SetActive(false);
        }
        
        public void Dispose()
        {
            _viewService.SaveLoadMenu.SaveFormSaveBtn.onClick.RemoveListener(ClickSaveButton);
            _viewService.SaveLoadMenu.SaveFormCancelBtn.onClick.RemoveListener(ClickCancelButton);
        }
    }
}
