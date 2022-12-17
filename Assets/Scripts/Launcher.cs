using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{

    private string gameVersion = "0.1";

    [SerializeField]
    public Button playButton;

    [SerializeField]
    public TMP_InputField playerNameInputField;


    void Awake() {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start() {
        playButton.onClick.AddListener(Connect);
    }

    public void Connect() {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }
    public override void OnConnectedToMaster() {
        Debug.Log("Launcher: Connected to master server.");
        PhotonNetwork.NickName = playerNameInputField.text;
        SceneManager.LoadScene(1);
    }
}
