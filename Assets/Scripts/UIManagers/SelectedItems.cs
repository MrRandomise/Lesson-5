using UnityEngine;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SelectedItems
    {
        private GameObject _saveContainer;

        [Inject]
        private void Construct(ViewService viewService)
        {
            _saveContainer = viewService.SaveLoadMenu.SaveLoadContainer;
        }

        public SaveLoadContent getSelectedItems()
        {
            for(int i = 0; i < _saveContainer.transform.childCount; i++)
            {
                if (_saveContainer.transform.GetChild(i).GetComponent<SaveLoadContent>().SelectedItem.activeSelf)
                {
                    return _saveContainer.transform.GetChild(i).GetComponent<SaveLoadContent>();
                }
            }
            return null;
        }
    }
}
