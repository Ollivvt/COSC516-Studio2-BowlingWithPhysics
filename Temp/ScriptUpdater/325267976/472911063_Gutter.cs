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
            Debug.Log("Ball entered gutter. Adjusting position, reducing speed, and maintaining forward motion.");

            // **Force ball to center smoothly**
            Vector3 targetPosition = new Vector3(transform.position.x, triggeredBody.transform.position.y, transform.position.z);
            triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, 0.8f); // Adjust fast but not instantly

            // **Slow down speed but keep forward movement**
            Vector3 velocity = ballRigidBody.linearVelocity;
            velocity.x = Mathf.Lerp(velocity.x, 0f, 0.5f); // Reduce sideways movement
            velocity.z = Mathf.Lerp(velocity.z, Mathf.Max(velocity.z * 0.5f, 2f), 0.2f); // Slow down forward movement but never stop completely
            ballRigidBody.linearVelocity = velocity;
        }
    }

    private void OnTriggerStay(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody != null)
        {
            // Continue applying small forward force to prevent stopping
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
