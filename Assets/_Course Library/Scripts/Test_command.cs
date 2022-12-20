
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Test_command : MonoBehaviour
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

    public OnButtonPress onButton;

    private GameObject parent;

    // Update is called once per frame
    public void Update()
    {
        RaycastHit res;
        bool var = interactor.enabled;
        if (interactor.TryGetCurrent3DRaycastHit(out res))
        {
            
            if (Objects_where_spawn.Contains(res.collider.gameObject))
            {
                groundPt = res.point; // the coordinate that the ray hits
                parent = res.collider.gameObject;
                if (var)
                {
                    action.Enable();
                }
                else
                {

                    action.Disable();
                }
                action.started += Pressed;
                if(parent.transform.childCount > 0)
                {
                    action.Disable();
                    onButton.action.Enable();
                }
                else
                {
                    onButton.action.Disable();
                }
            }
            if (!Objects_where_spawn.Contains(res.collider.gameObject))
            {
                action.Disable();
            }
        }
 

    }
    public void SpawnObj()
    {
        Instantiate(originalObject, groundPt, originalObject.transform.rotation, parent.transform);
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }
}
