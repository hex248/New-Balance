using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public float maximum;
    public float current;
    [SerializeField] Image progressImage;
    [SerializeField] TextMeshProUGUI percentageText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Color foregroundColour;
    [SerializeField] Color backgroundColour;

    void Update()
    {
        GetCurrentFill();

        transform.Find("progress").GetComponent<Image>().color = foregroundColour;
        GetComponent<Image>().color = backgroundColour;

        TimeSpan remainingTime = TimeSpan.FromSeconds(maximum-Mathf.Floor(current));
        string timerString = "";
        if ((int)(maximum-Mathf.Floor(current)) / 3600 > 0) timerString = remainingTime.ToString(@"hh\:mm\:ss");
        else if ((int)(maximum-Mathf.Floor(current)) / 60 > 0) timerString = remainingTime.ToString(@"mm\:ss");
        else if ((int)(maximum-Mathf.Floor(current)) > 0) timerString = remainingTime.ToString(@"ss");
        
        if (timerString == "") timerString = "00";
        
        timerText.text = timerString;
    }

    void GetCurrentFill()
    {
        current = Mathf.Clamp(current, 0, maximum);
        float fillAmount = Mathf.Round((current / maximum)*100);
        progressImage.fillAmount = fillAmount/100;
        if (percentageText != null) percentageText.text = $"{fillAmount}%";
    }

    public void SetCurrent(int newCurrent)
    {
        current = newCurrent;
    }
}
