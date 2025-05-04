using UnityEngine;

public class IoriKnockback_horizontal : MonoBehaviour
{
    public static IoriKnockback_horizontal Instance { get; private set; }
    private Rigidbody2D body;
    [SerializeField] private Iori_main ioriMain;

    // Knockback variables
    private float knockbackForce = 5f; // Default knockback force
    private float knockbackDeceleration = 20f; // Default deceleration
    private Vector2 knockbackVelocity;
    public bool isKnockbackActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (ioriMain == null) ioriMain = GetComponent<Iori_main>();
        body = ioriMain.Body;
    }

    private void Update()
    {
        // Handle knockback deceleration
        if (isKnockbackActive)
        {
            knockbackVelocity = Vector2.MoveTowards(knockbackVelocity, Vector2.zero, knockbackDeceleration * Time.deltaTime);
            body.position += knockbackVelocity * Time.deltaTime;

            if (knockbackVelocity.magnitude < 0.1f)
            {
                isKnockbackActive = false;
                knockbackVelocity = Vector2.zero;
                body.linearVelocity = Vector2.zero; // Stop movement completely
            }
        }
    }
    public void ApplyKnockback(Vector2 direction, float force, float deceleration)
    {
        if (direction == Vector2.zero)
        {
            Debug.LogWarning("Knockback direction is zero, no movement will occur.");
            return;
        }
        if (!Kyo_data.Instance.kyoFlipped)
        {
            direction = Vector2.left;
        }

        // Set custom parameters
        knockbackForce = force;
        knockbackDeceleration = deceleration;

        isKnockbackActive = true;
        knockbackVelocity = direction.normalized * knockbackForce; // Set initial knockback velocity
//        Debug.Log($"Knockback applied: Direction = {direction}, Force = {force}, Deceleration = {deceleration}");
    }
}
