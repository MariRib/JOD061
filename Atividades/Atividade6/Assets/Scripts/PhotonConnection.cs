﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonConnection : MonoBehaviourPunCallbacks
{

    public InputField input;
    public Button button;

    public string gameVersion;
    public string roomName;
    byte maxPlayers = 4;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }



    public void OnClickConnectar ()
    {
        if (!PhotonNetwork.IsConnected)
        {
            input.interactable = false;
            button.interactable = false;
            PhotonNetwork.NickName = input.text;
            PhotonNetwork.GameVersion = this.gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            return;
        }

        if (PhotonNetwork.CountOfRooms == 0)
        {
            button.interactable = false;
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = this.maxPlayers;
            PhotonNetwork.JoinOrCreateRoom(this.roomName, options, TypedLobby.Default);
            return;
        }

        PhotonNetwork.JoinRoom(this.roomName);
    }

    public override void OnConnectedToMaster()
    {
        button.interactable = true;
        button.GetComponentInChildren<Text>().text = "Jogar";
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

}
