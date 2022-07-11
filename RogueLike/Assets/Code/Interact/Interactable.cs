using Assets.Code.Input;
using Assets.Code.UI;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code.Interact
{
    public class Interactable : MonoBehaviour
    {
        public static Interactable Closest { get; private set; }
        public PolygonCollider2D ThisCollider2D;

        public UnityEvent OnInteract;

        private void Awake()
        {
            if (ThisCollider2D == null && !TryGetComponent(out ThisCollider2D))
                Debug.LogError("No collider found");
        }
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (!IsLocalPlayer(collider) || Closest == this || !IsCloserToCollider(collider))
                return;

            SetThisAsClosest();
        }

        private void OnTriggerExit2D()
        {
            if (Closest == this)
                RemoveAsClosest();
        }
        public float DistanceToCollider(Collider2D collider)
        {
            Vector3 offset = ThisCollider2D.bounds.center - collider.bounds.center;
            //offset on z axis isn't important
            offset.z = 0;
            //double y for isometric compensation ()
            offset.y *= 2;
            return offset.magnitude;
        }

        private bool IsCloserToCollider(Collider2D collider) => 
            Closest == null || DistanceToCollider(collider) < Closest.DistanceToCollider(collider);

        private bool IsLocalPlayer(Collider2D collider) => 
            collider.transform.parent.TryGetComponent(out NetworkIdentity identity) && identity.isLocalPlayer;

        private void SetThisAsClosest()
        {
            if (Closest != null)
                Closest.RemoveAsClosest();
            Closest = this;
            if (InputManager.TryGetInstance(out InputManager instance))
            {
                instance.Interact.OnValueSet += HandleInteract;
            }
        }
        public void RemoveAsClosest()
        {
            Closest = null;
            if (InputManager.TryGetInstance(out InputManager instance))
            {
                instance.Interact.OnValueSet -= HandleInteract;
            }
        }
        private void HandleInteract(object sender, EventValueEventArgs<bool> e)
        {
            if (e.FromDefaultToDefined)
            {
                OnInteract.Invoke();
            }
        }

        public void WriteToConsole(string text)
        {
            print(text);
        }
        
    }
}
