using UnityEngine;

public class Gutter : MonoBehaviour
{
    private Transform parentTransform;

    private void Start()
    {
        // Get the parent GameObject reference
        parentTransform = transform.parent; // This assumes the Gutter (Cylinder) is inside GutterParent
    }

    private void OnTriggerEnter(Collider triggeredBody)
    {
        if (triggeredBody.CompareTag("Ball"))
        {
            Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

            if (ballRigidBody)
            {
                Debug.Log("Ball entered gutter! Applying force...");

                // Reset movement
                ballRigidBody.linearVelocity = Vector3.zero;
                ballRigidBody.angularVelocity = Vector3.zero;

                // Apply force in the GutterParent's forward direction
                ballRigidBody.AddForce(parentTransform.forward * 10f, ForceMode.VelocityChange);
            }
        }
    }
}

