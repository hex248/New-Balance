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
    [SerializeField] Color foregroundColour;
    [SerializeField] Color backgroundColour;

    void Update()
    {
        GetCurrentFill();

        transform.Find("progress").GetComponent<Image>().color = foregroundColour;
        GetComponent<Image>().color = backgroundColour;
    }

    void GetCurrentFill()
    {
        current = Mathf.Clamp(current, 0, maximum);
        float fillAmount = Mathf.Round((current / maximum)*100);
        progressImage.fillAmount = fillAmount/100;
        percentageText.text = $"{fillAmount}%";
    }

    public void SetCurrent(int newCurrent)
    {
        current = newCurrent;
    }
}
