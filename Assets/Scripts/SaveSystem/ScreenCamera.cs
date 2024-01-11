using UnityEngine;

namespace SaveLoadCore
{
    public sealed class ScreenCamera
    {
        public bool TrySaveCameraView(Camera cam, out byte[] camRender)
        {
            try
            {
                RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
                cam.targetTexture = screenTexture;
                RenderTexture.active = screenTexture;
                cam.Render();
                Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
                renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                
                camRender = renderedTexture.EncodeToPNG();
                return true;
            }
            catch
            {
                Debug.LogWarning("Не удалось сделать скриншот!");
            }
            camRender = null;
            return false;
        }
    }
}

