using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIItem : MonoBehaviour
{
    private ShopManager shopManager;

    public ScriptableShopItem scriptableShopItem;

    private void Start()
    {
        shopManager = ShopManager.Instance;
    }

    public void BuyItem()
    {
        if(scriptableShopItem.Cost <= shopManager.GetUnits())
        {
            shopManager.RemoveUnits(scriptableShopItem.Cost);
            scriptableShopItem.UnlockModule();
        }
    }
}
