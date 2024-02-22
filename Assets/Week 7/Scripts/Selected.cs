using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
   
    private SpriteRenderer baseRenderer;
    bool isSelected = false;
    public Color selectedColor;
    // Start is called before the first frame update
    void Start()
    {
        baseRenderer = GetComponent<SpriteRenderer>();
        baseRenderer.color = selectedColor;
        
    }

    
    void Update()
    {
        if (isSelected)
        {
            baseRenderer.color = Color.green;
        }
        else if (!isSelected)
        {
            baseRenderer.color = selectedColor;
        }
    }

    private void OnMouseDown()
    {
        isSelected = true;
    }

    private void OnMouseUp()
    {
        isSelected = false;
    }
}
