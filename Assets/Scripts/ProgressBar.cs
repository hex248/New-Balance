using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] int maximum;
    [SerializeField] int current;
    [SerializeField] Image progressImage;
    [SerializeField] TextMeshProUGUI percentageText;
    
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        current = Mathf.Clamp(current, 0, maximum);
        float fillAmount = Mathf.Round(((float)current / (float)maximum)*100);
        progressImage.fillAmount = fillAmount/100;
        percentageText.text = $"{fillAmount}%";
    }

    public void SetCurrent(int newCurrent)
    {
        current = newCurrent;
    }
}
