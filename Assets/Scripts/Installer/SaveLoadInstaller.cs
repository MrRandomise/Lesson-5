using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ScreenCamera>().AsSingle();

            Container.Bind<SaveLoadInfo>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoadFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoad>().AsSingle().NonLazy();
        }
    }
}
