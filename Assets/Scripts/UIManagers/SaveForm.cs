using System.IO;
using Zenject;
using UnityEngine;
using System;
using SaveSystem.Core;

namespace SaveLoadCore.UIView
{
    public sealed class SaveForm : IDisposable
    {
        //private SaveLoadInfo _saveLoad;
        private SaveFormComponents _saveFormComponents;
        private LoadPreview _menuInitializeManager;
        private MainFomComponents _mainFomComponents;
        private SceneSaveManager _sceneSaveManager;

        [Inject]
        private void Construct(MainFomComponents mainFomComponents, SaveFormComponents saveFormComponents, LoadPreview saveLoadMenuInitialize, SceneSaveManager sceneSaveManager)
        {
            //_saveLoad = saveLoad;
            _mainFomComponents = mainFomComponents;
            _saveFormComponents = saveFormComponents;
            _menuInitializeManager = saveLoadMenuInitialize;
            _sceneSaveManager = sceneSaveManager;
            _saveFormComponents.SaveFormSaveBtn.onClick.AddListener(ClickSaveButton);
            _saveFormComponents.SaveFormCancelBtn.onClick.AddListener(ClickCancelButton);
        }

        private void ClickSaveButton()
        {
            if (File.Exists($"{Application.dataPath}/Saves/{_saveFormComponents.SaveFormInputName.text}.sav"))
            {
                _mainFomComponents.AcceptForm.SetActive(true);
                _mainFomComponents.SaveForm.SetActive(false);
            }
            else
            {
                _sceneSaveManager.SaveScene(_saveFormComponents.SaveFormInputName.text);
                //_saveLoad.SaveInfo(_saveFormComponents.SaveFormInputName.text);
                _menuInitializeManager.PreViewSave();
                _saveFormComponents.SaveFormInputName.text = string.Empty;
                _mainFomComponents.SaveForm.SetActive(false);
            }
        }

        private void ClickCancelButton()
        {
            _mainFomComponents.SaveForm.SetActive(false);
        }

        public void Dispose()
        {
            _saveFormComponents.SaveFormSaveBtn.onClick.RemoveListener(ClickSaveButton);
            _saveFormComponents.SaveFormCancelBtn.onClick.RemoveListener(ClickCancelButton);
        }
    }
}
