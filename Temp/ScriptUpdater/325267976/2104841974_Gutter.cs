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

            // Move ball smoothly into the center of the gutter
            Vector3 targetPosition = new Vector3(
                transform.position.x, 
                triggeredBody.transform.position.y, 
                transform.position.z
            );

            triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, Time.deltaTime * 5f);

            // Slow down movement and guide forward
            ballRigidBody.linearVelocity *= 0.5f;
            ballRigidBody.angularVelocity = Vector3.zero;
            ballRigidBody.AddForce(transform.forward * 5f, ForceMode.VelocityChange);
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
