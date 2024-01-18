using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveAcceptForm
    {
        private ViewService _viewService;
        private SaveLoad _saveLoad;

        [Inject]
        private void Construct(ViewService viewService, SaveLoad saveLoad)
        {
            _saveLoad = saveLoad;
            _viewService = viewService;
            _viewService.SaveLoadMenu.AcceptFormYesBtn.onClick.AddListener(ClickSaveButton);
            _viewService.SaveLoadMenu.AcceptFormNoBtn.onClick.AddListener(ClickCancelButton);
        }

        private void ClickSaveButton()
        {
            _saveLoad.SaveData();
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