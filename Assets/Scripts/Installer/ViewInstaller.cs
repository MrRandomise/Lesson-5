using UnityEngine;
using Zenject;

namespace SaveLoadCore.UIView
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private ViewService _viewService;
        [SerializeField] private MenuButtonService _menuButtonService;

        public override void InstallBindings()
        {
            Container.Bind<ViewService>().FromInstance(_viewService).AsSingle();

            Container.Bind<MenuButtonService>().FromInstance(_menuButtonService).AsSingle();

            Container.Bind<ShowMenu>().AsSingle().NonLazy();
        }
    }
}
