using System;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveFormObserver : IDisposable
    {
        private MainFomComponents _mainFomComponents;

        [Inject]
        private void Construct(MainFomComponents viewService)
        {
            _mainFomComponents = viewService;
            _mainFomComponents.SaveButton.onClick.AddListener(ClickSaveButton);
        }
        
        private void ClickSaveButton()
        {
            _mainFomComponents.SaveForm.SetActive(true);
        }

        public void Dispose()
        {
            _mainFomComponents.SaveButton.onClick.RemoveListener(ClickSaveButton);
        }
    }  
}

