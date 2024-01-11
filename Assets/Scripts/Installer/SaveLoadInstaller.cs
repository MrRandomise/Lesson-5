using SaveLoadCore;
using Service;
using UnityEngine;
using Zenject;

namespace Installer
{
    public sealed class SaveLoadInstaller : MonoInstaller
    {
        [SerializeField] private SaveObjectsService _saveObjectlist;

        public override void InstallBindings()
        {
            Container.Bind<SaveObjectsService>().FromInstance(_saveObjectlist).AsSingle();

            Container.BindInterfacesAndSelfTo<SaveLoad>().AsSingle().Lazy();

            Container.Bind<ScreenCamera>().AsSingle();
            //Container.BindInterfacesAndSelfTo<ClosePopup>().AsSingle();
            //Container.Bind<AddStatsButton>().AsSingle();
        }
    }
}
