using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Tooltip : MonoBehaviour
{
    public TMP_Text ItemName;
    public TMP_Text Description;
    public TMP_Text Price;

    public Vector2 Offset;
    public Vector2 Padding;

    private Rect rect;
    private RectTransform rectTransform;


    private Canvas canvas;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rect = rectTransform.rect;
        canvas = ShopManager.Instance.transform.GetComponent<Canvas>();
    }

    public void SetTooltipInfo(string name, string description, float price, int maxLvl)
    {
        ItemName.text = name;
        description = description.Replace("-MaxLevel-", maxLvl + "");
        description = description.Replace("-Name-", name);
        Description.text = description;       
        Price.text = "" + price;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Description.preferredHeight + 120);
        Description.rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - 50, Description.preferredHeight + 120);
    }

    private void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        Vector3 newPos = Input.mousePosition + (Vector3)Offset;
        newPos.z = 0f;
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + rect.width / 2) - Padding.x;
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - rect.width / 2) + Padding.x;
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + rect.height / 2) - Padding.y;
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }
        float bottomEdgeToScreenEdgeDistance = 0 - (newPos.y - rect.height / 2) + Padding.y;
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += bottomEdgeToScreenEdgeDistance;
        }
        transform.position = newPos;
    }

}
