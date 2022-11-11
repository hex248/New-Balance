using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlink : MonoBehaviour
{
    public string url;
    
    public void Redirect()
    {
        Application.OpenURL(url);
    }
}
