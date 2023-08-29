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
