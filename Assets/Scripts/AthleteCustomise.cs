using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteCustomise : MonoBehaviour
{
    AthleteSprite AS;
    GameObject selectedPart;

    void Start()
    {
        AS = GetComponent<AthleteSprite>();
    }

    void Update()
    {
        if (AS.skin.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.skin;
            AS.skin.GetComponent<AvatarClick>().clicked = false;
            Debug.Log(selectedPart.name);
        }
        else if (AS.body.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.body;
            AS.body.GetComponent<AvatarClick>().clicked = false;
            Debug.Log(selectedPart.name);
        }
        else if (AS.feet.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.feet;
            AS.feet.GetComponent<AvatarClick>().clicked = false;
            Debug.Log(selectedPart.name);
        }
    }
}
