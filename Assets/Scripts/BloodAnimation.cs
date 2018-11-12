using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAnimation : MonoBehaviour {

    private int randInt;
    private int currentInt = 0;
    private float minScale = 0.2f;
    private float maxScale = 1f;

    private Vector3 startPos;

	// Use this for initialization
	void Start () {
        randInt = Random.Range(0, 1000);
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, 1);
        startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {


        if (transform.localPosition.y < -240 || currentInt <= randInt)
        {
            float scale = Random.Range(minScale, maxScale);
            transform.localPosition = startPos;
            transform.localScale = new Vector3(scale, scale, 1);
        }

        if(currentInt <= randInt)
        {
            currentInt++;
        }

    }
}
