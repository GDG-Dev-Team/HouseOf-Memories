using UnityEngine;
using System.Collections;

public class DropThroughPlatform : MonoBehaviour
{ private GameObject currentPlatform;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPlatform != null)
            {
                StartCoroutine(DisableCollisionTemporarily());
            }
        }
    }

    IEnumerator DisableCollisionTemporarily()
    {
        PlatformEffector2D effector = currentPlatform.GetComponent<PlatformEffector2D>();
        if (effector != null)
        {
            Collider2D platformCollider = currentPlatform.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
            yield return new WaitForSeconds(0.5f); // Enough time to fall through
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlatform)
        {
            currentPlatform = null;
        }
    }
}
