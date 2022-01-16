using System.Collections;
using UnityEngine;

namespace SelimSahinUtils
{
    public class DelayManager : MonoBehaviour
    {
        private static MonoBehaviour mono = null;

        private static MonoBehaviour GetMono {
            get {
                if (mono == null)
                {
                    var obj = new GameObject("Delay Manager");
                    DontDestroyOnLoad(obj);
                    mono = obj.AddComponent<DelayManager>();
                }
                return mono;
            }
        }

        public static void WaitAndInvoke(System.Action callback, float t = 1f)
        {
            if (callback == null || t <= 0) return;
            GetMono.StartCoroutine(WaitAndInvokeCoroutine(callback, t));
        }

        private static IEnumerator WaitAndInvokeCoroutine(System.Action callback, float t)
        {
            yield return new WaitForSeconds(t);
            callback.Invoke();
        }

        public static void WaitAndInvoke<T>(System.Action<T> callback, T parameterValue, float t = 1f)
        {
            if (callback == null || t <= 0) return;
            GetMono.StartCoroutine(WaitAndInvokeCoroutine(callback, parameterValue, t));
        }

        private static IEnumerator WaitAndInvokeCoroutine<T>(System.Action<T> callback, T parameterValue, float t)
        {
            yield return new WaitForSeconds(t);
            callback.Invoke(parameterValue);
        }
    }
}
