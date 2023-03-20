using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guncontroller : MonoBehaviour
{
    public bool isFiring;
    public List<BulletController> bullets = new List<BulletController>();
    private int currentBulletIndex = 0;
    public float bulletSpeed;
    private float shotTimeCounter = 0.0f;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;

    public Image[] ammoUI;
    public int maxAmmo = 7;
    public int currentAmmo = 7;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Switch between bullets using mouse scroll wheel
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0)
        {
            currentBulletIndex--;
            if (currentBulletIndex < 0)
            {
                currentBulletIndex = bullets.Count - 1;
            }
        }
        else if (scrollWheel < 0)
        {
            currentBulletIndex++;
            if (currentBulletIndex >= bullets.Count)
            {
                currentBulletIndex = 0;
            }
        }

        if (Input.GetMouseButtonDown(0))
            isFiring = true;

        if (Input.GetMouseButtonUp(0))
            isFiring = false;

        shotTimeCounter += Time.deltaTime;
        if (isFiring && currentAmmo > 0)
        {
            if (shotTimeCounter > timeBetweenShots)
            {
                shotTimeCounter = 0.0f;
                BulletController newBullet = Instantiate(bullets[currentBulletIndex], firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                currentAmmo--;
                UpdateAmmoUI();
            }
        }
    }

    public void UpdateAmmoUI()
    {
        for (int i = 0; i < ammoUI.Length; i++)
        {
            if (i < currentAmmo)
            {
                ammoUI[i].enabled = true;
            }
            else
            {
                ammoUI[i].enabled = false;
            }
        }
    }

}
