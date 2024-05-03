using GameEngine;
using SaveLoadCore;
using SaveLoadCore.Tools;
using SaveSystem.Core;
using SaveSystem.Data;
using SaveSystem.FileSaverSystem;
using UnityEngine;
using Zenject;

namespace SaveSystem.intaller
{
    public sealed class SaveLoadInstaller : MonoInstaller
    {
        [SerializeField] private UnitsPrefabStorage prefabStorage;
        [SerializeField] private Transform unitManagerContainer;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(unitManagerContainer).AsSingle();
            Container.Bind<UnitsPrefabStorage>().FromInstance(prefabStorage).AsSingle();

            Container.Bind<SavingSystem>().AsSingle();
            Container.Bind<UnitManager>().AsSingle();
            Container.Bind<ResourceService>().AsSingle();
            Container.Bind<SaveLoadInfoManager>().AsSingle();
            Container.Bind<SceneSaveManager>().AsSingle();
            Container.Bind<SceneObjectsManager>().AsSingle();
            Container.Bind<ScreenCamera>().AsSingle();
            Container.Bind<FileNameManager>().AsSingle();


            Container.BindInterfacesAndSelfTo<FileSystemSaverLoader>().AsSingle();
            Container.BindInterfacesTo<UnitSavingStorage>().AsSingle();
            Container.BindInterfacesTo<ResourceSavingManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadFactory>().AsSingle();
        }
    }
}
