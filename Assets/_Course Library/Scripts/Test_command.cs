
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;

public class Test_command : MonoBehaviourPunCallbacks
{
    public XRRayInteractor interactor;

    [Tooltip("The object that will be spawned")]
    public GameObject originalObject = null;
    [Tooltip("The transform where the object is spanwed")]
    public Transform spawnPosition = null;

    public List<GameObject> Objects_where_spawn;

    public Vector3 groundPt;

    public int numero_bandiere;

    public InputAction action = null;

    public UnityEvent OnPress = new UnityEvent();

    public UnityEvent OnRelease = new UnityEvent();

    public GameObject cosaColpisco;
    public void Update()
    {
        RaycastHit res;
        bool var = interactor.enabled;

        if (PhotonNetwork.IsMasterClient)
        {
            if (interactor.TryGetCurrent3DRaycastHit(out res))
            {
                cosaColpisco = res.collider.gameObject;
                if (Objects_where_spawn.Contains(res.collider.gameObject))
                {
                    if (cosaColpisco.GetComponent<ContaBandiere>().numerobandiere == 0)
                    {
                        groundPt = res.point; // the coordinate that the ray hits    
                        if (var) // se il raggio è attivo
                        {
                            action.Enable(); // abilita azione
                        }
                        else
                        {

                            action.Disable();
                        }
                        action.started += Pressed;
                    }
                    if (cosaColpisco.GetComponent<ContaBandiere>().numerobandiere == 1)
                    {
                        action.Disable();
                    }
                }
                if (!Objects_where_spawn.Contains(res.collider.gameObject))
                {
                    action.Disable();
                }
                
            }
        }

    }
    public void SpawnObj()
    {
        //Instantiate(originalObject, groundPt, originalObject.transform.rotation, parent.transform);
        GameObject bandiera = PhotonNetwork.Instantiate(originalObject.name, groundPt, originalObject.transform.rotation, 0);
        cosaColpisco.GetComponent<ContaBandiere>().numerobandiere++;
        bandiera.GetComponent<Destroy>().nomePannello = cosaColpisco.name;
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }
}
