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

public class UpdateInfectivityRateForWood : MonoBehaviour
{
    GameObject textForWood;
    public GameObject description;
    public UnityEvent OnButtonClicked;
    public GameObject tempButton;
    public TextMeshPro textMeshPro;
    private bool tempUpdated = false;
    UnityEngine.TouchScreenKeyboard keyboard;
    public static string keyboardText = "";

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = "Adnan";
        textForWood = GameObject.Find("InformationForWoodText");
        description = textForWood.gameObject.transform.GetChild(1).gameObject;
        textMeshPro = description.GetComponent<TextMeshPro>();
        textMeshPro.SetText("hi");

        tempButton = textForWood.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;

        var tempButtonToChange = tempButton.GetComponent<Interactable>();
        tempButtonToChange.OnClick.AddListener(() => textMeshPro.SetText("button change detected!"));

        //textForWood.GetComponent<TextMesh>.textForWood = "Adnan";
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

    double formulaForRate()
    {
        return 0.0;
    }
}
