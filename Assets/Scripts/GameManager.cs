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
                PhotonNetwork.Instantiate(playerPrefab.name, GenerateRandomPosition(9f, 1.2f, 9f), Quaternion.identity, 0);
            }
            else {
                Debug.Log("GameManager: ignoring spawn for player");
            }
        }
    }


    #region private methods

    private void LoadArena() {
        if(PhotonNetwork.IsMasterClient) {
            Debug.Log("GameManager: Loading GenericRoom");
            PhotonNetwork.LoadLevel("GenericRoom");
        }
    }

    private Vector3 GenerateRandomPosition(float limitX, float Y, float limitZ) {
        return new Vector3(Random.Range(-limitX, limitX), Y, Random.Range(-limitZ, limitZ));
    }


    #endregion

    #region PUN Callbacks

    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerEnteredRoom(Player other) {
        Debug.LogFormat("GameManager: {0} entered the room.", other.NickName);
        if(PhotonNetwork.IsMasterClient) {
            //LoadArena();
        }
        UIManager.Instance.PlayerJoinedMessage(other.NickName);
    }

    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerLeftRoom(Player other) {
        Debug.LogFormat("GameManager: {0} left the room.", other.NickName);
    }

    #endregion
}
