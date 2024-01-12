using System;
using TMPro;
using UnityEngine.UI;
using Zenject;
using UnityEngine;

namespace SaveLoadCore.UIView
{
    public sealed class ShowMenu : IDisposable
    {
        private Button _menuButton;
        private TMP_Text _menuButtonText;
        private SaveLoadComponents _saveLoadMenu;
        private SaveLoadMenuInitializeManager _menuInitializeManager;
        private bool isToogle = false;

        [Inject]
        private void Construct(MenuButtonService menuButtonService, ViewService saveLoadMenu, SaveLoadMenuInitializeManager saveMenuManager)
        {
            _menuButton = menuButtonService.MenuButton;
            _menuButtonText = menuButtonService.MenuButtonText;
            _saveLoadMenu = saveLoadMenu.SaveLoadMenu;
            _menuInitializeManager = saveMenuManager;
            ClickButton();
        }

        private void ClickButton()
        {
            _menuButton.onClick.AddListener(ShowCloseMenu);
        }

        private void ShowCloseMenu()
        {
            if (!isToogle)
            {
                isToogle = true;
                _saveLoadMenu.gameObject.SetActive(true);
                _menuInitializeManager.PreViewSave();
                _menuButtonText.text = "Закрыть меню";
            }
            else
            {
                isToogle = false;
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

