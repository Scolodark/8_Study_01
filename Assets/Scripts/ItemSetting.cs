using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetting : MonoBehaviour
{
    [SerializeField] Sprite spriteinven;//�κ��丮�� ���� �̹���
    public void GetItem()
    {
        if(InventoryManager.Instance.GetItem(spriteinven))//�������� ���� �� ����
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("������ â�� ������");
        }
    }
}
