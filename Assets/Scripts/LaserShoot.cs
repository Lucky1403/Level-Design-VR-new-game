using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    public void FireLaser()
    {
        if (ps != null)
        {
            ps.Emit(1);
            Debug.Log("Laser Fired!");
        }
    }
}
