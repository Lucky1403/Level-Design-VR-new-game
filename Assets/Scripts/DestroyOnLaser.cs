using UnityEngine;

public class DestroyOnLaser : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
