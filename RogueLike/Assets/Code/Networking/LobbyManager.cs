using System;
using JetBrains.Annotations;
using Mirror;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Code.Networking
{
    public class LobbyManager : MonoBehaviour
    {
        private const string HostAddressKey = "HostAddres";

        private NetworkManager NetworkManager => NetworkManager.singleton;

        protected Callback<LobbyCreated_t> LobbyCreatedCallback;
        protected Callback<GameLobbyJoinRequested_t> GameLobbyJoinRequestedCallback;
        protected Callback<LobbyEnter_t> LobbyEnterCallback;

        public UnityEvent OnStartCreateLobby;
        public UnityEvent OnFinishCreateLobby;
        public UnityEvent OnStartJoinLobby;
        public UnityEvent OnFinishJoinLobby;

        private CSteamID _lobbyId;

        private void Start()
        {
            if (!SteamManager.Initialized)
                return;

            LobbyCreatedCallback = Callback<LobbyCreated_t>.Create(HandleLobbyCreated);
            GameLobbyJoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(HandleGameLobbyJoinRequested);
            LobbyEnterCallback = Callback<LobbyEnter_t>.Create(HandleLobbyEnter);
        }

        public void HostLocal()
        {
            NetworkManager.StartHost();
        }
        public void HostLobby()
        {
            Debug.Log("Creating Lobby");
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, NetworkManager.maxConnections);
            OnStartCreateLobby.Invoke();
        }

        public void InviteToLobby()
        {
            SteamFriends.ActivateGameOverlayInviteDialog(_lobbyId);
        }

        private void HandleLobbyCreated(LobbyCreated_t arg)
        {
            if (arg.m_eResult != EResult.k_EResultOK)
            {
                print(Enum.GetName(typeof(EResult), arg.m_eResult));
                return;
            }
            Debug.Log($"lobby succesfully created at:'{arg.m_ulSteamIDLobby}'");

            NetworkManager.StartHost();
            _lobbyId = new(arg.m_ulSteamIDLobby);

            SteamMatchmaking.SetLobbyData(
                _lobbyId, 
                HostAddressKey,
                SteamUser.GetSteamID().ToString());
            OnFinishCreateLobby.Invoke();
        }

        private void HandleGameLobbyJoinRequested(GameLobbyJoinRequested_t arg)
        {
            SteamMatchmaking.JoinLobby(arg.m_steamIDLobby);
        }
        private void HandleLobbyEnter(LobbyEnter_t arg)
        {
            if (NetworkServer.active)
                return;
            OnFinishJoinLobby.Invoke();
            if (arg.m_EChatRoomEnterResponse != 1)
                return;
            _lobbyId = new(arg.m_ulSteamIDLobby);

            string hostAddress = SteamMatchmaking.GetLobbyData(_lobbyId, HostAddressKey);
            NetworkManager.networkAddress = hostAddress;
            NetworkManager.StartClient();
        }
    }
}
