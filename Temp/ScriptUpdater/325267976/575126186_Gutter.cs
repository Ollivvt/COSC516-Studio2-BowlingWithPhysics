using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        // Debug.Log("Triggered by: " + triggeredBody.gameObject.name);
        
        if (triggeredBody.CompareTag("Ball"))
        {
            Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

            float velocityMagnitude = ballRigidBody.linearVelocity.magnitude;

            if (ballRigidBody)
            {
                Debug.Log("Applying force to direct ball into gutter.");
                ballRigidBody.linearVelocity = Vector3.zero;
                ballRigidBody.angularVelocity = Vector3.zero;

                // Push ball slightly downward and towards the center of the gutter
                Vector3 pushDown = Vector3.down * 2f;
                Vector3 pushForward = transform.forward * 5f;
                Vector3 pushCenter = transform.right * -2f; // Adjust based on gutter orientation

                ballRigidBody.AddForce(pushDown + pushForward + pushCenter, ForceMode.VelocityChange);
            }
        }
    }
}

