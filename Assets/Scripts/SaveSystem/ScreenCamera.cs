using UnityEngine;

namespace SaveLoadCore
{
    public sealed class ScreenCamera
    {
        public byte[] TrySaveCameraView()
        {
            var cam = Camera.main;
            RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
            cam.targetTexture = screenTexture;
            RenderTexture.active = screenTexture;
            cam.Render();
            Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
            renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

            return renderedTexture.EncodeToPNG();
        }

        public bool TryLoadCameraView(byte[] source, out Sprite screen)
        {
            try
            {
                Texture2D renderedTexture = new Texture2D(2, 2);
                ImageConversion.LoadImage(renderedTexture, source);
                renderedTexture.Apply();
                screen = Sprite.Create(renderedTexture, new Rect(0, 0, renderedTexture.width, renderedTexture.height), Vector2.zero);
                return true;
            }
            catch
            {
                Debug.LogWarning("Что то пошло не так!");
            }
            screen = null;
            return false;
        }
    }
}

