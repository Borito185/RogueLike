using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code.Util.MonoBehaviours
{
    public class InvokeAfterDuration : MonoBehaviour
    {
        public float DurationInSeconds;
        public UnityEvent Event;
        private Coroutine _activeCoroutine;
        public void StartWaiting()
        {
            if (_activeCoroutine != null)
                StopCoroutine(_activeCoroutine);

            _activeCoroutine = StartCoroutine(Wait());
        }

        public IEnumerator Wait()
        {
            if (DurationInSeconds > 0)
            {
                float timeWhenEvent = Time.time + DurationInSeconds;
                while (Time.time < timeWhenEvent)
                    yield return null;
            }

            Event.Invoke();
        }
    }
}