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
    public GameObject elementBlock;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAmmoUI();
        image = elementBlock.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentBulletIndex = 0;
            // Change color to yellow
            image.color = Color.yellow;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentBulletIndex = 1;
            // Change color to blue
            image.color = Color.blue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentBulletIndex = 2;
            // Change color to red
            image.color = Color.red;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentBulletIndex = 3;
            // Change color to purple
            image.color = new Color(0.5f, 0, 0.5f);
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
    public void AmmonBoost()
    {
        FindObjectOfType<Guncontroller>().currentAmmo++;
        FindObjectOfType<Guncontroller>().UpdateAmmoUI();
    }

}
