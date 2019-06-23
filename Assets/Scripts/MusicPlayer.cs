using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        int noOfInstances = FindObjectsOfType<MusicPlayer>().Length;

        if (noOfInstances > 1)
        {
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);
	}
}
