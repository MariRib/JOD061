using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PhotonConnection : MonoBehaviourPunCallbacks
   
{

    string gameVersion;
    string nickName;
    string roomName = "JOD061";

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.NickName="Player";
        
        Debug.Log("Conectando ao servidor...", this);

        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = nickName + Random.Range(0, 9999);
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado!", this);
        if (PhotonNetwork.CountOfRooms == 0)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Criada a sala " + roomName);
        Debug.Log("Jogador " + PhotonNetwork.LocalPlayer.NickName + " entrou na sala " + roomName + " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")");
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.Log("Desconectado!");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Jogador " + PhotonNetwork.LocalPlayer.NickName + " entrou na sala " + roomName + " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Jogador " + PhotonNetwork.LocalPlayer.NickName + " saiu na sala " + roomName + " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")");
    }

}
