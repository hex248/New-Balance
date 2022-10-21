using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AthleteSprite : MonoBehaviour
{
    public GameObject skin;
    public GameObject body;
    public GameObject shoes;
    GameObject currentShoe;

    public List<GameObject> shoeObjects = new List<GameObject>();

    AthleteManager AM;

    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
    }

    void Update()
    {
        currentShoe = shoeObjects.Find(s => s.name == AM.athletes[AM.selectedAthleteIDX].shoes);
        if (currentShoe != null)
        {
            for (int i = 0; i < shoeObjects.Count; i++)
            {
                if (shoeObjects[i].name != currentShoe.name) shoeObjects[i].SetActive(false);
            }
            currentShoe.SetActive(true);
        }

        int[] s = AM.athletes[AM.selectedAthleteIDX].skinRGB;
        skin.GetComponent<SpriteRenderer>().color = new Color(s[0] / 255.0f, s[1] / 255.0f, s[2] / 255.0f);
        int[] b = AM.athletes[AM.selectedAthleteIDX].bodyRGB;
        body.GetComponent<SpriteRenderer>().color = new Color(b[0] / 255.0f, b[1] / 255.0f, b[2] / 255.0f);
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (AM.athletes[AM.selectedAthleteIDX].active)
            {
                skin.GetComponent<Animator>().SetBool("running", true);
                body.GetComponent<Animator>().SetBool("running", true);
                currentShoe.GetComponent<Animator>().SetBool("running", true);
            }
            else
            {
                skin.GetComponent<Animator>().SetBool("running", false);
                body.GetComponent<Animator>().SetBool("running", false);
                currentShoe.GetComponent<Animator>().SetBool("running", false);
            }
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("running", false);
            body.GetComponent<Animator>().SetBool("running", false);
            currentShoe.GetComponent<Animator>().SetBool("running", false);
        }
    }
}
