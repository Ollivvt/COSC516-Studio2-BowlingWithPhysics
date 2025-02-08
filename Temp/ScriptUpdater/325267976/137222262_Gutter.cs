using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody)
        {
            Debug.Log("Ball detected: Moving inside gutter");

            // Move ball to the exact center of the gutter
            triggeredBody.transform.position = new Vector3(
                transform.position.x,  // Keep same X (left/right)
                triggeredBody.transform.position.y, // Maintain height
                transform.position.z   // Align to gutter Z position
            );

            // Reset velocity so it doesn't bounce away
            ballRigidBody.linearVelocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            // Push ball forward along the gutter
            ballRigidBody.AddForce(transform.forward * 8f, ForceMode.VelocityChange);
        }
    }
}
