using System.Collections.Generic;
using Assets.Code.Input;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code.UI
{
    [AddComponentMenu(menuName:"UI/Menu")]
    public class UIMenu : MonoBehaviour
    {
        public UnityEvent OnEscape;

        private bool _escapeIsSubscribed;
        private void HandleEscape(object sender, EventValueEventArgs<bool> e)
        {
            if (!e.FromDefaultToDefined) return;

            OnEscape.Invoke();
        }
        protected virtual void OnEnable() =>
            SubscribeEscape();
        protected virtual void OnDisable() =>
            UnsubscribeEscape();

        protected virtual void OnDestroy() =>
            UnsubscribeEscape();

        public void SubscribeEscape()
        {
            if (_escapeIsSubscribed) return;
            _escapeIsSubscribed = true;
            InputManager.Instance.Escape.OnValueSet += HandleEscape;
        }
        public void UnsubscribeEscape()
        {
            if (!_escapeIsSubscribed) return;
            _escapeIsSubscribed = false;
            if (InputManager.Instance == null) return;
            InputManager.Instance.Escape.OnValueSet -= HandleEscape;
        }
    }
}