using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class LoadButton : IDisposable
    {
        private ViewService _viewService;
        private SaveLoad _load;
        private List<GameObject> _loadObjectList;
        private SelectedItems _selectedItems;
        private SaveLoadContent _saveLoadContent;

        [Inject]
        private void Construct(SaveComponentsService saveComponentsService, ViewService viewService, SaveLoad saveLoad, SelectedItems selectedItems)
        {
            _viewService = viewService;
            _load = saveLoad;
            _loadObjectList = saveComponentsService.SaveObjectList;
            _selectedItems = selectedItems;

            _viewService.SaveLoadMenu.LoadButton.onClick.AddListener(ClickLoadButton);
        }

        private void ClickLoadButton()
        {
            _saveLoadContent = _selectedItems.getSelectedItems();
            var savefile = _saveLoadContent.HideField.text;
            _viewService.SaveLoadMenu.gameObject.SetActive(false);
            _load.LoadFile(savefile, out var data);

            for (int i = 0; i < data.SaveObjects.Count; i++)
            { 
                for (int j = 0; j < data.SaveObjects[i].Count; j++)
                {
                    //_loadObjectList[i].transform.GetChild(j).transform = data.SaveObjects[i][j].transform;
                }
            }
        }

        public void Dispose()
        {
            _viewService.SaveLoadMenu.LoadButton.onClick.RemoveListener(ClickLoadButton);
        }
    }
}
