using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();
        BallController ballController = triggeredBody.GetComponent<BallController>();

        if (ballRigidBody != null && ballController != null)
        {
            ballController.isInGutter = true; // Tell BallController it's in the gutter

            Debug.Log("Ball detected: Adjusting position into gutter");

            // Smoothly move ball towards the center of the gutter
            Vector3 targetPosition = new Vector3(
                transform.position.x, 
                triggeredBody.transform.position.y, 
                transform.position.z
            );

            triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, 0.5f);

            // Reduce speed and smoothly push forward
            ballRigidBody.linearVelocity *= 0.8f;
            ballRigidBody.angularVelocity = Vector3.zero;
            ballRigidBody.AddForce(transform.forward * 8f, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerExit(Collider triggeredBody)
    {
        BallController ballController = triggeredBody.GetComponent<BallController>();
        if (ballController != null)
        {
            ballController.isInGutter = false; // Reset state when leaving gutter
        }
    }
}
