using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadCore
{
    [Serializable]
    public class SaveLoadMediators
    {
        public List<GameObject> _saveObjectList;
        private ScreenCamera _screenCamer = new ScreenCamera();

        public SaveDataStruct GetLoadStruct(string name)
        {
            var data = new SaveDataStruct();
            data.FileName = $"{Application.dataPath}/Saves/{name}.sav";
            data.SaveScreen = _screenCamer.TrySaveCameraView();
            data.SaveName = name;
            data.SaveDate = DateTime.Now;
            data.SaveObjects = GetSaveObjects();

            return data;
        }

        private List<GameObject> GetSaveObjects()
        {
            var list = new List<GameObject>();
            for(int i = 0; i < _saveObjectList.Count; i++)
            {
                for(int j = 0; j < _saveObjectList[i].transform.childCount; j++)
                {
                    _saveObjectList[i].transform.GetChild(j).GetComponent<TargetSave>().Saved = true;
                    list.Add(_saveObjectList[i].transform.GetChild(j).gameObject);
                }
            }
            return list;
        }

        public void ClearScene()
        {
            for (int i = 0; i < _saveObjectList.Count; i++)
            {
                for (int j = 0; j < _saveObjectList[i].transform.childCount; j++)
                {
                    if (!_saveObjectList[i].transform.GetChild(j).GetComponent<TargetSave>().Saved)
                    {
                        MonoBehaviour.Destroy(_saveObjectList[i].transform.GetChild(j).gameObject);
                    }
                }
            }
        }
    }
}