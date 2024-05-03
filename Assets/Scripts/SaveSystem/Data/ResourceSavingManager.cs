using System;
using System.Collections.Generic;
using GameEngine;
using SaveSystem.Core;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace SaveSystem.Data
{
    public class ResourceSavingManager : ISaveState
    {
        private readonly ResourceService _resourceService;
        private readonly List<Dictionary<string, string>> _resources = new();

        public ResourceSavingManager(ResourceService service)
        {
            _resourceService = service;
        }
        
        public List<Dictionary<string, string>> CaptureState()
        {
            _resources.Clear();
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            foreach (var res in _resourceService.GetResources())
            {
                var state = new Dictionary<string, string>
                {
                    {"ID", res.ID},
                    {"Amount", res.Amount.ToString()},
                    {"StateType", "Resource"},
                    {"Scene", sceneIndex.ToString()}
                };
                _resources.Add(state);
            }

            return _resources;
        }

        public void RestoreState(List<Dictionary<string, string>> loadedData)
        {
            _resources.Clear();
            var existingResources = Object.FindObjectsOfType<Resource>();
           
            foreach (var data in loadedData)
            {
                if (data["StateType"] == "Resource")
                {
                    _resources.Add(data);
                }
            }
            foreach (var res in existingResources)
            {
                foreach (var resource in _resources)
                {
                    if (res.ID == resource["ID"])
                    {
                        res.Amount = Convert.ToInt32(resource["Amount"]);
                    }
                }
            }
            _resourceService.SetResources(existingResources);
        }
    }
}
