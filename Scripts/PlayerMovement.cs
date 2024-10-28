using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;
    private Rigidbody2D body;
  private void Awake(){
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    boxCollider = GetComponent<BoxCollider2D>();
  } 
  private void Update(){
     // Lấy đầu vào từ người chơi
        horizontalInput = Input.GetAxis("Horizontal");


        // Cập nhật vận tốc của Rigidbody2D dựa trên input
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Giới hạn vị trí x trong khoảng từ -7 đến 7
        //Vector2 currentPosition = transform.position;
        //currentPosition.x = Mathf.Clamp(currentPosition.x, -7f, 7f);
        //transform.position = currentPosition;
        //jump animation
       
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < 0.0f){
            transform.localScale = new Vector3(-1,1,1);
        }
        // set animation run , jump
        anim.SetBool("run",horizontalInput!=0);
        anim.SetBool("grounded",isGrounded());

        // logic wall jump
        if (wallJumpCoolDown < 0.2f)
        {
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 5;
            }
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
        {
            wallJumpCoolDown += Time.deltaTime;
        }
  }
  public void Jump(){
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jump);
            anim.SetTrigger("jump");
        }
        else if (!isGrounded() && onWall())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 9);
            }
            wallJumpCoolDown = 0;
        }
     
     
  }
  public void OnCollisionEnter2D(Collision2D collision){
    
  }
    public bool isGrounded()                                      
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    }
    public bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return isGrounded() && !onWall() && horizontalInput==0;
    }
}
