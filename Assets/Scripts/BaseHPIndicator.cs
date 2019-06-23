using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHPIndicator : MonoBehaviour {

    PlayerBase playerBase;

	// Use this for initialization
	void Awake () {
        playerBase = FindObjectOfType<PlayerBase>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBaseStatus();
	}

    private void UpdateBaseStatus()
    {
        if (playerBase)
        {
            GetComponent<Text>().text = "❤️ " + playerBase.GetBaseHP().ToString();
        }
    }
}
