using System;
using UnityEngine;

namespace Assets.Code.Tick
{
    public class TickManager : Singleton<TickManager>
    {
        public event Action OnTick;

        [SerializeField]
        [Range(0.01f, 1)]
        private float _tickTime;

        private float _time;

        void Update()
        {
            _time += Time.deltaTime;
            if (_time < _tickTime) return;
            _time -= _tickTime;
            OnTick?.Invoke();
        }


    }
}
