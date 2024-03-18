using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 이동과 점프")]
    Rigidbody2D rigid;
    Animator anim;

    [SerializeField] float moveSpeed = 5f;//이동속도
    [SerializeField] float jumpForce = 5f;//점프하는 힘
    [SerializeField] bool isGround;

    bool isJump;
    float verticalVelocity = 0f;

    Vector3 moveDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        checkGrounded();

        moving();
    }

    private void checkGrounded()
    {
        isGround = false;

        if (verticalVelocity > 0f) return;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (ray)//Ground에 닿음
        {

        }
    }

    private void moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//-1,0,1
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
        //0 false
        //1,-1 true
        anim.SetBool("Walk",moveDir.x != 0.0f);

        if(moveDir.x != 0.0f)//오른쪽으로 갈때 스케일x는 -1, 왼쪽으로 갈때 스케일x는 1
        {
            Vector3 locScale = transform.localScale;
            locScale.x = Input.GetAxisRaw("Horizontal") * -1;
            transform.localScale = locScale;
        }
    }

}
