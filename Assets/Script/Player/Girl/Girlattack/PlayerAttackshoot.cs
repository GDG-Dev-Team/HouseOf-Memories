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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Destroy(bullet, 5f);
    }
}


