using UnityEngine;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveLoadSelectedItems
    {
        private GameObject _saveContainer;

        [Inject]
        private void Construct(MainFomComponents mainFomComponents)
        {
            _saveContainer = mainFomComponents.SaveLoadContainer;
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
