using System;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private Camera _camera;
        private ScreenCamera _screenCamer;

        public void Initialize()
        {
            Save();
        }

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveObjectsService saveObjects)
        {
            _screenCamer = screenCamer;
            _camera = saveObjects.ScreenCamera;
        }

        public void Save()
        {
            if(_screenCamer.TrySaveCameraView(_camera, out var scrrenShot))
            {
                var Date = DateTime.Now;
            }
        }

        public void Load()
        {

        }
    }
}

