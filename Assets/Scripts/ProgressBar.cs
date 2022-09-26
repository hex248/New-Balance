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
        float fillAmount = (float)current / (float)maximum;
        progressImage.fillAmount = fillAmount;
        percentageText.text = $"{fillAmount*100}%";
    }

    public void SetCurrent(int newCurrent)
    {
        current = newCurrent;
    }
}
