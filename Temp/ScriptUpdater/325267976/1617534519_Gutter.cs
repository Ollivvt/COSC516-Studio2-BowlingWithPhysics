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
            Debug.Log("Ball entered gutter. Adjusting entry and reducing speed.");

            // **Gradually align ball to center WITHOUT sudden jumps**
            Vector3 targetPosition = new Vector3(transform.position.x, triggeredBody.transform.position.y - 0.1f, transform.position.z);
            triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, 0.3f);

            // **Gradually reduce speed while maintaining motion**
            Vector3 velocity = ballRigidBody.linearVelocity;
            velocity.x *= 0.05f; // Minimize side movement
            velocity.z *= 0.7f; // Gradually slow down forward speed
            ballRigidBody.linearVelocity = velocity;

            // Apply gentle rolling force
            ballRigidBody.AddForce(transform.forward * 3f, ForceMode.Acceleration);
        }
    }

    private void OnTriggerStay(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody != null)
        {
            // Keep reducing speed slightly over time for a natural rolling effect
            Vector3 velocity = ballRigidBody.linearVelocity;
            velocity.z *= 0.98f; // Slowly reduce forward velocity
            ballRigidBody.linearVelocity = velocity;

            // Apply a small push to prevent complete stopping
            ballRigidBody.AddForce(transform.forward * 1.5f, ForceMode.Acceleration);
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
