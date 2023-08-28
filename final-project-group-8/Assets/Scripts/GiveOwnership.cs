using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveOwnership : MonoBehaviour
{
    [SerializeField] VelNet.NetworkObject networkObject;
    public OVRGrabber grabberL;
    public OVRGrabber grabberR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<OVRGrabbable>().grabbedBy == grabberL || this.GetComponent<OVRGrabbable>().grabbedBy == grabberR)
        {
            networkObject.TakeOwnership();
        }
    }
}
