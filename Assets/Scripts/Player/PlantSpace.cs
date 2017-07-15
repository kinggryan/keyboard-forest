using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSpace : MonoBehaviour {

    UnityEngine.UI.Text text;

    private int level = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Grow()
    {
        level++;
        text.text = "" + level;
    }
}
