using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WoodTempAndRHRouter : MonoBehaviour
{
    public GameObject woodConditionsButton;
    public GameObject surfaceConditionsManager;
    public IMixedRealitySceneSystem sceneSystem;
    public int woodTempValue;
    public int woodRelativeHumidValue;
    public double woodSurvivalRate;
    public HandleColorChange survivalRateForWood;
    public SurfaceSelectorRouter sceneHandler;
    // Start is called before the first frame update
    void Start()
    {
        woodTempValue = 25;
        woodRelativeHumidValue = 35;
        woodSurvivalRate = survivalRateForWood.calculateSurvivalRateWood();
        //sceneSystem = sceneHandler.sceneSystem;
        surfaceConditionsManager = GameObject.Find("WoodConditionsButtonManager");
        woodConditionsButton = surfaceConditionsManager.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;

        var woodButtonEvent = woodConditionsButton.GetComponent<Interactable>();
        //woodButtonEvent.OnClick.AddListener(async () => await loadWoodSceneAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //async System.Threading.Tasks.Task loadWoodSceneAsync()
    //{
    //    await sceneHandler.sceneSystem.LoadContent("2-WoodIImageTargets", LoadSceneMode.Single);
        
    //    //SceneManager.LoadScene("2-WoodIImageTargets");
    //}
}
