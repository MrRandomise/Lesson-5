using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveSystem.Core;
using UnityEngine;

namespace SaveSystem.Data
{
    public class SaveFileStorage : ISaveState
    {
        private readonly ScreenCamera _screenCamer;

        private SaveFileStorage(ScreenCamera screenCamer)
        {
            _screenCamer = screenCamer;
        }
        
        public Dictionary<string, string> CaptureState(string name)
        {
            var state = new Dictionary<string, string>
            {
                {"Id", UnityEngine.Random.Range(1000000, 9999999999).ToString()},
                {"Name", name},
                {"Sceen", _screenCamer.TrySaveCameraView().ToString()}
            };
            return state;
        }

        public void RestoreState(List<Dictionary<string, string>> loadedData)
        {
            //DestroyAllUnits();
            //InitUnitsList(loadedData);

            //var unitsList = new List<Unit>();
            //foreach (var state in _units)
            //{
            //    var unitInstance = Object.Instantiate(_prefabStorage.GetUnitByName(state["Type"]), _unitsRoot, true);
            //    unitInstance.transform.position = new Vector3(float.Parse(state["PositionX"]),
            //        float.Parse(state["PositionY"]), float.Parse(state["PositionZ"]));
            //    unitInstance.transform.eulerAngles = new Vector3(float.Parse(state["RotationX"]),
            //        float.Parse(state["RotationY"]), float.Parse(state["RotationZ"]));
            //    var unit = unitInstance.GetComponent<Unit>();
            //    unit.HitPoints = Convert.ToInt32(state["HP"]);
            //    unitsList.Add(unit);
            //}
            //_unitsManager.SetupUnits(unitsList);
        }
    }
}
