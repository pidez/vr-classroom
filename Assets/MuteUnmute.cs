using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using Photon.Pun;
using TMPro;

public class MuteUnmute : MonoBehaviourPunCallbacks
{
    public TMP_Text username;

    public void Mute() 
    {
        GameObject[] Giocatori = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Giocatori.Length; i++)
        {
            Debug.Log("Stampato " + Giocatori[i].GetComponent<PhotonView>().Owner.NickName);
            if (Giocatori[i].GetComponent<PhotonView>().Owner.NickName == username.text)
            {
                Debug.Log("sono entrato");
                if (Giocatori[i].GetComponent<PlayerController>().recorder.RecordingEnabled)
                {
                    Giocatori[i].GetComponent<PlayerController>().muteSelf();
                }
            }
            Debug.Log(" " + i);
        }

    }

    // Update is called once per frame
    public void Unmute()
    {
        GameObject[] Giocatori = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Giocatori.Length; i++)
        {
            Debug.Log("Stampato " + Giocatori[i].GetComponent<PhotonView>().Owner.NickName);
            if (Giocatori[i].GetComponent<PhotonView>().Owner.NickName == username.text)
            {
                Debug.Log("sono entrato");
                if (!Giocatori[i].GetComponent<PlayerController>().recorder.RecordingEnabled)
                {
                    Giocatori[i].GetComponent<PlayerController>().muteSelf();
                }
            }
            Debug.Log(" " + i);
        }
    }

}
