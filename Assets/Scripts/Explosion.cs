using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] GameObject playerShip;

    public void PlayExplosion()
    {
        explosionVFX.Play();
        Destroy(playerShip);
    }
}
