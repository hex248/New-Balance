using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LockerItem : MonoBehaviour
{
    public Sprite preview;

    public ItemType itemType;
    public string itemName;
    public string itemID;

    public bool locked = false;

    void Update()
    {
        if (locked)
        {
            transform.Find("lock").gameObject.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.Find("lock").gameObject.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
        transform.Find("Preview").GetComponent<Image>().sprite = preview;
    }

    public void Clicked()
    {
        if (!locked)
        {
            FindObjectOfType<AthleteCustomise>().ChangeShoe(this);
        }
    }
}
