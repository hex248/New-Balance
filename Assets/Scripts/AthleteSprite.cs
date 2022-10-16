using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteSprite : MonoBehaviour
{
    public GameObject skin;
    public GameObject body;
    public GameObject feet;
    public GameObject shoes;

    AthleteManager AM;

    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
    }

    void Update()
    {
        int[] s = AM.athletes[AM.selectedAthleteIDX].skinRGB;
        skin.GetComponent<SpriteRenderer>().color = new Color(s[0] / 255.0f, s[1] / 255.0f, s[2] / 255.0f);
        int[] b = AM.athletes[AM.selectedAthleteIDX].bodyRGB;
        body.GetComponent<SpriteRenderer>().color = new Color(b[0] / 255.0f, b[1] / 255.0f, b[2] / 255.0f);
        int[] f = AM.athletes[AM.selectedAthleteIDX].feetRGB;
        feet.GetComponent<SpriteRenderer>().color = new Color(f[0] / 255.0f, f[1] / 255.0f, f[2] / 255.0f);
        if (AM.athletes[AM.selectedAthleteIDX].active)
        {
            skin.GetComponent<Animator>().SetBool("running", true);
            body.GetComponent<Animator>().SetBool("running", true);
            feet.GetComponent<Animator>().SetBool("running", true);
            shoes.GetComponent<Animator>().SetBool("running", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("running", false);
            body.GetComponent<Animator>().SetBool("running", false);
            feet.GetComponent<Animator>().SetBool("running", false);
            shoes.GetComponent<Animator>().SetBool("running", false);
        }
    }
}
