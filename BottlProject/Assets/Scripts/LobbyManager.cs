using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";

    public string gameScene;

    [SerializeField] byte MAX_PLAYERS = 2;

    private void Awake() // set up photon settings on start
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    private void OnConnectedToMaster() // joined lobby
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    //public void CreateGame()
    //{
    //    PhotonNetwork.CreateRoom("lobby", new RoomOptions() { MaxPlayers = MAX_PLAYERS }, null);
    //}

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYERS;
        PhotonNetwork.JoinOrCreateRoom("lobby", roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(gameScene);
    }
}