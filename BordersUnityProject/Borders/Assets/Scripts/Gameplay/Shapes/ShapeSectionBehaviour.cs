﻿using UnityEngine;
using System.Collections;

public class ShapeSectionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");

        if(other.tag == "Pellet")
        {
            other.gameObject.SetActive(false);
        }
    }
}