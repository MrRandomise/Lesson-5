using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class ShowMenu : IDisposable
    {

        private Button _menuButton;
        private TMP_Text _menuButtonText;
        private GameObject _saveLoadMenu;
        private LoadPreview _menuInitializeManager;

        [Inject]
        private void Construct(MenuButtonComponents menuButtonService, MainFomComponents mainFomComponents, LoadPreview saveMenuManager)
        {
            _menuButton = menuButtonService.MenuButton;
            _menuButtonText = menuButtonService.MenuButtonText;
            _saveLoadMenu = mainFomComponents.SaveLoadMenu;
            _menuInitializeManager = saveMenuManager;
            ClickButton();
        }

        private void ClickButton()
        {
            _menuButton.onClick.AddListener(ShowCloseMenu);
        }

        private void ShowCloseMenu()
        {
            if (!_saveLoadMenu.gameObject.activeSelf)
            {
                _saveLoadMenu.gameObject.SetActive(true);
                _menuInitializeManager.PreViewSave();
                _menuButtonText.text = "Закрыть меню";
            }
            else
            {
                _saveLoadMenu.gameObject.SetActive(false);
                _menuButtonText.text = "Открыть меню";
            }
        }

        public void Dispose()
        {
            _menuButton.onClick.RemoveListener(ShowCloseMenu);
        }
    }
}

