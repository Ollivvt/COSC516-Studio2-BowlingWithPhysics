using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();
        BallController ballController = triggeredBody.GetComponent<BallController>();

        if (ballRigidBody != null && ballController != null)
        {
            ballController.SetInGutter(true); // Mark ball as in gutter
            Debug.Log("Ball entered gutter. Adjusting position and maintaining speed.");

            // Align ball smoothly to center of the gutter over time
            Vector3 targetPosition = new Vector3(transform.position.x, triggeredBody.transform.position.y, transform.position.z);
            triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, Time.deltaTime * 8f);

            // Reduce lateral movement but keep forward momentum
            Vector3 velocity = ballRigidBody.linearVelocity;
            velocity.x *= 0.1f; // Reduce side movement gradually
            velocity.z = Mathf.Max(velocity.z, 4f); // Ensure forward speed doesn't drop below 4
            ballRigidBody.linearVelocity = velocity;
        }
    }

    private void OnTriggerStay(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody != null)
        {
            // Apply a slight forward push to keep movement consistent
            ballRigidBody.AddForce(transform.forward * 3f, ForceMode.Acceleration);
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
