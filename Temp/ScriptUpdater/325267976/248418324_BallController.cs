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
            // **Reduce lateral movement gradually, allowing a natural slowdown**
            Vector3 currentVelocity = ballRigidBody.linearVelocity;
            currentVelocity.x = Mathf.Lerp(currentVelocity.x, 0f, 0.5f); // Reduce side movement smoothly
            currentVelocity.z = Mathf.Lerp(currentVelocity.z, Mathf.Max(currentVelocity.z * 0.5f, 2f), 0.3f); // Gradual forward slowdown
            ballRigidBody.linearVelocity = currentVelocity;

            // **Ensure the ball stays centered inside the gutter**
            Vector3 ballPosition = transform.position;
            ballPosition.x = Mathf.Lerp(ballPosition.x, transform.parent.position.x, 0.5f); // Move to center over time
            transform.position = ballPosition;
        }
    }
}
