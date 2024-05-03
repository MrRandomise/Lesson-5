using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveSystem.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace SaveSystem.Data
{
    public class UnitSavingManager : ISaveState
    {
        private readonly UnitManager _unitsManager;
        private readonly UnitsPrefabStorage _prefabStorage;
        private readonly List<Dictionary<string, string>> _units = new();
        private readonly Transform _unitsRoot;

        public UnitSavingManager(UnitManager manager, UnitsPrefabStorage storage, Transform root)
        {
            _unitsManager = manager;
            _prefabStorage = storage;
            _unitsRoot = root;
        }

        public List<Dictionary<string, string>> CaptureState()
        {
            _units.Clear();
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            foreach (var unit in _unitsManager.GetAllUnits())
            {
                var state = new Dictionary<string, string>
                {
                    {"PositionX", unit.Position.x.ToString()},
                    {"PositionY", unit.Position.y.ToString()},
                    {"PositionZ", unit.Position.z.ToString()},
                    {"RotationX", unit.Rotation.x.ToString()},
                    {"RotationY", unit.Rotation.y.ToString()},
                    {"RotationZ", unit.Rotation.z.ToString()},
                    {"HP", unit.HitPoints.ToString()},
                    {"Type", unit.Type},
                    {"StateType", "Unit"},
                    {"Scene", sceneIndex.ToString()}
                };
                _units.Add(state);
            }

            return _units;
        }

        public void RestoreState(List<Dictionary<string, string>> loadedData)
        {
            DestroyAllUnits();
            InitUnitsList(loadedData);

            var unitsList = new List<Unit>();
            foreach (var state in _units)
            {
                var unitInstance = Object.Instantiate(_prefabStorage.GetUnitByName(state["Type"]), _unitsRoot, true);
                unitInstance.transform.position = new Vector3(float.Parse(state["PositionX"]),
                    float.Parse(state["PositionY"]),float.Parse(state["PositionZ"]));
                unitInstance.transform.eulerAngles = new Vector3(float.Parse(state["RotationX"]),
                    float.Parse(state["RotationY"]),float.Parse(state["RotationZ"]));
                var unit = unitInstance.GetComponent<Unit>();
                unit.HitPoints = Convert.ToInt32(state["HP"]);
                unitsList.Add(unit);
            }
            _unitsManager.SetupUnits(unitsList);
        }

        private void InitUnitsList(List<Dictionary<string, string>> loadedData)
        {
            _units.Clear();
            foreach (var data in loadedData)
            {
                if (data["StateType"] == "Unit")
                {
                    _units.Add(data);
                }
            }
        }

        private void DestroyAllUnits()
        {
            var unitList = _unitsManager.GetAllUnits().ToList();
            for(var i = 0; i < unitList.Count(); i++)
            {
                _unitsManager.DestroyUnit(unitList[i]);
            }
            var existingUnits = Object.FindObjectsOfType<Unit>();
            foreach (var unit in existingUnits)
            {
                Object.Destroy(unit.gameObject);
            }
        }
    }
}
