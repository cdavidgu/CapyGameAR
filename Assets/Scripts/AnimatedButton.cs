using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class AnimatedButton :  MonoBehaviour,IPointerDownHandler, IPointerUpHandler

{

    Vector3 originalScale;
    float scaleFactor=1.25f;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = new Vector3(originalScale.x*scaleFactor, originalScale.y*scaleFactor,   1.0f); 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

}