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


public class UpdateSurvivalTimeWood : MonoBehaviour
{
    GameObject survivalInfoWood;
    public GameObject description;
    public UnityEvent OnButtonClicked;
    public GameObject fastForwardButton;
    public GameObject currentTimeButton;
    public GameObject sliderForTemp;
    public GameObject sliderForHumid;
    public TextMeshPro textMeshPro;
    private int count = 0;
    public bool updateEveryFrame = true;
    private bool contentRendered = false;
    public ConfirmSizeOfWoodSurface sizeScriptWood;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = "Adnan";
        survivalInfoWood = GameObject.Find("SurvivalInfoWood");
        description = survivalInfoWood.gameObject.transform.GetChild(1).gameObject;
        //fast forward button
        fastForwardButton = survivalInfoWood.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        var fastForwardChange = fastForwardButton.GetComponent<Interactable>();
        fastForwardChange.OnClick.AddListener(() => updateContent());

        //set to current time button
        currentTimeButton = survivalInfoWood.gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject;
        var currentTimeChange = fastForwardButton.GetComponent<Interactable>();
        currentTimeChange.OnClick.AddListener(() => resetToCurrentTime());
        //survivalInfoWood.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (sizeScriptWood.confirmWoodSurfaceSize)
        {
            //survivalInfoWood.SetActive(true);
            sizeScriptWood.confirmWoodSurfaceSize = false;
        }
        if (updateEveryFrame)
        {
            textMeshPro = description.GetComponent<TextMeshPro>();
            DateTime currentTime = DateTime.Now;
            textMeshPro.SetText(currentTime.ToString());
        }
    }

    void changeColour()
    {

    }

    void renderContent()
    {

        contentRendered = true;
    }

    void updateContent()
    {
        updateEveryFrame = false;
        TextMeshPro localText = description.GetComponent<TextMeshPro>();
        count++;
        var cubeRenderer = GameObject.Find("CubeForWood").GetComponent<Renderer>();
        Shader newShader = Shader.Find("Safe");
        cubeRenderer.material.shader = newShader;
        textMeshPro.SetText("clicked" + count.ToString());
    }

    void resetToCurrentTime()
    {
        updateEveryFrame = true;
    }
}
