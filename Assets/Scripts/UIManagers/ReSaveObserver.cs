using System;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class ReSaveObserver: IDisposable
    {
        private MainFomComponents _mainFomComponents;

        [Inject]
        private void Construct(MainFomComponents mainFomComponents)
        {
            _mainFomComponents = mainFomComponents;
            _mainFomComponents.ReSave.onClick.AddListener(ClickReSaveButton);
        }

        private void ClickReSaveButton()
        {
            _mainFomComponents.AcceptForm.SetActive(true);
        }

        public void Dispose()
        {
            _mainFomComponents.ReSave.onClick.RemoveListener(ClickReSaveButton);
        }
    }
}
