using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 1f;
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private Transform launchIndicator;
    private bool isBallLaunched;
    private bool isInGutter;
    private Rigidbody ballRigidBody;
    public InputManager inputManager;

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody>();

        if (ballRigidBody == null)
        {
            Debug.LogError("BallController: Rigidbody not found! Ensure the ball has a Rigidbody component.");
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
        ballRigidBody.AddForce(transform.forward * force, ForceMode.Impulse);
        launchIndicator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gutter")) // Ensure the gutter has the "Gutter" tag
        {
            isInGutter = true;
            Debug.Log("Ball entered the gutter. Applying smooth transition.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gutter"))
        {
            isInGutter = false; // Reset gutter state when exiting
        }
    }
}
