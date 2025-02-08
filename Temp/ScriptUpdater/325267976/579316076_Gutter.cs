using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        // Ensure only the ball triggers this event
        BallController ballController = triggeredBody.GetComponent<BallController>();
        if (ballController == null) return;

        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();
        if (ballRigidBody == null) return;

        // Stop external forces acting on the ball
        ballRigidBody.linearVelocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;

        // Move ball smoothly toward the center of the gutter
        Vector3 targetPosition = new Vector3(transform.position.x, triggeredBody.transform.position.y, transform.position.z);
        triggeredBody.transform.position = Vector3.Lerp(triggeredBody.transform.position, targetPosition, 0.2f);

        // Add a forward push to simulate smooth rolling down the gutter
        ballRigidBody.AddForce(transform.forward * 5f, ForceMode.VelocityChange);
    }
}
