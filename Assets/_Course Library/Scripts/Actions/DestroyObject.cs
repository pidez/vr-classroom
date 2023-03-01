using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

/// <summary>
/// Destroys object after a few seconds
/// </summary>
public class DestroyObject : MonoBehaviourPunCallbacks
{
    public Destroy destroy;
    public void Destroy()
    {
        string nomepannello = destroy.nomePannello;
        GameObject Pannello = GameObject.Find(nomepannello);
        if (Pannello.GetComponent<ContaBandiere>().numerobandiere == 1)
        {
            Pannello.GetComponent<ContaBandiere>().numerobandiere--;
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
