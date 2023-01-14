using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class UIManager : MonoBehaviourPunCallbacks
{

    public static UIManager Instance { get; private set; }

    public UIMessageItem messagePrefab;
    //public Transform contentParent;
    //public GameObject onScreenConsolePrefab;
    public Transform contentParent1;
    public GameObject Impostazioni;
    //private GameObject onScreenConsoleInstance;

    /*
    private void Awake() { 

        bool beingDestroyed = false;

        DontDestroyOnLoad(this);

        if (Instance != null && Instance != this) { 
            Destroy(this.gameObject);
            beingDestroyed = true;
        } 
       else { 
            Instance = this; 
        }

        if(onScreenConsoleInstance == null && !beingDestroyed) {
            onScreenConsoleInstance = Instantiate(onScreenConsolePrefab);
            DontDestroyOnLoad(onScreenConsoleInstance);
            contentParent = onScreenConsoleInstance.transform.Find("Panel/Scroll View/Viewport/ConsoleContent");
            if(contentParent == null) {
                Debug.LogError("Content Parent not found for console.");
            }
        }

    }
    */
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Impostazioni.SetActive(true);
            UIMessageItem message = Instantiate(messagePrefab, contentParent1);
            string messageLine1 = PhotonNetwork.NickName;
            message.SetLine(messageLine1);
        }

    }
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIMessageItem message = Instantiate(messagePrefab, contentParent1);
            //string messageLine = "Ciao";
            //message.SetLine(messageLine);
            string messageLine1 = "Ciao";
            message.SetLine(messageLine1);
        }
    }
    */

    public void PlayerJoinedMessage(string playerName)
    {
        UIMessageItem message = Instantiate(messagePrefab, contentParent1);
        string messageLine = playerName;
        message.SetLine(messageLine);
    }

    public void PlayerLeftMessage(string playerName)
    {
        GameObject[] Messaggi = GameObject.FindGameObjectsWithTag("Message");
        for(int i = 0; i < Messaggi.Length; i++)
        {
            if (Messaggi[i].GetComponent<TMP_Text>().text == playerName) 
            {
                Destroy(Messaggi[i]);            
            }
        }
    }
}
