using UnityEngine;

public class KnockbackPhysicsKyoXY : MonoBehaviour
{
    public static KnockbackPhysicsKyoXY Instance { get; private set; }
    private Rigidbody2D body;
    [SerializeField] private Kyo_main kyoMain;

    // Knockback variables
    private float knockbackForceX = 5f; // Default X-axis knockback force
    private float knockbackForceY = 5f; // Default Y-axis knockback force
    private float decelerationX = 20f; // Default X-axis deceleration
    private float decelerationY = 20f; // Default Y-axis deceleration
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

        if (kyoMain == null) kyoMain = GetComponent<Kyo_main>();
        body = kyoMain.Body;
    }

    private void Update()
    {
        // Handle knockback deceleration
        if (isKnockbackActive)
        {
            // Decelerate knockback velocity independently for each axis
            knockbackVelocity.x = Mathf.MoveTowards(knockbackVelocity.x, 0f, decelerationX * Time.deltaTime);
            knockbackVelocity.y = Mathf.MoveTowards(knockbackVelocity.y, 0f, decelerationY * Time.deltaTime);

            body.position += knockbackVelocity * Time.deltaTime;

            // Stop knockback when both velocities are close to zero
            if (Mathf.Abs(knockbackVelocity.x) < 0.1f && Mathf.Abs(knockbackVelocity.y) < 0.1f)
            {
                isKnockbackActive = false;
                knockbackVelocity = Vector2.zero;
                body.linearVelocity = Vector2.zero; // Stop movement completely
            }
        }
    }

    public void ApplyKnockback(float directionX, float directionY, float forceX, float forceY, float decelerationX, float decelerationY)
    {
        // Validate the direction
        if (directionX == 0 && directionY == 0)
        {
            Debug.LogWarning("Knockback direction is zero, no movement will occur.");
            return;
        }
        if (Iori_data.Instance.ioriFlipped)
        {
            directionX*=-1;
        }

        // Set custom parameters
        knockbackForceX = forceX;
        knockbackForceY = forceY;
        this.decelerationX = decelerationX;
        this.decelerationY = decelerationY;

        isKnockbackActive = true;

        // Calculate initial knockback velocity
        knockbackVelocity = new Vector2(directionX, directionY).normalized;
        knockbackVelocity.x *= knockbackForceX;
        knockbackVelocity.y *= knockbackForceY;

        // Debug message for testing
        // Debug.Log($"Knockback applied: DirectionX = {directionX}, DirectionY = {directionY}, " +
        //   $"ForceX = {forceX}, ForceY = {forceY}, DecelerationX = {decelerationX}, DecelerationY = {decelerationY}");
    }
}
