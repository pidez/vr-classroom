
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;

public class Object_Spawner : MonoBehaviourPunCallbacks
{
    public XRRayInteractor XRRayInteractor;

    [Tooltip("The object that will be spawned")]
    public GameObject bandierina = null;
    [Tooltip("The transform where the object is spanwed")]
    public Transform spawnPosition = null;

    public List<GameObject> Objects_where_spawn;

    private Vector3 groundPt;

    public InputAction action = null;

    public UnityEvent OnPress = new UnityEvent();

    public GameObject cosaColpisco;
    public void Update()
    {
        RaycastHit res;
        bool var = XRRayInteractor.enabled;

        if (PhotonNetwork.IsMasterClient)
        {
            if (XRRayInteractor.TryGetCurrent3DRaycastHit(out res))
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
        GameObject bandiera = PhotonNetwork.Instantiate(bandierina.name, groundPt, bandierina.transform.rotation, 0);
        cosaColpisco.GetComponent<ContaBandiere>().numerobandiere++;
        bandiera.GetComponent<Object_Destroyer>().nomePannello = cosaColpisco.name;
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }
}
