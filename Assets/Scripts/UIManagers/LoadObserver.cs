using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class LoadObserver
    {
        //private MainFomComponents _mainFomComponents;
        //private SaveLoad _load;
        //private SaveLoadSelectedItems _selectedItems;

        //[Inject]
        //private void Construct(MainFomComponents mainFomComponents, SaveLoadSelectedItems selectedItems, SaveLoad saveLoad)
        //{
        //    _mainFomComponents = mainFomComponents;
        //    _mainFomComponents.LoadButton.onClick.AddListener(ClickLoadButton);
        //    _selectedItems = selectedItems;
        //    _load = saveLoad;
        //}

        //private void ClickLoadButton()
        //{
        //    var name = _selectedItems.getSelectedItems().HideField.text;
        //    //_load.TryLoadGameObject(name);
        //    _mainFomComponents.SaveLoadMenu.SetActive(false);
        //}

        //public void Dispose()
        //{
        //    _mainFomComponents.LoadButton.onClick.RemoveListener(ClickLoadButton);
        //}
    }
}
