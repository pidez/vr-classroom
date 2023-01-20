using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    readonly string SOLAR_SYSTEM = "SolarSystem";
    public GameObject playerPrefab;
    public GameObject panelPrefab1;
    public GameObject panelPrefab2;
    public GameObject newPanel1;
    public GameObject newPanel2;
    public Test_command test_Command;
    public UIManager uIManager;
    public string nome;
    void Start() {
        if(playerPrefab == null) {
            Debug.LogError("GameManager: Player prefab is null! Set it in the inspector.");
        }
        else {
            if(PlayerController.localPlayerInstance == null) {
                Debug.Log("GameManager: Instantiating player");
                Vector3 spawnPos;
                if (PhotonNetwork.CurrentRoom.Name == SOLAR_SYSTEM) {
                    spawnPos = GenerateRandomPositionOnXAxis(3f, 0, -25);
                } else {
                    spawnPos = GenerateRandomPosition(3f, 1.2f, 3f);

                }
                GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity, 0);
                if (panelPrefab1 && panelPrefab2 != null)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        newPanel1 = PhotonNetwork.Instantiate(panelPrefab1.name, panelPrefab1.transform.position, Quaternion.identity, 0);
                        newPanel2 = PhotonNetwork.Instantiate(panelPrefab2.name, panelPrefab2.transform.position, Quaternion.identity, 0);
                        test_Command.Objects_where_spawn.Add(newPanel1);
                        test_Command.Objects_where_spawn.Add(newPanel2);
                    }
                }
                Transform netPlayerCanvas = newPlayer.transform.Find("Head/NameCanvasContainer/Canvas/Name Panel/Player Name");
                nome = netPlayerCanvas.GetComponent<TMP_Text>().text;
                GameObject authHelper = GameObject.Find("AuthHelper");
                if (PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
                    //Se si è i primi ad entrare, allora si è il docente
                    newPlayer.GetComponent<PlayerController>().SetTeacher(true);
                }
                //Caso in cui il docente si fosse scollegato
                // la presenza del gameobject authHelper ci informa che il docente si è ricollegato
                // allora si rende master client e si distrugge authHelper.
                else if (authHelper != null) {
                    newPlayer.GetComponent<PlayerController>().SetTeacher(true);
                    PhotonNetwork.SetMasterClient(newPlayer.GetComponent<PhotonView>().Controller);
                    Destroy(authHelper);
                }
                else {
                    newPlayer.GetComponent<PlayerController>().SetTeacher(false);
                }
            }
            else {
                Debug.Log("GameManager: ignoring spawn for player");
            }
        }
    }


    #region private methods

    private Vector3 GenerateRandomPosition(float limitX, float Y, float limitZ) {
        return new Vector3(Random.Range(-limitX, limitX), Y, Random.Range(-limitZ, limitZ));
    }

    private Vector3 GenerateRandomPositionOnXAxis(float limitX, float Y, float Z) {
        return new Vector3(Random.Range(-limitX, limitX), Y, Z);
    }

    #endregion

    #region PUN Callbacks

    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("GameManager: {0} entered the room.", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"pippo:{other.NickName}");
            uIManager.PlayerJoinedMessage(other.NickName);
        }
    }


    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerLeftRoom(Player other) {
        Debug.LogFormat("GameManager: {0} left the room.", other.NickName);
        if (uIManager != null)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                uIManager.PlayerLeftMessage(other.NickName);
            }
        }
    }

    #endregion
}
