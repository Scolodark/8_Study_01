using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 이동과 점프")]
    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D boxColl;

    [SerializeField] float moveSpeed = 5f;//이동속도
    [SerializeField] float jumpForce = 5f;//점프하는 힘
    [SerializeField] bool isGround;

    bool isJump = false;
    float verticalVelocity = 0f;

    [SerializeField] float rayDistance = 1;
    [SerializeField] Color rayColor;
    [SerializeField] bool showRay = false;

    Vector3 moveDir;

    private void OnDrawGizmos()
    {
        if(showRay == true)
        {
            Gizmos.color = rayColor;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, rayDistance));
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        checkGrounded();

        moving();

        doJump();

        checkGravity();

    }

    private void checkGrounded()
    {
        isGround = false;

        if (verticalVelocity > 0f) return;

        //클라이언트 <-API-> 서버//풀이 적음

        //RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));
        RaycastHit2D ray = Physics2D.BoxCast(boxColl.bounds.center,boxColl.bounds.size, 0f, Vector2.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));
        if (ray)//Ground에 닿음
        {
            isGround = true;
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

    private void doJump()//플레이어가 스페이스키를 누른다면 점프 할 수 있게 준비
    {
        if (isGround == false) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void checkGravity()
    {
        if (isGround == false)//공중에 있을때
        {
            verticalVelocity -= 9.81f*Time.deltaTime;

            if(verticalVelocity < -10.0f)
            {
                verticalVelocity = -10.0f;
            }
        }
        else//땅에 붙어 있을때
        {
            if(isJump == true)
            {
                isJump=false;
                verticalVelocity = jumpForce;
            }
            else if (verticalVelocity < 0)
            {
                verticalVelocity = 0f;
            }
        }

        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }

}
