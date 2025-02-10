using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();
        BallController ballController = triggeredBody.GetComponent<BallController>();

        if (ballRigidBody != null && ballController != null)
        {
            ballController.SetInGutter(true); // Mark ball as in the gutter
            Debug.Log("Ball entered gutter. Forcing position and maintaining speed.");

            // **Force ball to exact center of the gutter**
            Vector3 targetPosition = new Vector3(transform.position.x, triggeredBody.transform.position.y, transform.position.z);
            triggeredBody.transform.position = targetPosition;

            // **Force forward velocity without slowdown**
            Vector3 velocity = ballRigidBody.linearVelocity;
            velocity.x = 0f; // Completely remove sideways movement
            velocity.z = Mathf.Max(velocity.z, 6f); // Ensure forward velocity is never below 6
            ballRigidBody.linearVelocity = velocity;

            // **Apply a strong forward push**
            ballRigidBody.AddForce(transform.forward * 10f, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerStay(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody != null)
        {
            // Keep forcing forward motion inside the gutter
            ballRigidBody.AddForce(transform.forward * 5f, ForceMode.Acceleration);
        }
    }

    private void OnTriggerExit(Collider triggeredBody)
    {
        BallController ballController = triggeredBody.GetComponent<BallController>();
        if (ballController != null)
        {
            ballController.SetInGutter(false); // Reset state when leaving gutter
        }
    }
}
