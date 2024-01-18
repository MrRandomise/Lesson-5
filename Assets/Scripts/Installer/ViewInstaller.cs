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

            Container.Bind<SaveButton>().AsSingle().NonLazy();

            Container.Bind<SaveForm>().AsSingle().NonLazy();

            Container.Bind <SaveAcceptForm>().AsSingle().NonLazy();

            Container.Bind<LoadButton>().AsSingle().NonLazy();

            Container.Bind<ReSaveButton>().AsSingle().NonLazy();

            Container.Bind<SaveLoadSelectedItems>().AsSingle().NonLazy();

            Container.Bind<SaveLoadMenuInitializeManager>().AsSingle().NonLazy();
        }
    }
}
