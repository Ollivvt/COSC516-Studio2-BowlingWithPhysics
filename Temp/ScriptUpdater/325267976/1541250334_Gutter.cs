using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggeredBody)
    {
        if (triggeredBody.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = triggeredBody.GetComponent<Rigidbody>();

            if (ballRigidbody)
            {
                ballRigidbody.linearVelocity = Vector3.zero;
                ballRigidbody.angularVelocity = Vector3.zero;
                ballRigidbody.AddForce(transform.forward * 5f, ForceMode.VelocityChange);
            }
        }
    }
}