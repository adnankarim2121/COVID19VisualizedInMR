using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;
using System;
using UnityEngine.SceneManagement;

public class UpdateInfectivityRateForWood : MonoBehaviour
{
    GameObject textForWood;
    public GameObject description;
    public UnityEvent OnButtonClicked;
    public GameObject tempButton;
    public GameObject sliderForTemp;
    public GameObject sliderForHumid;
    public TextMeshPro textMeshPro;
    public double tempVal = 0.0;
    public double humidVal = 0.0;
    private bool tempUpdated = false;
    UnityEngine.TouchScreenKeyboard keyboard;
    public static string keyboardText = "";
    public HandleColorChange survivalRateInformation;

    // Start is called before the first frame update
    void Start()
    {
        //Scene s = SceneManager.GetSceneByName("2-WoodIImageTargets");
        //GameObject[] gameObjects = s.GetRootGameObjects();
        textForWood = GameObject.Find("WoodVuforiaContent").transform.Find("ImageTarget_Wood").gameObject.transform.Find("InfoForWood").gameObject;

        description = textForWood.gameObject.transform.GetChild(1).gameObject;
        textMeshPro = description.GetComponent<TextMeshPro>();
        double infectivityRateWood = formulaForInfectivityRate(survivalRateInformation.woodTemp, survivalRateInformation.woodRH);
        textMeshPro.SetText("The infectivity rate for wood is: ");
        //textMeshPro.ForceMeshUpdate(true);

        
        //GetComponent<TextMesh>().text = "Adnan";


        //var tempButtonToChange = tempButton.GetComponent<Interactable>();
        //tempButtonToChange.OnClick.AddListener(() => formulaForInfectivityRate(1.1,2.2));

        ////slider for temperature input
        //sliderForTemp = GameObject.Find("WoodTempSlider");
        //sliderForTemp.GetComponent<PinchSlider>().OnValueUpdated.AddListener((SliderValueTemp) => formulaForInfectivityRate(SliderValueTemp, humidVal));

        ////slider for humid input
        //sliderForHumid = GameObject.Find("WoodHumidSlider");
        //sliderForHumid.GetComponent<PinchSlider>().OnValueUpdated.AddListener((SliderValueTemp) => formulaForInfectivityRate(tempVal, SliderValueTemp));
        ////textForWood.GetComponent<TextMesh>.textForWood = "Adnan";
    }


    private double formulaForInfectivityRate(int temp, int humidity)
    {
        double uv = 2;
        humidVal = humidity;
        double answer = 0.16030 + 0.04018 * ((tempVal - 20.615) / 10.585) + 0.02176 * ((humidVal - 45.235))
            + 0.14369 * ((uv - 0.95) / 0.95) + 0.02636 * ((tempVal - 20.615) / 10.585) * ((uv - 0.95) / 0.95);
        return answer;
    }




    // Update is called once per frame
    void Update()
    {

    }

}
