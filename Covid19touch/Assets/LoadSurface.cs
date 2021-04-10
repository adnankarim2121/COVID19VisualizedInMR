using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;


public class LoadSurface : MonoBehaviour, IMixedRealitySpeechHandler
{
    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        loadSurface(eventData.Command.Keyword);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("ImageTarget_Wood").SetActive(false);
       // GameObject.Find("VuforiaContent").transform.GetChild(1).gameObject.SetActive(false);
        //GameObject.Find("VuforiaContent").transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadSurface(string keyword)
    {
        System.Console.Write(keyword);
        switch(keyword)
        {
            case "Wood":
                //GameObject.Find("ImageTarget_Wood").SetActive(true);
                //System.Console.Write("Made it here!");
                break;
            default:
                break;
        }
    }


}
