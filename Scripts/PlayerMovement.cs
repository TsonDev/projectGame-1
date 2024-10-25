using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Move : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jump;
    private Animator anim;
    private bool grounded;
  private Rigidbody2D body;
  private void Awake(){
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  } 
  private void Update(){
     // Lấy đầu vào từ người chơi
        float horizontalInput = Input.GetAxis("Horizontal");


        // Cập nhật vận tốc của Rigidbody2D dựa trên input
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Giới hạn vị trí x trong khoảng từ -7 đến 7
        Vector2 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -7f, 7f);
        transform.position = currentPosition;
        //jump animation
        if(Input.GetKey(KeyCode.Space)){
            body.velocity = new Vector2(body.velocity.x,jump);
        }
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < 0.0f){
            transform.localScale = new Vector3(-1,1,1);
        }
        // set animation run
        anim.SetBool("run",horizontalInput!=0);
  }
  
}
