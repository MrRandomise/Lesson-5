using Core;
using GameEngine;
using SaveSystem.Core;
using SaveSystem.Data;
using SaveSystem.FileSaverSystem;
using SaveSystem.Tools;
using UnityEngine;
using Zenject;

namespace SaveSystem.intaller
{
    public sealed class SaveLoadInstaller : MonoInstaller
    {
        [SerializeField] private UnitsPrefabStorage prefabStorage;
        [SerializeField] private Transform unitManagerContainer;
        [SerializeField] private SavingSystemHelper helper;

        public override void InstallBindings()
        {
            Container.Bind<SavingSystem>().AsSingle();
            
            Container.Bind<UnitManager>().AsSingle();
            Container.Bind<Transform>().FromInstance(unitManagerContainer).AsSingle();
            Container.Bind<ResourceService>().AsSingle();
            Container.Bind<UnitsPrefabStorage>().FromInstance(prefabStorage).AsSingle();
            Container.Bind<SavingSystemHelper>().FromInstance(helper).AsSingle();
            Container.Bind<SceneSaveManager>().AsSingle();
            Container.Bind<SceneStorage>().AsSingle();

            Container.BindInterfacesAndSelfTo<FileSystemSaverLoader>().AsSingle();
            Container.BindInterfacesTo<UnitSavingManager>().AsSingle();
            Container.BindInterfacesTo<ResourceSavingManager>().AsSingle();
        }
    }
}
