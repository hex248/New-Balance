using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockerItem : MonoBehaviour
{
    public Sprite preview;

    public ItemType itemType;
    public string itemName;
    public string itemID;
    public int price;

    public bool locked = false;

    void Update()
    {
        if (locked)
        {
            transform.Find("lock").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("lock").gameObject.SetActive(false);
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
