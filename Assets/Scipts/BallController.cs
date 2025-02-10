using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private Transform launchIndicator;

    private bool isBallLaunched;
    public bool isInGutter { get; private set; } // Allow Gutter.cs to access this safely
    private Rigidbody ballRigidBody;
    public InputManager inputManager;

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody>();

        if (ballRigidBody == null)
        {
            Debug.LogError("BallController: Rigidbody not found! Ensure the ball has a Rigidbody.");
            return;
        }

        if (inputManager == null)
        {
            Debug.LogError("BallController: InputManager is not assigned in the Inspector.");
            return;
        }

        if (ballAnchor == null)
        {
            Debug.LogError("BallController: BallAnchor is not assigned in the Inspector.");
            return;
        }

        if (launchIndicator == null)
        {
            Debug.LogError("BallController: LaunchIndicator is not assigned in the Inspector.");
            return;
        }

        inputManager.OnSpacePressed.AddListener(LaunchBall);
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        ballRigidBody.isKinematic = true;
    }

    void Update()
    {
        if (!isBallLaunched)
        {
            // Keep ball position exactly in sync with player
            transform.position = ballAnchor.position;
        }
    }

    private void LaunchBall()
    {
        if (isBallLaunched || isInGutter) return; // Prevent multiple launches
        isBallLaunched = true;
        transform.parent = null;
        ballRigidBody.isKinematic = false;

        // Ensure the ball launches in the correct direction
        Vector3 launchDirection = launchIndicator.forward;
        launchDirection.y = 0; // Prevent unwanted vertical launch
        ballRigidBody.AddForce(launchDirection * force, ForceMode.Impulse);

        launchIndicator.gameObject.SetActive(false);
    }

    public void SetInGutter(bool value)
    {
        isInGutter = value;
        if (value)
        {
            // **Force ball to remain centered**
            Vector3 currentVelocity = ballRigidBody.linearVelocity;
            currentVelocity.x = 0f; // Completely remove sideways drift
            currentVelocity.y = 0f; // Stop bouncing
            currentVelocity.z = Mathf.Max(currentVelocity.z, 5f); // Keep moving forward smoothly
            ballRigidBody.linearVelocity = currentVelocity;

            // **Force Launch Indicator to remain centered**
            if (launchIndicator != null)
            {
                Vector3 launchIndicatorPosition = launchIndicator.position;
                launchIndicatorPosition.x = transform.position.x; // Keep it centered
                launchIndicatorPosition.y = transform.position.y + 0.1f; // Prevent clipping issues
                launchIndicator.position = launchIndicatorPosition;
            }
        }
    }
}
