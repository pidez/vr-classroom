using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;

    void Start() {
        if(playerPrefab == null) {
            Debug.LogError("GameManager: Player prefab is null! Set it in the inspector.");
        }
        else {
            if(PlayerController.localPlayerInstance == null) {
                Debug.Log("GameManager: Instantiating player");
                GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, GenerateRandomPosition(3f, 1.2f, 3f), Quaternion.identity, 0);
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


    #endregion

    #region PUN Callbacks

    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerEnteredRoom(Player other) {
        Debug.LogFormat("GameManager: {0} entered the room.", other.NickName);
        UIManager.Instance.PlayerJoinedMessage(other.NickName);
    }

    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerLeftRoom(Player other) {
        Debug.LogFormat("GameManager: {0} left the room.", other.NickName);
    }

    #endregion
}
