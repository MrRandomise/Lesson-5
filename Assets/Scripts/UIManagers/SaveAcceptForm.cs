using System;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveAcceptForm: IDisposable
    {
        private ViewService _viewService;
        private SaveLoad _saveLoad;
        private SaveLoadSelectedItems _selectedItems;
        private LoadPreview _menuInitializeManager;

        [Inject]
        private void Construct(ViewService viewService, SaveLoad saveLoad, SaveLoadSelectedItems saveLoadSelectedItems, LoadPreview saveLoadMenuInitializeManager)
        {
            _saveLoad = saveLoad;
            _viewService = viewService;
            _selectedItems = saveLoadSelectedItems;
            _menuInitializeManager = saveLoadMenuInitializeManager;
            _viewService.SaveLoadMenu.AcceptFormYesBtn.onClick.AddListener(ClickSaveButton);
            _viewService.SaveLoadMenu.AcceptFormNoBtn.onClick.AddListener(ClickCancelButton);
        }

        private void ClickSaveButton()
        {
            string name;
            if(String.IsNullOrEmpty(_viewService.SaveLoadMenu.SaveFormInputName.text))
            {
                name = _selectedItems.getSelectedItems().SaveName.text;
            }
            else
            {
                name = _viewService.SaveLoadMenu.SaveFormInputName.text;
                _viewService.SaveLoadMenu.SaveFormInputName.text = string.Empty;
            }
            _saveLoad.TrySaveFile(name);
            _menuInitializeManager.PreViewSave();
            _viewService.SaveLoadMenu.AcceptForm.SetActive(false);
        }

        private void ClickCancelButton()
        {
            _viewService.SaveLoadMenu.SaveForm.SetActive(false);
            _viewService.SaveLoadMenu.AcceptForm.SetActive(false);
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.AcceptFormYesBtn.onClick.RemoveListener(ClickSaveButton);
            _viewService.SaveLoadMenu.AcceptFormNoBtn.onClick.RemoveListener(ClickCancelButton);
        }
    }
}