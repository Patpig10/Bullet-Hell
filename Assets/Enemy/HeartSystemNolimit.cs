using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class HeartSystemNolimit : MonoBehaviour
{
    public GameObject[] hearts;
    private int life;
    private int maxLife;
    private bool dead;
    public PointManager pointManager;
    public int healingCost = 10;

    private void Start()
    {
        life = hearts.Length;
        maxLife = life;
    }

    void Update()
    {
        if (dead == true)
        {
            // set gameover scene or reload level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload level code
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (pointManager.currentPoints >= healingCost && life < maxLife)
            {
                pointManager.DeductPoints(healingCost);
                AddLife();
            }
        }
    }
    public void TakeDamage(int d)
    {
        life -= d;
        //Destroy (hearts[life].gameObject);
        hearts[life].gameObject.SetActive(false);
        if (life < 1)
        {
            dead = true;
        }
    }
    public void AddLife()
    {
        if (life < maxLife)
        {
            hearts[life].gameObject.SetActive(true);
            life += 1;
        }


    }
    /*  void OnTriggerEnter(Collider other)

      {

          Bullet bullet = other.GetComponent<Bullet>();

          if (bullet != null)
          {
              TakeDamage(1);
          }
         // Destroy(gameObject);

      }*/

}
