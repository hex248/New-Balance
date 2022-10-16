using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AvatarClick : MonoBehaviour
{
    public bool clicked = false;
    
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        clicked = true;
    }
}
