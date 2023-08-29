# WaterPhysics

<img src="https://github.com/sukrubeyy/WaterPhysics/blob/main/Assets/Images/WaterPhysics.gif"/>

<ul>
<li>Swimmer</li>
<p> All corner of the object you must add this class</p>
<pre>
<code>
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

</code>
</pre>

<li>Wave Manager</li>
<pre>
<code>
using UnityEngine;
public class WaveManager : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 1f;
    public float offset = 0f;
    public float length = 2;
    public static WaveManager singleton;
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != null)
        {
            Destroy(this);
        }
    }
    private void Update() => offset += Time.deltaTime * speed;
    public float GetWaveHeight(float _x) => amplitude * Mathf.Sin(_x / length + offset);
}

</code>
</pre>

<li>Water Manager</li>
<pre>
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterManager : MonoBehaviour
{
    private MeshFilter _mesh;
    void Awake() =>        _mesh = GetComponent<MeshFilter>();
    private void Update()
    {
        Vector3[] vertices = _mesh.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = WaveManager.singleton.GetWaveHeight(transform.position.x + vertices[i].x);
        }
        _mesh.mesh.vertices = vertices;
        _mesh.mesh.RecalculateNormals();
    }
    
}
<code>
</code>
</pre>
</ul>

