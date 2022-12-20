using UnityEngine;

/// <summary>
/// Destroys object after a few seconds
/// </summary>
public class DestroyObject : MonoBehaviour
{
    [Tooltip("Time before destroying in seconds")]
    public float lifeTime = 5.0f;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
