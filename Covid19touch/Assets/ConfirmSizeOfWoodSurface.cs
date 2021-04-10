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

public class ConfirmSizeOfWoodSurface : MonoBehaviour
{
    private GameObject confirmButton;
    public GameObject currentTimeButton;
    public bool confirmWoodSurfaceSize = false;
    private Interactable eventClick;
    // Start is called before the first frame update
    void Start()
    {
        confirmButton = GameObject.Find("ConfirmationButtonWood");
        eventClick = confirmButton.GetComponent<Interactable>();
        eventClick.OnClick.AddListener(() => updateContent());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateContent()
    {
        confirmWoodSurfaceSize = true;
        confirmButton.SetActive(false);
        GameObject.Find("CubeForWood").GetComponent<BoundsControl>().enabled = false;


    }
}
