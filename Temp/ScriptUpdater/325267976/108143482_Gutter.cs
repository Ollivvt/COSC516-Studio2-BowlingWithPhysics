using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody)
        {
            Debug.Log("Ball detected in the gutter, adjusting motion...");

            // Adjust ball's position smoothly toward the gutter center
            Vector3 targetPosition = new Vector3(
                transform.position.x, 
                triggeredBody.transform.position.y - 0.1f, // Slight downward shift for realism
                transform.position.z
            );

            // Lerp position for a smooth transition
            triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, 0.1f);

            // Reduce velocity slightly to simulate gradual entry
            ballRigidBody.linearVelocity = new Vector3(ballRigidBody.linearVelocity.x * 0.85f, ballRigidBody.linearVelocity.y, ballRigidBody.linearVelocity.z * 0.85f);

            // Ensure ball continues rolling forward
            ballRigidBody.AddForce(transform.forward * 3f, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerStay(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody)
        {
            // Apply a small continuous forward force to keep the ball moving along the gutter
            ballRigidBody.AddForce(transform.forward * 1.5f, ForceMode.Acceleration);
        }
    }
}
