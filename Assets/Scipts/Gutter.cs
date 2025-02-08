using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();

        if (ballRigidBody)
        {
            // Debug.Log("Ball detected: Smoothly adjusting position into gutter");

            // Move ball smoothly toward the center of the gutter
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
}
