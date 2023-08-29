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
