using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

/// <summary>
/// Destroys object after a few seconds
/// </summary>
public class DestroyObject : MonoBehaviourPunCallbacks
{
    public void Destroy()
    {
        GameObject Sphere1 = GameObject.Find("Sphere(Clone)");
        GameObject Pannello = GameObject.Find(Sphere1.GetComponent<Destroy>().nomePannello);
        if (Pannello.GetComponent<ContaBandiere>().numerobandiere == 1)
        {
            Pannello.GetComponent<ContaBandiere>().numerobandiere--;
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
