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
        private bool _isWaiting;
        private float _startTime;

        private bool DurationIsOver => _startTime + DurationInSeconds <= Time.time;
        public void StartWaiting()
        {
            _isWaiting = true;
            _startTime = Time.time;
        }

        private void Update()
        {
            if (!_isWaiting || !DurationIsOver)
                return;
            _isWaiting = false;
            Event.Invoke();
        }
    }
}