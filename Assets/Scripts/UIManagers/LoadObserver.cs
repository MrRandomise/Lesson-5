using SaveSystem.Core;
using System;
using TMPro;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class LoadObserver : IDisposable
    {
        private MainFomComponents _mainFomComponents;
        private SceneSaveManager _sceneSaveManager;
        private SaveLoadSelectedItems _selectedItems;
        private TMP_Text _menuButtonText;

        [Inject]
        private void Construct(SceneSaveManager sceneSaveManager, MainFomComponents mainFomComponents, SaveLoadSelectedItems selectedItems, MenuButtonComponents menuButtonService)
        {
            _sceneSaveManager = sceneSaveManager;
            _mainFomComponents = mainFomComponents;
            _mainFomComponents.LoadButton.onClick.AddListener(ClickLoadButton);
            _selectedItems = selectedItems;
            _menuButtonText = menuButtonService.MenuButtonText;
        }

        private void ClickLoadButton()
        {
            var name = _selectedItems.getSelectedItems().HideField.text;
            _sceneSaveManager.LoadScene(name);
            _mainFomComponents.SaveLoadMenu.SetActive(false);
            _menuButtonText.text = "Открыть меню";
        }

        public void Dispose()
        {
            _mainFomComponents.LoadButton.onClick.RemoveListener(ClickLoadButton);
        }
    }
}
