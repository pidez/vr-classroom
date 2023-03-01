using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;

public class Destroy : MonoBehaviourPunCallbacks
{
    public XRRayInteractor xR;

    public GameObject Object;

    public GameObject cosaColpisco;

    public InputAction action = null;

    public UnityEvent OnPress = new UnityEvent();

    public string nomePannello;

    public bool var1;
    // Update is called once per frame


    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            xR = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
            RaycastHit ray;
            var1 = xR.enabled;
            if (xR.TryGetCurrent3DRaycastHit(out ray))
            {
                cosaColpisco = ray.collider.gameObject;
                if (ray.collider.gameObject == Object)
                {

                    if (var1)
                    {
                        action.Enable();

                    }
                    else
                    {
                        action.Disable();
                    }
                    action.started += Pressed;

                }
                if (ray.collider.gameObject != Object)
                {
                    action.Disable();
                }
            }
        }
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }

}
