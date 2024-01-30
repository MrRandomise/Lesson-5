using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoadInstaller : MonoInstaller
    {
        [SerializeField] private SaveLoadMediators _saveLoadMediators;

        public override void InstallBindings()
        {
            Container.Bind<SaveLoadMediators>().FromInstance(_saveLoadMediators).AsSingle();

            Container.Bind<ScreenCamera>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoadFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoad>().AsSingle().NonLazy();
        }
    }
}
