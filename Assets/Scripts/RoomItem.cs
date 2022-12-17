using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class RoomItem : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text roomName;
    public TMP_Text playersCount;

    public void OnPointerClick(PointerEventData pointerEventData) {
        Debug.Log("RoomItem: clicked.");

        //Si affida alla OnJoinedRoom in LobbyManager
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void SetRoomName(string name) {
        if (!string.IsNullOrEmpty(name))
            roomName.text = name;
    }

    public void SetPlayersCount(int players, byte of) {
        playersCount.text = players + "/" + of;
    }

    public string GetRoomName() {
        return roomName.text;
    }

    public string GetPlayersCount() {
        return playersCount.text;
    }
}
