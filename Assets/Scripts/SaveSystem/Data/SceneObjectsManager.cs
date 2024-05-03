using GameEngine;
using UnityEngine;

namespace SaveSystem.Core
{
    public class SceneObjectsManager
    {
        private readonly UnitManager unitManager;
        private readonly ResourceService resourceService;

        public SceneObjectsManager(UnitManager manager, ResourceService service, Transform unitsRoot)
        {
            unitManager = manager;
            resourceService = service;
            manager.SetContainer(unitsRoot);
        }

        public void Init()
        {
            unitManager.SetupUnits(Object.FindObjectsOfType<Unit>());
            resourceService.SetResources(Object.FindObjectsOfType<Resource>());
        }
    }
}
