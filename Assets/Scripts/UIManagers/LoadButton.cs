using System;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class LoadButton : IDisposable
    {
        private ViewService _viewService;
        private SaveLoad _load;
        private SaveLoadSelectedItems _selectedItems;

        [Inject]
        private void Construct(ViewService viewService, SaveLoadSelectedItems selectedItems, SaveLoad saveLoad)
        {
            _viewService = viewService;
            _viewService.SaveLoadMenu.LoadButton.onClick.AddListener(ClickLoadButton);
            _selectedItems = selectedItems;
            _load = saveLoad;
        }

        private void ClickLoadButton()
        {
            var name = _selectedItems.getSelectedItems().HideField.text;
            _load.LoadData(name);
            _viewService.SaveLoadMenu.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.LoadButton.onClick.RemoveListener(ClickLoadButton);
        }
    }
}
