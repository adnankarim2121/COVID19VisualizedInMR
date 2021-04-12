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
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;

public class PlasticContentHandler : MonoBehaviour
{
    private GameObject confirmButton;
    public GameObject currentTimeButton;
    public bool confirmWoodSurfacePlastic = false;
    GameObject textForPlastic;
    public GameObject description;
    public TextMeshPro textMeshPro;
    public TextMeshPro textMeshProSR;
    private Interactable eventClick;
    public int tempVal = 21;
    public int humidVal = 65;
    public HandleColorChange survivalRateInformation;
    GameObject survivalInfoPlastic;
    public GameObject descriptionSR;
    public UnityEvent OnButtonClicked;
    public GameObject fastForwardButton;
    public bool updateEveryFrame = true;
    private string survivalRateInformatioString;
    DateTime currentTime;
    // Start is called before the first frame update
    void Start()
    {
        confirmButton = GameObject.Find("ConfirmationButtonPlastic");
        eventClick = confirmButton.GetComponent<Interactable>();
        eventClick.OnClick.AddListener(() => updateContentButton());


        //Infectivity rate for wood handling on startup
        textForPlastic = GameObject.Find("InfoForPlastic");
        description = textForPlastic.gameObject.transform.GetChild(1).gameObject;
        textMeshPro = description.GetComponent<TextMeshPro>();
        //double irw = formulaForInfectivityRate(survivalRateInformation.woodTemp, survivalRateInformation.woodRH);
        double infectivityRatePlastic = formulaForInfectivityRate(tempVal, humidVal);
        textMeshPro.SetText("The infectivity rate for plastic is: " + infectivityRatePlastic.ToString() + " per min");


        //Survival rate handeling
        //GetComponent<TextMesh>().text = "Adnan";
        survivalInfoPlastic = GameObject.Find("SurvivalInfoPlastic");
        descriptionSR = survivalInfoPlastic.gameObject.transform.GetChild(1).gameObject;
        textMeshProSR = descriptionSR.GetComponent<TextMeshPro>();
        currentTime = DateTime.Now;
        survivalRateInformatioString = "The time that COVID-19 was found on this plastic surface was at: " + currentTime.ToString() + "\n"
            + "The survival rate of the virus on this surface at these conditions are: " + calculateSurvivalRatePlastic().ToString() + " hours" + "\n"
            + "This surface will be safe to touch at: " + currentTime.AddHours(calculateSurvivalRatePlastic()).ToString();
        textMeshProSR.SetText(survivalRateInformatioString);
        ////fast forward button
        fastForwardButton = survivalInfoPlastic.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        var fastForwardChange = fastForwardButton.GetComponent<Interactable>();
        fastForwardChange.OnClick.AddListener(() => updateContent());

        //set to current time button
        currentTimeButton = survivalInfoPlastic.gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject;
        var currentTimeChange = fastForwardButton.GetComponent<Interactable>();
        currentTimeChange.OnClick.AddListener(() => resetToCurrentTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmWoodSurfacePlastic)
        {
            //survivalInfoPlastic.SetActive(true);
            //ensure button is clicked only once
            confirmWoodSurfacePlastic = false;
        }
        //textMeshProSR.SetText(DateTime.Now.ToString());
    }

    void updateContentButton()
    {
        confirmWoodSurfacePlastic = true;
        confirmButton.SetActive(false);
        GameObject.Find("CubeForPlastic").GetComponent<BoundsControl>().enabled = false;


    }

    void updateContent()
    {
        updateEveryFrame = false;
        TextMeshPro localText = description.GetComponent<TextMeshPro>();
        var cubeRenderer = GameObject.Find("CubeForPlastic").GetComponent<Renderer>();
        Shader newShader = Shader.Find("Safe");
        cubeRenderer.material.shader = newShader;
        GameObject.Find("SurfaceButtonManager").SetActive(true);
    }

    void resetToCurrentTime()
    {
        var cubeRenderer = GameObject.Find("CubeForPlastic").GetComponent<Renderer>();
        Shader newShader = Shader.Find("NotSafe");
        cubeRenderer.material.shader = newShader;
        textMeshProSR.SetText(survivalRateInformatioString);
    }
    private double formulaForInfectivityRate(int temp, int humidity)
    {
        double uv = 2;
        humidVal = humidity;
        double answer = 0.16030 + 0.04018 * ((tempVal - 20.615) / 10.585) + 0.02176 * ((humidVal - 45.235))
            + 0.14369 * ((uv - 0.95) / 0.95) + 0.02636 * ((tempVal - 20.615) / 10.585) * ((uv - 0.95) / 0.95);
        return answer;
    }

    public double calculateSurvivalRatePlastic()
    {
        double t_hrs = 6.81; // This value is given in the half-life columnn t_hrs=T
        double tau_val; // tau_val=τ
        double lambda_val;
        lambda_val = Math.Log(2) / t_hrs;
        tau_val = 1 / lambda_val;

        // This code is for the equation N(t)=No*exp(-t/τ) this equation is survival time of virus on the surface

        double initial_val = .05;
        int time_var = 72;
        double exponent_val;
        exponent_val = (-time_var / tau_val);
        double inverse_exp;
        inverse_exp = Math.Exp(exponent_val);
        double final_val;
        final_val = initial_val * inverse_exp; // The is the final amount that is left of the virus after the certain half-life
        Console.WriteLine("  the final value with  " + t_hrs + "  half-life is  " + final_val);
        Console.ReadLine();
        return time_var;
    }
}
