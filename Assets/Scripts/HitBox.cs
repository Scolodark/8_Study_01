using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public enum enumHitType
    {
        WallCheck,
        ItemCheck,
    }

    [SerializeField] private enumHitType hitType;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        //player = transform.parent.GetComponent<Player>();
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.TriggerEnter(hitType, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.TriggerExit(hitType, collision);
    }
}
