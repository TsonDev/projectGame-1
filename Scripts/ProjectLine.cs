using UnityEngine;

public class projectLine : MonoBehaviour
{
    [SerializeField]private float speed;
    private Animator anim;
    private bool hit;
    private BoxCollider2D boxCollider;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }

}
