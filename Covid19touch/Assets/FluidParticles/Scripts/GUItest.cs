using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUItest : MonoBehaviour {

    public Fluid fluid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10,10,130,35),"Reset"))
        {
            fluid.Reset();
        }
    }
}
