using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        if (triggeredBody.CompareTag("Ball"))
        {
            Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

            if (ballRigidBody)
            {
                Debug.Log("Ball detected! Applying force...");

                // Stop the ball's current motion
                ballRigidBody.linearVelocity = Vector3.zero;
                ballRigidBody.angularVelocity = Vector3.zero;

                // Apply force in the gutter’s forward direction
                ballRigidBody.AddForce(transform.forward * 5f, ForceMode.VelocityChange);
            }
        }
    }
}

