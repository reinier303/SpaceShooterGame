using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ShopManager shopManager;

    public ScriptableShopItem scriptableShopItem;

    public Image Icon;


    private void Start()
    {
        shopManager = ShopManager.Instance;
        Initialize();
    }

    private void Initialize()
    {
        Icon.sprite = scriptableShopItem.ShopIcon;
    }

    public void BuyItem()
    {
        if(scriptableShopItem.Cost <= shopManager.GetUnits())
        {
            shopManager.RemoveUnits(scriptableShopItem.Cost);
            shopManager.ItemBought(scriptableShopItem);
            scriptableShopItem.UnlockModule();
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        shopManager.Tooltip.gameObject.SetActive(true);
        shopManager.Tooltip.SetTooltipInfo(scriptableShopItem.ShopItemName, scriptableShopItem.Description, scriptableShopItem.Cost, scriptableShopItem.MaxLvl);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shopManager.Tooltip.gameObject.SetActive(false);
    }
}
