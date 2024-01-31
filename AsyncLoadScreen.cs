using System.Collections;
using UnityEngine;

namespace SaveLoadCore.UIView
{
    public sealed class AsyncLoadScreen : MonoBehaviour
    {
        public void StartAsyncLoad(IEnumerator corutine)
        {
            StartCoroutine(corutine);
        }
    }
}