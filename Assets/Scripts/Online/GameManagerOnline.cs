using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerOnline : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    private void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 0);
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
    }


    public override void OnLeftRoom()
    {
        //Когда текущий игрок выходит (мы)
        SceneManager.LoadScene(2);
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} enterred room", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}
