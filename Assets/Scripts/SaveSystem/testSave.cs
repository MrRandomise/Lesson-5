using SaveLoadCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class testSave : MonoBehaviour, IInitializable
{
    private ES3Settings _saveSetting;
    private SaveLoadMediators _saveLoadMediators;
    private const string _secretCryptKey = "1234";
    private const string _infoName = "infoName";
    private const string _infoDate = "infoDate";
    private const string _infoScreen = "infoScreen";
    private const string _dataObject = "dataObjects";
    private List<GameObject> sabeobj;
        
    [Inject]
    public void Construct(SaveLoadMediators saveLoadMediators)
    {
        _saveLoadMediators = saveLoadMediators;
        sabeobj = _saveLoadMediators._saveObjectList;
    }

    public void Initialize()
    {
        _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);
    }

    public void SaveBtn()
    {
        Debug.Log("Сохранение");
        var data = _saveLoadMediators.GetLoadStruct("123");

        ES3.Save(_dataObject, sabeobj, data.FileName, _saveSetting);
    }

    public void LoadBtn()
    {
        Debug.Log("Загрузка");
        var filename = $"{Application.dataPath}/Saves/123.sav";
        ES3.Load(_dataObject, filename, new List<GameObject>(), _saveSetting);
    }
}
