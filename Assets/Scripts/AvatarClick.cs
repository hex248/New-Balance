using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClick : MonoBehaviour
{
    public bool clicked = false;
    
    void OnMouseDown()
    {
        clicked = true;
    }
}
