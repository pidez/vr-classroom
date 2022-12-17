using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public UIMessageItem messagePrefab;
    public Transform contentParent;
    public GameObject onScreenConsolePrefab;

    private GameObject onScreenConsoleInstance;

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
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            UIMessageItem message = Instantiate(messagePrefab, contentParent);
            string messageLine = "Ciao";
            message.SetLine(messageLine);
        }
    }

    public void PlayerJoinedMessage(string playerName) {
        UIMessageItem message = Instantiate(messagePrefab, contentParent);
        string messageLine = playerName + " joined the room";
        message.SetLine(messageLine);
    }

}
