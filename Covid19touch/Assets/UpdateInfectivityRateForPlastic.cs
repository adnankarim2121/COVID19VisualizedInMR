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

public class UpdateInfectivityRateForPlastic : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = "Adnan";
        textForWood = GameObject.Find("InfoForPlastic");

        description = textForWood.gameObject.transform.GetChild(1).gameObject;
        textMeshPro = description.GetComponent<TextMeshPro>();
        textMeshPro.SetText("Change temperature and humidity value to calculate infectivity rate");

        tempButton = textForWood.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;

        var tempButtonToChange = tempButton.GetComponent<Interactable>();
        tempButtonToChange.OnClick.AddListener(() => formulaForInfectivityRate(1.1, 2.2));

        //slider for temperature input
        sliderForTemp = GameObject.Find("WoodTempSlider");
        sliderForTemp.GetComponent<PinchSlider>().OnValueUpdated.AddListener((SliderValueTemp) => formulaForInfectivityRate(SliderValueTemp, humidVal));

        //slider for humid input
        sliderForHumid = GameObject.Find("WoodHumidSlider");
        sliderForHumid.GetComponent<PinchSlider>().OnValueUpdated.AddListener((SliderValueTemp) => formulaForInfectivityRate(tempVal, SliderValueTemp));
        //textForWood.GetComponent<TextMesh>.textForWood = "Adnan";
    }


    private void formulaForInfectivityRate(double temp, SliderEventData humidity)
    {
        double uv = 2;
        humidVal = humidity.NewValue * 100;
        double answer = 0.16030 + 0.04018 * ((tempVal - 20.615) / 10.585) + 0.02176 * ((humidVal - 45.235))
            + 0.14369 * ((uv - 0.95) / 0.95) + 0.02636 * ((tempVal - 20.615) / 10.585) * ((uv - 0.95) / 0.95);
        textMeshPro.SetText(answer.ToString());
        var sliderText = GameObject.Find("CurrentValueHumid").GetComponent<TextMesh>();
        sliderText.text = humidVal.ToString();
    }
    private void formulaForInfectivityRate(SliderEventData temp, double humidity)
    {
        double uv = 2;
        tempVal = temp.NewValue * 100;
        double answer = 0.16030 + 0.04018 * ((tempVal - 20.615) / 10.585) + 0.02176 * ((humidity - 45.235))
            + 0.14369 * ((uv - 0.95) / 0.95) + 0.02636 * ((tempVal - 20.615) / 10.585) * ((uv - 0.95) / 0.95);
        textMeshPro.SetText(answer.ToString());
        var sliderText = GameObject.Find("CurrentValueTemp").GetComponent<TextMesh>();
        sliderText.text = tempVal.ToString();
    }



    // Update is called once per frame
    void Update()
    {

        //get keyboard text components
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                keyboardText = keyboard.text;
                textMeshPro.SetText(keyboardText);
                keyboard = null;
            }
        }
    }


    public static void AddOnClick(Interactable interactable)
    {
        interactable.OnClick.AddListener(() => Debug.Log("sdsdsd"));
    }
    void invokeKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    void formulaForInfectivityRate(double temp, double humidity)
    {
        double uv = 2;
        double answer = 0.16030 + 0.04018 * ((temp - 20.615) / 10.585) + 0.02176 * ((humidity - 45.235))
            + 0.14369 * ((uv - 0.95) / 0.95) + 0.02636 * ((temp - 20.615) / 10.585) * ((uv - 0.95) / 0.95);
        textMeshPro.SetText(answer.ToString());
    }
}
