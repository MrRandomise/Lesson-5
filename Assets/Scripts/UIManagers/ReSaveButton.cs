using System;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class ReSaveButton: IDisposable
    {
        private ViewService _viewService;

        [Inject]
        private void Construct(ViewService viewService)
        {
            _viewService = viewService;
            _viewService.SaveLoadMenu.ReSave.onClick.AddListener(ClickReSaveButton);
        }

        private void ClickReSaveButton()
        {
            _viewService.SaveLoadMenu.AcceptForm.SetActive(true);
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.ReSave.onClick.RemoveListener(ClickReSaveButton);
        }
    }
}
