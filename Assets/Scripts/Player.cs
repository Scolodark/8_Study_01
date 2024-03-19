using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� �̵��� ����")]
    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D boxColl;

    [SerializeField] float moveSpeed = 5f;//�̵��ӵ�
    [SerializeField] float jumpForce = 5f;//�����ϴ� ��
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

        //Ŭ���̾�Ʈ <-API-> ����//Ǯ�� ����

        //RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));
        RaycastHit2D ray = Physics2D.BoxCast(boxColl.bounds.center,boxColl.bounds.size, 0f, Vector2.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));
        if (ray)//Ground�� ����
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

        if(moveDir.x != 0.0f)//���������� ���� ������x�� -1, �������� ���� ������x�� 1
        {
            Vector3 locScale = transform.localScale;
            locScale.x = Input.GetAxisRaw("Horizontal") * -1;
            transform.localScale = locScale;
        }
    }

    private void doJump()//�÷��̾ �����̽�Ű�� �����ٸ� ���� �� �� �ְ� �غ�
    {
        if (isGround == false) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void checkGravity()
    {
        if (isGround == false)//���߿� ������
        {
            verticalVelocity -= 9.81f*Time.deltaTime;

            if(verticalVelocity < -10.0f)
            {
                verticalVelocity = -10.0f;
            }
        }
        else//���� �پ� ������
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
