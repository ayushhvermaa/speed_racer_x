using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    private float horizontalInput;
    public float horizontalMultiplier = 2f;
    public float roadWidth = 20f;
    public float maxAngle = 30f;
    private float minRoadX;
    private float maxRoadX;
    private void Start()
    {
        minRoadX = -roadWidth + 6f;
        maxRoadX = roadWidth - 6f;
    }
    private void FixedUpdate()
    {
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        float minZ = -0.2f;
        float maxZ = 0.2f;

        float targetX = Mathf.Clamp(rb.position.x + horizontalMove.x, minRoadX, maxRoadX);
        float targetZ = Mathf.Clamp(rb.position.z, minZ, maxZ);

        rb.MovePosition(new Vector3(targetX, rb.position.y, targetZ));

        float angle = -horizontalInput * maxAngle;
        Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);

        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 5f);
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
private void OnCollisionEnter(Collision collision)
{
    if (collision.collider.CompareTag("Blocker"))
    {
        GameManage gameManager = FindObjectOfType<GameManage>();
        gameManager.LoseLife();

        collision.gameObject.SetActive(false);

        Vector3 reflectionDirection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
        rb.velocity = reflectionDirection.normalized * speed;

        Debug.Log("Car collided with blocker");
    }
}

    public void UpdatePlayerSpeed(float increaseAmount, float maxSpeed)
    {
        speed += increaseAmount;
        speed = Mathf.Min(speed, maxSpeed);
    }
}




