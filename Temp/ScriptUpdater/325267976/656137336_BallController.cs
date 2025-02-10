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
            // Reduce lateral movement gradually, but do not kill forward motion
            Vector3 currentVelocity = ballRigidBody.linearVelocity;
            currentVelocity.x *= 0.2f; // Slow down sideways drift slightly
            currentVelocity.z = Mathf.Max(currentVelocity.z, 4f); // Ensure the ball maintains a minimum speed
            ballRigidBody.linearVelocity = currentVelocity;
        }
    }
}
