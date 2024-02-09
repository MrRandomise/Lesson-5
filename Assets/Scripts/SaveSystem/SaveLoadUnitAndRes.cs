using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadCore
{
    public abstract class SaveLoadUnitAndRes<TData>
    {
        public List<GameObject> _saveObjectList;

        private SaveDataStruct GetSaveObjects()
        {
            return data.GetData();
        }



        //public void ClearScene()
        //{
        //    for (int i = 0; i < _saveObjectList.Count; i++)
        //    {
        //        for (int j = 0; j < _saveObjectList[i].transform.childCount; j++)
        //        {
        //            if (!_saveObjectList[i].transform.GetChild(j).GetComponent<TargetSave>().Saved)
        //            {
        //                MonoBehaviour.Destroy(_saveObjectList[i].transform.GetChild(j).gameObject);
        //            }
        //        }
        //    }
        //}
    }
}