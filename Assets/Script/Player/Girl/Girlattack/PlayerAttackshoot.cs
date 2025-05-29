using UnityEngine;

public class PlayerAttackshoot : MonoBehaviour
{
    [Header("Gun Variable")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    void Update()
    {


        if (Input.GetKeyDown("e"))
            Shoot();
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * 30f; // نفس اتجاه firePoint الحالي
        }
    }


}
