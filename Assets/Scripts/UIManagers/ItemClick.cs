using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class ItemClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private SaveLoadContent _field;
        private static GameObject _selectedItem = null;
        private ViewService _viewService;

        [Inject]
        private void construct(ViewService viewService)
        {
            _viewService = viewService;
        }

        private void OnDisable()
        {
            if (_selectedItem != null)
            {
                _selectedItem.SetActive(false);
            }
            if (_viewService.SaveLoadMenu.ReSave.interactable)
            {
                _viewService.SaveLoadMenu.ReSave.interactable = false;
            }
            if (_viewService.SaveLoadMenu.LoadButton.interactable)
            {
                _viewService.SaveLoadMenu.LoadButton.interactable = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_selectedItem != null)
            {
                _selectedItem.SetActive(false);
            }
            _selectedItem = _field.SelectedItem;
            _selectedItem.SetActive(true);
            if(!_viewService.SaveLoadMenu.ReSave.interactable)
            {
                _viewService.SaveLoadMenu.ReSave.interactable = true;
            }
            if (!_viewService.SaveLoadMenu.LoadButton.interactable)
            {
                _viewService.SaveLoadMenu.LoadButton.interactable = true;
            }
        }
    }
}
