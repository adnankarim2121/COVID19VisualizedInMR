using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurfaceSelectorRouter : MonoBehaviour
{
    public GameObject woodButton;
    public GameObject metalButton;
    public GameObject plasticButton;
    public GameObject surfaceManager;
    public IMixedRealitySceneSystem sceneSystem;
    // Start is called before the first frame update
    void Start()
    {
        sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        surfaceManager = GameObject.Find("SurfaceButtonManager");
        woodButton = surfaceManager.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        metalButton = surfaceManager.gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject;
        plasticButton = surfaceManager.gameObject.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject;

        var woodButtonEvent = woodButton.GetComponent<Interactable>();
        woodButtonEvent.OnClick.AddListener(async () => await loadWoodConditionsSceneAsync());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async System.Threading.Tasks.Task loadWoodConditionsSceneAsync()
    {
        await sceneSystem.LoadContent("WoodTempAndHumidSelector", LoadSceneMode.Single);
        //SceneManager.LoadScene("2-WoodIImageTargets");
    }
}
