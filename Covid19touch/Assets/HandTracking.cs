using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;



public class HandTracking : MonoBehaviour
{
    public GameObject sphereOnFinger;

    GameObject thumbObject;
    public GameObject particleSystem;
    public int count = 0;
    GameObject indexFingerObject;
    GameObject middleFingerObject;
    GameObject ringFingerObject;
    GameObject pinkyFingerObject;
    MixedRealityPose pose;
    // Start is called before the first frame update
    void Start()
    {
        // When program starts, make sure the markers are on top of each finger
        thumbObject = Instantiate(sphereOnFinger, this.transform);
        indexFingerObject = Instantiate(sphereOnFinger, this.transform);
        middleFingerObject = Instantiate(sphereOnFinger, this.transform);
        ringFingerObject = Instantiate(sphereOnFinger, this.transform);
        pinkyFingerObject = Instantiate(sphereOnFinger, this.transform);
        particleSystem = Instantiate(particleSystem, this.transform.position, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        // If HoloLens cant find finger joint, it won't set gameobject position to hand it doesn't exist; hence initially false
        thumbObject.GetComponent<Renderer>().enabled = false;
        indexFingerObject.GetComponent<Renderer>().enabled = false;
        middleFingerObject.GetComponent<Renderer>().enabled = false;
        ringFingerObject.GetComponent<Renderer>().enabled = false;
        pinkyFingerObject.GetComponent<Renderer>().enabled = false;
        particleSystem.GetComponent<Renderer>().enabled = false;
        //this is the HoloLens way to say, if we find the right hand finger tip, return its position (pose)
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
        {
            count++;
            thumbObject.GetComponent<Renderer>().enabled = true;
            thumbObject.transform.position = pose.Position;
            particleSystem.GetComponent<Renderer>().enabled = true;
            particleSystem.transform.position = pose.Position;

        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
        {
            indexFingerObject.GetComponent<Renderer>().enabled = true;
            indexFingerObject.transform.position = pose.Position;

        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
        {
            middleFingerObject.GetComponent<Renderer>().enabled = true;
            middleFingerObject.transform.position = pose.Position;

        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
        {
            ringFingerObject.GetComponent<Renderer>().enabled = true;
            ringFingerObject.transform.position = pose.Position;

        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
        {
            pinkyFingerObject.GetComponent<Renderer>().enabled = true;
            pinkyFingerObject.transform.position = pose.Position;

        }

    }
}
