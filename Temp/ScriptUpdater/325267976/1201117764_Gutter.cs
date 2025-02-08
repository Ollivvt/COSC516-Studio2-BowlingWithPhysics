using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        if (triggeredBody.CompareTag("Ball")) // Ensure the detected object is the ball
        {
            // Declare the variable inside the scope before using it
            Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

            if (ballRigidBody) // Check if the Rigidbody exists
            {
                Debug.Log("Ball detected in gutter! Applying force...");

                // Stop the ball's current motion
                ballRigidBody.linearVelocity = Vector3.zero;
                ballRigidBody.angularVelocity = Vector3.zero;

                // Apply force to push the ball down the gutter
                ballRigidBody.AddForce(transform.forward * 10f, ForceMode.VelocityChange);
            }
        }
    }
}
