using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class NearObjectImplementation : MonoBehaviour, IMixedRealityTouchHandler
{
    public GameObject cubeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
    }
    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        cubeToDestroy = GameObject.Find("CubeToDestroy");
        Destroy(cubeToDestroy);
    }
    public void OnTouchUpdated(HandTrackingInputEventData eventData) { }

}
