using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Rigidbody ballRB = triggeredBody.GetComponent<Rigidbody>();
        if (ballRB)
        {
            ballRB.linearVelocity = Vector3.zero;
            ballRB.angularVelocity = Vector3.zero;
            ballRB.AddForce(transform.forward * 5f, ForceMode.VelocityChange);
        }
    }
}
