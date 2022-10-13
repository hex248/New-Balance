using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteSprite : MonoBehaviour
{
    [SerializeField] GameObject skin;
    [SerializeField] GameObject body;
    [SerializeField] GameObject feet;

    AthleteManager AM;

    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
    }

    void Update()
    {
        // skin.GetComponent<SpriteRenderer>().color = 
        if (AM.athletes[AM.selectedAthleteIDX].active)
        {
            skin.GetComponent<Animator>().SetBool("running", true);
            body.GetComponent<Animator>().SetBool("running", true);
            feet.GetComponent<Animator>().SetBool("running", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("running", false);
            body.GetComponent<Animator>().SetBool("running", false);
            feet.GetComponent<Animator>().SetBool("running", false);
        }
    }
}
