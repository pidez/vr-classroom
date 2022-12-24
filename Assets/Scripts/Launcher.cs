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

    private bool isTeacher;

    [SerializeField]
    public Button playAsStudentButton;
    public Button playAsTeacherButton;

    [SerializeField]
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;


    void Awake() {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

        isTeacher = false;
    }

    void Start() {
        playAsTeacherButton.onClick.AddListener(ConnectAsTeacher);
        playAsStudentButton.onClick.AddListener(Connect);
    }

    public void ConnectAsTeacher() {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        if(AuthManager.Instance.AuthenticateAsTeacher(username, password)) {
            isTeacher = true;
            Connect();
        }
    }
    public void Connect() {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Launcher: Connected to master server.");
        PhotonNetwork.NickName = usernameInputField.text;
        if(isTeacher){
            SceneManager.LoadScene(1);
        }
        else {
            SceneManager.LoadScene(2);
        }
    }
}
