using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveLoadItemClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private SaveLoadContent _field;
        private static GameObject _selectedItem = null;
        private MainFomComponents _mainFromComponents;

        [Inject]
        private void construct(MainFomComponents mainFomComponents)
        {
            _mainFromComponents = mainFomComponents;
        }

        private void OnDisable()
        {
            if (_selectedItem != null)
            {
                _selectedItem.SetActive(false);
            }
            if (_mainFromComponents.ReSave.interactable)
            {
                _mainFromComponents.ReSave.interactable = false;
            }
            if (_mainFromComponents.LoadButton.interactable)
            {
                _mainFromComponents.LoadButton.interactable = false;
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

            if(!_mainFromComponents.ReSave.interactable)
            {
                _mainFromComponents.ReSave.interactable = true;
            }
            if (!_mainFromComponents.LoadButton.interactable)
            {
                _mainFromComponents.LoadButton.interactable = true;
            }
        }
    }
}
