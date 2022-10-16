using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SidebarItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    AthleteManager AM;
    public Athlete currentAthlete;
    public TextMeshProUGUI nameText;
    public GameObject popup;

    [SerializeField] Image skin;
    [SerializeField] Image body;
    [SerializeField] Sprite skinSprite;
    [SerializeField] Sprite bodySprite;

    [SerializeField] ProgressBar progressBar;

    public bool hover = false;

    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
    }

    void LateUpdate()
    {
        if (currentAthlete.athleteName != "") {

            skin.sprite = skinSprite;
            int[] s = AM.athletes[currentAthlete.id].skinRGB;
            skin.color = new Color(s[0] / 255.0f, s[1] / 255.0f, s[2] / 255.0f);

            body.sprite = bodySprite;
            int[] b = AM.athletes[currentAthlete.id].bodyRGB;
            body.color = new Color(b[0] / 255.0f, b[1] / 255.0f, b[2] / 255.0f);


            nameText.text = currentAthlete.athleteName;
            
            if (hover) {
                popup.SetActive(true);
            }
            else
            {
                popup.SetActive(false);
            }
            currentAthlete = AM.athletes.Find(a => a.id == currentAthlete.id);

            if (currentAthlete.active)
            {
                progressBar.gameObject.SetActive(true);

                long unixTime = GetUnixTime();

                Activity currentActivity = currentAthlete.activity;
                progressBar.maximum = currentActivity.endUnix-currentActivity.startUnix; // activity length
                progressBar.current = unixTime-currentActivity.startUnix; // time since start of activity
            }
            else
            {
                progressBar.gameObject.SetActive(false);
            }
        }
        else
        {
            popup.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentAthlete != null)
        {
            AM.SetAthleteByID(currentAthlete.id);
        }
    }

    long GetUnixTime()
    {
        DateTime time = DateTime.Now;
        long unixTime = ((DateTimeOffset)time).ToUnixTimeSeconds();
        return unixTime;
    }
}
