using Mirror;
using UnityEngine;

namespace Assets.Code.UI
{
    public class CameraFollowPlayer : NetworkBehaviour
    {
        [SerializeField]
        private Camera mainCam;

        [SerializeField]
        private Vector3 _offset;

        void Awake()
        {
            mainCam = Camera.main;
        }

        public override void OnStartLocalPlayer()
        {
            if (mainCam == null) return;

            mainCam.transform.SetParent(transform);
            mainCam.transform.localPosition = _offset;
        }

        public override void OnStopLocalPlayer()
        {
            if (mainCam == null) return;
            mainCam.transform.SetParent(null);
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(mainCam.gameObject, UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
    }
}