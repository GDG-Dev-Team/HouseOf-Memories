using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public int Health;

    void Awake()
    {
        if (!instance)
            instance = this;
    }




    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Health--;

            if (Health == 0)
            {
                Destroy(gameObject);
                Time.timeScale = 0;
                SceneManage.instance.LoadMenu("Lose Menu");
            }
        }
        switch (Health)
        {
            case 0:
            {
                health1.gameObject.SetActive(false);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);
                break;
            }
            case 1:
            {
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);
                break;
            }
            case 2:
            {
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(false);
                break;
            }
            case 3:
            {
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(true);

                break;
            }
        }


    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Health--;

            if (Health == 0)
            {
                Destroy(gameObject);
                Time.timeScale = 0;
                SceneManage.instance.LoadMenu("Lose Menu");
            }
        }
        switch (Health)
        {
            case 0:
                {
                    health1.gameObject.SetActive(false);
                    health2.gameObject.SetActive(false);
                    health3.gameObject.SetActive(false);
                    break;
                }
            case 1:
                {
                    health1.gameObject.SetActive(true);
                    health2.gameObject.SetActive(false);
                    health3.gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    health1.gameObject.SetActive(true);
                    health2.gameObject.SetActive(true);
                    health3.gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    health1.gameObject.SetActive(true);
                    health2.gameObject.SetActive(true);
                    health3.gameObject.SetActive(true);

                    break;
                }
        }


    }
}
