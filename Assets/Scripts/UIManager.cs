using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType {
    shop,
    athletes,
    sponsors,
    leaderboard
}

public class UIManager : MonoBehaviour
{
    public void UIButtonPress(int num) {
        ButtonType buttonType = (ButtonType)num;
        Debug.Log(buttonType);
    }
}
