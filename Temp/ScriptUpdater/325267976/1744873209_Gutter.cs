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
            Debug.Log("Ball entered gutter. Adjusting speed and centering.");

            // **Instantly center the ball in the gutter**
            Vector3 targetPosition = new Vector3(transform.position.x, triggeredBody.transform.position.y, transform.position.z);
            triggeredBody.transform.position = targetPosition;

            // **Adjust velocity: Reduce speed for smooth motion**
            Vector3 velocity = ballRigidBody.linearVelocity;
            velocity.x = 0f; // Remove sideways drift
            velocity.z = Mathf.Clamp(velocity.z, 3f, 5f); // Keep speed in range
            ballRigidBody.linearVelocity = velocity;
        }
    }

    private void OnTriggerStay(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody != null)
        {
            // **Apply a steady, controlled push forward**
            ballRigidBody.AddForce(transform.forward * 2f, ForceMode.Acceleration);
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
