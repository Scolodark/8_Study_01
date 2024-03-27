using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetting : MonoBehaviour
{
    [SerializeField] Sprite spriteinven;//인벤토리에 나올 이미지
    public void GetItem()
    {
        if(InventoryManager.Instance.GetItem(spriteinven))//아이템을 넣을 수 있음
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("아이템 창이 가득참");
        }
    }
}
