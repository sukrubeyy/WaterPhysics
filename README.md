# WaterPhysics

<img src="https://github.com/sukrubeyy/WaterPhysics/blob/main/Assets/Images/WaterPhysics.gif"/>

<ul>
<li>Swimmer</li>
<p> All corner of the object you must add this class</p>
<pre>
<code>
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

