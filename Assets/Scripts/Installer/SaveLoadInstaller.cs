using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoadInstaller : MonoInstaller
    {
        [SerializeField] private SaveComponentsService _saveObjectlist;

        public override void InstallBindings()
        {
            Container.Bind<SaveComponentsService>().FromInstance(_saveObjectlist).AsSingle();

            Container.Bind<ScreenCamera>().AsSingle();

            Container.Bind<LoadManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoadFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoad>().AsSingle().NonLazy();
        }
    }
}
