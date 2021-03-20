using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class NearObjectImplementation : MonoBehaviour, IMixedRealityTouchHandler
{
    public GameObject cubeToDestroy;
    public GameObject particleSystem;

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
        Instantiate(particleSystem, new Vector3(0, 0, 0), Quaternion.identity);
        Destroy(cubeToDestroy);
    }
    public void OnTouchUpdated(HandTrackingInputEventData eventData) { }

}
