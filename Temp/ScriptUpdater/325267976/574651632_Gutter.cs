using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        if (triggeredBody.CompareTag("Ball"))
        {
            Debug.Log("Ball detected by gutter!");

            Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

            if (ballRigidBody)
            {
                // Stop the ball from falling out
                ballRigidBody.constraints = RigidbodyConstraints.FreezePositionY;

                // Apply a small forward force so it moves inside the gutter
                ballRigidBody.linearVelocity = Vector3.zero;
                ballRigidBody.angularVelocity = Vector3.zero;
                ballRigidBody.AddForce(transform.forward * 3f, ForceMode.VelocityChange);
            }
        }
    }
}
