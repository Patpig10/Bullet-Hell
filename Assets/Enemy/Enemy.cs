using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator hitAnimator;

    public int health = 1;
    public Rigidbody rb;
    public FrostStatus frostStatus;
    public BurningStatus burningStatus;
    public int damage = BulletController.damage;
    public int originalMoveSpeed;
    private ChasePlayer chasePlayer;
    public bool isBurning = false;
    public bool isFrost = false;
    public bool isElectric = false;
    public bool isWet = false;
    public bool isShocked = false;
    public GameObject explosionPrefab;
    public DamageAura damageAura;

    void Start()
    {
        frostStatus = GetComponent<FrostStatus>();
        burningStatus = GetComponent<BurningStatus>();

        chasePlayer = FindObjectOfType<ChasePlayer>();
        originalMoveSpeed = (int)chasePlayer.MoveSpeed;

    }

    // ...


    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Enemy health: " + health);
        }
    }
    private IEnumerator SetIsFrostForDuration(float duration)
    {
        isFrost = true;
        yield return new WaitForSeconds(duration);
        isFrost = false;
    }
    private IEnumerator SetIsBurningForDuration(float duration)
    {
        isBurning = true;
        yield return new WaitForSeconds(duration);
        isBurning = false;
    }

    private IEnumerator SetIsElectricForDuration(float duration)
    {
        isElectric = true;
        yield return new WaitForSeconds(duration);
        isElectric = false;
    }

    private IEnumerator SetIsWetForDuration(float duration)
    {
        isWet = true;
        yield return new WaitForSeconds(duration);
        isWet = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            int damage = BulletController.damage;
            TakeDamage(damage);
        }
        if (other.CompareTag("Ice"))
        {
            int damage = BulletController.damage;
            TakeDamage(damage);

            if (Random.value < 0.5f && frostStatus != null)
            {
                frostStatus.ApplyFrostEffect();
                Debug.Log("Enemy frozen!");

                StartCoroutine(SetIsFrostForDuration(5f));

                float moveSpeed = chasePlayer.MoveSpeed * 0.5f;
                chasePlayer.MoveSpeed = (int)moveSpeed;
            }
        }
        else if (other.CompareTag("Fire"))
        {
            int damage = BulletController.damage;
            TakeDamage(damage);

            if (Random.value < 1f && burningStatus != null)
            {
                

                burningStatus.StartBurning();
                Debug.Log("Enemy burning!");
                StartCoroutine(SetIsBurningForDuration(5f));

            }
            if(isBurning && isFrost)
            {
                StartCoroutine(SetIsWetForDuration(5f));

            }
        }
        else
        {
            // regular bullet damage
        }

        if (other.CompareTag("Ice"))
        {
            if (Random.value < 1f && frostStatus != null)
            {
                frostStatus.ApplyFrostEffect();

                float moveSpeed = chasePlayer.MoveSpeed * 0.5f;
                chasePlayer.MoveSpeed = (int)moveSpeed;
            }
            if (burningStatus != null)
            {
                burningStatus.StopBurning();
            }
            if (isBurning && isFrost)
            {
                StartCoroutine(SetIsWetForDuration(5f));

            }
        }
        if (other.CompareTag("Lightening"))
        {
            int damage = BulletController.damage;

            TakeDamage(damage);
            if (isElectric && isBurning)
            {
                StartCoroutine(SetIsElectricForDuration(5f));

                Debug.Log("Explosion!");
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            isElectric = true;
            if(isElectric && isWet)
            {
                if (damageAura != null)
                {
                    // Apply damage aura effect to self
                    StartCoroutine(damageAura.ApplyDamageAura(this));
                }
            }
        }

            if (other.CompareTag("Fire"))
        {
            if (Random.value < 1f && burningStatus != null)
            {
                StartCoroutine(SetIsBurningForDuration(5f));
                burningStatus.StartBurning();
                Debug.Log("Enemy burning!");
            }
            if (frostStatus != null)
            {
                frostStatus.RemoveFrostEffect();
                chasePlayer.MoveSpeed = originalMoveSpeed;
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);

        FindObjectOfType<Guncontroller>().currentAmmo++;
        FindObjectOfType<Guncontroller>().UpdateAmmoUI();
    }
}
