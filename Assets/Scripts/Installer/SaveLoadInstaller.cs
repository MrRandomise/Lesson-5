using SaveLoadCore.UIView;
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

            Container.Bind<SaveLoadMenuInitializeManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoad>().AsSingle();
        }
    }
}
