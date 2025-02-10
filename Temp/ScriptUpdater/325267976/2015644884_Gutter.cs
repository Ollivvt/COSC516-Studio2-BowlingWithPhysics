using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();
        BallController ballController = triggeredBody.GetComponent<BallController>();

        if (ballRigidBody != null && ballController != null)
        {
            ballController.SetInGutter(true); // Tell BallController it's in the gutter

            Debug.Log("Ball detected: Adjusting position into gutter");

            // Move ball smoothly into the center of the gutter but keep forward momentum
            Vector3 targetPosition = new Vector3(
                transform.position.x, 
                triggeredBody.transform.position.y, 
                transform.position.z
            );

            // Adjust ball's position slightly instead of stopping it
            triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, Time.deltaTime * 10f);

            // Preserve forward velocity
            Vector3 adjustedVelocity = ballRigidBody.linearVelocity;
            adjustedVelocity.x = (targetPosition.x - triggeredBody.transform.position.x) * 2f; // Smoothly correct position
            ballRigidBody.linearVelocity = adjustedVelocity; 

            // Apply a small push forward to prevent stopping
            ballRigidBody.AddForce(transform.forward * 2f, ForceMode.Acceleration);
        }
    }

    private void OnTriggerStay(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();
        BallController ballController = triggeredBody.GetComponent<BallController>();

        if (ballRigidBody != null && ballController != null)
        {
            // Continue applying a small force forward to ensure movement
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
