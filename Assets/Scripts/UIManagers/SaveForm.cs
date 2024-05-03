using System.IO;
using System;
using SaveSystem.Core;
using Zenject;
using SaveLoadCore.Tools;

namespace SaveLoadCore.UIView
{
    public sealed class SaveForm : IDisposable
    {
        private SaveFormComponents _saveFormComponents;
        private LoadPreview _menuInitializeManager;
        private MainFomComponents _mainFomComponents;
        private SceneSaveManager _sceneSaveManager;
        private FileNameManager _fileNameManager;

        [Inject]
        private void Construct(MainFomComponents mainFomComponents, SaveFormComponents saveFormComponents, LoadPreview saveLoadMenuInitialize, SceneSaveManager sceneSaveManager, FileNameManager fileNameManager)
        {
            _mainFomComponents = mainFomComponents;
            _saveFormComponents = saveFormComponents;
            _menuInitializeManager = saveLoadMenuInitialize;
            _sceneSaveManager = sceneSaveManager;
            _fileNameManager = fileNameManager;
            _saveFormComponents.SaveFormSaveBtn.onClick.AddListener(ClickSaveButton);
            _saveFormComponents.SaveFormCancelBtn.onClick.AddListener(ClickCancelButton);
        }

        private void ClickSaveButton()
        {
            if (File.Exists(_fileNameManager.GetSaveFile(_saveFormComponents.SaveFormInputName.text)))
            {
                _mainFomComponents.AcceptForm.SetActive(true);
                _mainFomComponents.SaveForm.SetActive(false);
            }
            else
            {
                _sceneSaveManager.SaveScene(_saveFormComponents.SaveFormInputName.text);
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
