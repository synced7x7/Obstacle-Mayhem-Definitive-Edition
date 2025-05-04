using UnityEngine;

public class kyoCollisionMechanism : MonoBehaviour
{
    private Rigidbody2D kyoBody;
    private Animator kyoAnim;
    private Collider2D kyoCollider;
    private void Awake()
    {
        kyoBody = GetComponent<Rigidbody2D>();
        kyoAnim = GetComponent<Animator>();
        kyoCollider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Kyo_data.Instance.kyoGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Kyo_data.Instance.kyoGrounded = false;
        }
    }
}
