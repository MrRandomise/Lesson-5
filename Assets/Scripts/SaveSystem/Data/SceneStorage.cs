using GameEngine;
using UnityEngine;

namespace Core
{
    public class SceneStorage
    {
        private readonly UnitManager unitManager;
        private readonly ResourceService resourceService;

        public SceneStorage(UnitManager manager, ResourceService service, Transform unitsRoot)
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
