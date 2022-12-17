using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{

    //Critical: used by GameManager to keep track of
    //the spawned instances
    public static GameObject localPlayerInstance;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Image speakerImage;

    public Canvas playerNameCanvas;

    private GameObject XROrigin;
    private GameObject XRHead;
    private GameObject XRleftHand;
    private GameObject XRrightHand;

    private PhotonVoiceView photonVoiceView;
    private Recorder recorder;

    private float playerNameOffset = 0.5f;

    void Awake() {

        //Get photon voice view instance
        photonVoiceView = GetComponent<PhotonVoiceView>();
        recorder = GetComponentInChildren<Recorder>();

        recorder.RecordingEnabled = photonView.IsMine;

        if(photonView.IsMine){
            //Critical: teniamo traccia delle istanze dei giocatori gia spawnati
            localPlayerInstance = this.gameObject;

            speakerImage.enabled = false;
        }

        string nickname = photonView.Owner.NickName;
        playerNameCanvas.transform.Find("Name Panel/Player Name").GetComponent<TMP_Text>().text = nickname;

        DontDestroyOnLoad(this.gameObject);

        //Devo posizionare XR Origin dove è stato spawnato l'avatar
        FindAndBindXRComponents();
    }


    void Update() {

        //Critical: Quando si carica una nuova scena bisogna prima di tutto assicurarsi
        //di avere i riferimenti ai componenti dell'XR Origin, visto che questo
        //Cambia da una scena all'altra.
        if(XRHead == null || XRleftHand == null || XRrightHand == null) {
            FindAndBindXRComponents();
        }

        if(photonView.IsMine) {
            mapPosition(head, XRHead.transform);
            mapPosition(leftHand, XRleftHand.transform);
            mapPosition(rightHand, XRrightHand.transform);

            if(playerNameCanvas != null)
                MapPosition(playerNameCanvas.transform, head, 0, playerNameOffset, 0);
        }

        //E' una cosa atroce, ma per qualche motivo la soglia continua a tornare a zero.
        recorder.VoiceDetectionThreshold = 0.02f;
    }

    // Assigns to the prefab components (head, left and right hands) the position and the rotation of the XROrigin
    void mapPosition(Transform target, Transform rigTransform) {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    /*Metodo che mappa la posizione di target in quella di other, con un eventuale offset sulla posizione*/
    void MapPosition(Transform target, Transform other, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f) {

        target.position = other.position + new Vector3(offsetX, offsetY, offsetZ);
        if(other.gameObject == XRrightHand || other.gameObject == XRleftHand) {
            target.rotation = other.rotation;
            target.Rotate(90, 0, 0);
        }
        else {
            target.rotation = other.rotation;
        }
    }

    private void FindAndBindXRComponents() {
        XROrigin = GameObject.Find("XR Origin");
        if(photonView.IsMine)
            mapPosition(XROrigin.transform, gameObject.transform);

        XRHead = GameObject.Find("Main Camera");
        XRleftHand = GameObject.Find("LeftHand Controller");
        XRrightHand = GameObject.Find("RightHand Controller");
    }

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            bool IsRecording = photonVoiceView.IsRecording;
            speakerImage.enabled = IsRecording;
            stream.SendNext(IsRecording);
        }
        else {
            speakerImage.enabled = (bool) stream.ReceiveNext();
        }
    }
    #endregion

}
