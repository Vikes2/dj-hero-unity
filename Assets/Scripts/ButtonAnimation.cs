using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Vector3 defaultTransform;


    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(defaultTransform.x + 0.04f, defaultTransform.y + 0.04f, defaultTransform.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultTransform;
    }

    // Use this for initialization
    void Start () {
        defaultTransform = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
