using System;
using Zenject;

namespace SaveLoadCore.UIView
{
    public sealed class SaveAcceptForm
    {
        //IDisposable
        //private SaveLoadInfo _saveLoad;
        //private SaveFormComponents _saveFormComponents;
        //private AcceptFormComponents _acceptFormComponents;
        //private MainFomComponents _mainFomComponents;
        //private SaveLoadSelectedItems _selectedItems;
        //private LoadPreview _menuInitializeManager;

        //[Inject]
        //private void Construct(SaveLoadInfo saveLoad, MainFomComponents mainFomComponents, AcceptFormComponents acceptFormComponents, SaveFormComponents saveFormComponents, SaveLoadSelectedItems saveLoadSelectedItems, LoadPreview saveLoadMenuInitializeManager)
        //{
        //    _saveLoad = saveLoad;
        //    _saveFormComponents = saveFormComponents;
        //    _acceptFormComponents = acceptFormComponents;
        //    _mainFomComponents = mainFomComponents;
        //    _selectedItems = saveLoadSelectedItems;
        //    _menuInitializeManager = saveLoadMenuInitializeManager;
        //    _acceptFormComponents.AcceptFormYesBtn.onClick.AddListener(ClickSaveButton);
        //    _acceptFormComponents.AcceptFormNoBtn.onClick.AddListener(ClickCancelButton);
        //}

        //private void ClickSaveButton()
        //{
        //    string name;
        //    if(String.IsNullOrEmpty(_saveFormComponents.SaveFormInputName.text))
        //    {
        //        name = _selectedItems.getSelectedItems().SaveName.text;
        //    }
        //    else
        //    {
        //        name = _saveFormComponents.SaveFormInputName.text;
        //        _saveFormComponents.SaveFormInputName.text = string.Empty;
        //    }
        //    _saveLoad.SaveInfo(name);
        //    _menuInitializeManager.PreViewSave();
        //    _mainFomComponents.AcceptForm.SetActive(false);
        //}

        //private void ClickCancelButton()
        //{
        //    _mainFomComponents.SaveForm.SetActive(false);
        //    _mainFomComponents.AcceptForm.SetActive(false);
        //}

        //public void Dispose()
        //{
        //    _acceptFormComponents.AcceptFormYesBtn.onClick.RemoveListener(ClickSaveButton);
        //    _acceptFormComponents.AcceptFormNoBtn.onClick.RemoveListener(ClickCancelButton);
        //}
    }
}