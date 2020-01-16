using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text LogText; 
    private void Start()
    {
        PhotonNetwork.NickName = "Plyer " + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() //Подключились к серверу матчмейкингу 
    {
        Log("Connected to Master");
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers  = 2}); //Настройки комнаты
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("Game 1");
    }

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
