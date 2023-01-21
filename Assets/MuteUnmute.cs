using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using Photon.Pun;
using TMPro;

public class MuteUnmute : MonoBehaviourPunCallbacks
{
    public TMP_Text username;
    
    [SerializeField]
    private TMP_Text btnMsg;

    public void Mute() 
    {
        GameObject[] Giocatori = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Giocatori.Length; i++)
        {
            if (Giocatori[i].GetComponent<PhotonView>().Owner.NickName == username.text) {
                Debug.Log("Muting player: " + username.text);
                bool isMuted = Giocatori[i].GetComponent<PlayerController>().mutePlayer();
                if (isMuted) {
                    btnMsg.text = "Unmute";
                } else {
                    btnMsg.text = "Mute";

                }
            }
        }

    }

}
