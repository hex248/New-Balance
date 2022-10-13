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
        int[] s = AM.selectedAthlete.skinRGB;
        skin.GetComponent<SpriteRenderer>().color = new Color(s[0] / 255.0f, s[1] / 255.0f, s[2] / 255.0f);
        int[] b = AM.selectedAthlete.bodyRGB;
        body.GetComponent<SpriteRenderer>().color = new Color(b[0] / 255.0f, b[1] / 255.0f, b[2] / 255.0f);
        int[] f = AM.selectedAthlete.feetRGB;
        feet.GetComponent<SpriteRenderer>().color = new Color(f[0] / 255.0f, f[1] / 255.0f, f[2] / 255.0f);
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
