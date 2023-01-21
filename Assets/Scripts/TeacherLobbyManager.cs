using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class TeacherLobbyManager : LobbyManager
{

    [SerializeField]
    public Button createRoomButton;

    [SerializeField]
    public TMP_InputField roomNameInputField;

    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    [SerializeField]
    TMP_Text lobbyMessage;


    void Start() {
        PhotonNetwork.JoinLobby();
        createRoomButton.onClick.AddListener(HelpCreateRoom);
        lobbyMessage.text = PhotonNetwork.NickName + ", as a teacher";
        base.startCreateRoomCoroutine();
    }


    #region private methods

    private void HelpCreateRoom() {
        string roomName = roomNameInputField.text;
        if (roomName != null)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }
        else
        {
            Debug.Log("Nome Stanza non valido");
        }
    }

    #endregion

    #region public methods


    #endregion

}
