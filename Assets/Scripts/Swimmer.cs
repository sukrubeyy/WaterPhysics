using UnityEngine;

public class Swimmer : MonoBehaviour
{
    public float displacementAmount = 3f;
    public float depthBeforeSubMerged = 1f;
    public Rigidbody rb;

    public int swimSystemCount = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / swimSystemCount, transform.position, ForceMode.Acceleration);
        
        float waveHeight = WaveManager.singleton.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubMerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
