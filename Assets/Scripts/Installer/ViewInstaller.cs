using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace SaveLoadCore.UIView
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private List<GameObject> _saveObjects;

        [SerializeField] private MenuButtonComponents _menuButtonService;

        [SerializeField] private AcceptFormComponents _acceptFormComponents;

        [SerializeField] private MainFomComponents _mainFomComponents;

        [SerializeField] private SaveFormComponents _saveFormComponents;

        public override void InstallBindings()
        {
            Container.Bind<MenuButtonComponents>().FromInstance(_menuButtonService).AsSingle();

            Container.Bind<AcceptFormComponents>().FromInstance(_acceptFormComponents).AsSingle();

            Container.Bind<MainFomComponents>().FromInstance(_mainFomComponents).AsSingle();

            Container.Bind<SaveFormComponents>().FromInstance(_saveFormComponents).AsSingle();

            Container.Bind<ShowMenu>().AsSingle().NonLazy();

            Container.Bind <SaveForm>().AsSingle().NonLazy();

            Container.Bind<SaveFormObserver>().AsSingle().NonLazy();

            Container.Bind <SaveAcceptForm>().AsSingle().NonLazy();

            Container.Bind<LoadObserver>().AsSingle().NonLazy();

            Container.Bind<ReSaveObserver>().AsSingle().NonLazy();

            Container.Bind<SaveLoadSelectedItems>().AsSingle().NonLazy();

            Container.Bind<LoadPreview>().AsSingle().NonLazy();
        }
    }
}
