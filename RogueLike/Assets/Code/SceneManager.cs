using Assets.Code.Networking;
using Mirror;
using Steamworks;
using UnityEngine;

namespace Assets.Code
{
    public class SceneManager : NetworkSingleton<SceneManager>
    {
        [Scene]
        public string GameScene;

        public void StartGame()
        {
            NetworkManager.singleton.ServerChangeScene(GameScene);
        }
        public void ExitGame()
        {
            StopOnline();

            Application.Quit();
        }

        public void StopOnline()
        {
            if (isClient)
                NetworkManager.singleton.StopClient();
            if (isServer)
                NetworkManager.singleton.StopHost();
        }
    }
}