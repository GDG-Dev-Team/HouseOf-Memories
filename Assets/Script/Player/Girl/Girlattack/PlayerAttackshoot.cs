using UnityEngine;

public class PlayerAttackshoot : MonoBehaviour
{
    [Header("Gun Variable")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    void Update() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ·› «··«⁄» Õ”» „Êﬁ⁄ «·„«Ê”
        if (mouseWorldPos.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0); // ·› «··«⁄» ··Ì”«—
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // ·› «··«⁄» ··Ì„Ì‰
        }

        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void Shoot()
    {
        GameObject bullet= Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Destroy(bullet, 5f);
    }
}


