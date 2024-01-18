using System.IO;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveForm 
    {
        private ViewService _viewService;
        private SaveLoad _saveLoad;

        [Inject]
        private void Construct(ViewService viewService, SaveLoad saveLoad)
        {
            _saveLoad = saveLoad;
            _viewService = viewService;
            _viewService.SaveLoadMenu.SaveFormSaveBtn.onClick.AddListener(ClickSaveButton);
            _viewService.SaveLoadMenu.SaveFormCancelBtn.onClick.AddListener(ClickCancelButton);
        }

        private void ClickSaveButton()
        { 
            if(File.Exists(_viewService.SaveLoadMenu.SaveFormInputName.text))
            {
                _viewService.SaveLoadMenu.AcceptForm.SetActive(true);
            }
            else
            {
                _saveLoad.SaveData();
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
