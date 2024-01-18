using System;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveButton : IDisposable
    {
        private ViewService _viewService;

        [Inject]
        private void Construct(ViewService viewService)
        {
            _viewService = viewService;
            _viewService.SaveLoadMenu.SaveButton.onClick.AddListener(ClickSaveButton);
        }
        
        private void ClickSaveButton()
        {
            _viewService.SaveLoadMenu.SaveForm.SetActive(true);
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.SaveButton.onClick.RemoveListener(ClickSaveButton);
        }
    }  
}

