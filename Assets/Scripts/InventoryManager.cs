using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] GameObject objInventory;
    //[SerializeField] KeyCode keyInventory;
    List<Transform> listInventory = new List<Transform>();
    [SerializeField] GameObject objitem;

    private void Awake()//�̱���
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        initInventory();
    }

    private void initInventory()
    {
        Transform[] rangeData = objInventory.transform.GetComponentsInChildren<Transform>();
        listInventory.AddRange(rangeData);

        listInventory.RemoveAt(0);
    }

    void Update()
    {
        showInventory();
    }

    //private void OnGUI() <= ���� Ű Ȯ��
    //{
    //    Event e = Event.current;
    //    if(e.isKey == true)
    //    {
    //        Debug.Log($"key={e.keyCode}");
    //    }
    //}

    private void showInventory()
    {
        if(Input.GetKeyDown(KeyCode.I)|| Input.GetKeyDown(KeyCode.Tab))
        {
            objInventory.SetActive(!objInventory.activeSelf);

            //if(objInventory.activeSelf == true) <= ���� ���� ���
            //{
            //    objInventory.SetActive(false);
            //}
            //else
            //{
            //    objInventory.SetActive(true);
            //}
        }
    }

    /// <summary>
    /// ����ִ� ������ ���� ��ȣ�� ����
    /// </summary>
    /// <returns></returns>
    private int getEmpyteItemSlot()
    {
        int count = listInventory.Count;
        for(int i = 0; i < count; i++)
        {
            Transform trsSlot = listInventory[i];
            if(trsSlot.childCount == 0)
            {
                return i;
            }
        }

        return -1;
    }

    public bool GetItem(Sprite _spr)
    {
        int slotNum = getEmpyteItemSlot();
        if(slotNum == -1)
        {
            return false;//������ ���� ����
        }

        //Todo
        Instantiate(objitem);

        return true;//������ ���� ����
    }
}
