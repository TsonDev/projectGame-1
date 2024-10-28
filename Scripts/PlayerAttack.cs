using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float coolDownTimer = Mathf.Infinity;
    private void Awake()
    {
       anim = GetComponent<Animator>();
       playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCooldown && playerMovement.canAttack())
        Attack();
        coolDownTimer += Time.deltaTime;
    }
    public void Attack()
    {
        anim.SetTrigger("attack");
        coolDownTimer = 0;
    }
    
}
