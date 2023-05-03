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
    [SerializeField] private ParticleSystem burningParticles;

    void Start()
    {
        if (burningParticles != null)
        {
            burningParticles.Stop();
        }
        frostStatus = GetComponent<FrostStatus>();
        burningStatus = GetComponent<BurningStatus>();

        chasePlayer = FindObjectOfType<ChasePlayer>();
        originalMoveSpeed = (int)chasePlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        chasePlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 5f; // set the initial speed

        if (damageAura != null)
        {
            damageAura.StartCoroutine(damageAura.ApplyDamageAura(this));
        }
    }


    // ...


    void Update()
    {
        if (burningParticles != null && burningParticles.isPlaying && (burningStatus == null || !burningStatus.IsActive()))
        {
            burningParticles.Stop();
        }
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
        if (burningParticles != null && !burningStatus.IsActive())
        {
            burningParticles.Stop();
        }
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

                float moveSpeed = chasePlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().speed * 0.5f;
                chasePlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = moveSpeed;
            }
        }
        else if (other.CompareTag("Fire"))
        {
            int damage = BulletController.damage;
            TakeDamage(damage);

            if (Random.value < 1f && burningStatus != null)
            {
                burningParticles.Play();

                burningStatus.StartBurning();
                Debug.Log("Enemy burning!");
                StartCoroutine(SetIsBurningForDuration(5f));
                if (burningParticles != null && !burningStatus.IsActive())
                {
                    burningParticles.Play();
                }

            }

            if (isBurning && isFrost)
            {
                StartCoroutine(SetIsWetForDuration(5f));
            }
        }
        else if (other.CompareTag("Lightening"))
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
            if (isElectric && isWet)
            {
                if (damageAura != null)
                {
                  
                    StartCoroutine(damageAura.ApplyDamageAura(this));
                }
            }
        }
        if (other.CompareTag("Fire"))
        {
            if (Random.value < 1f && burningStatus != null)
            {
               
                    burningParticles.Play();
                
                StartCoroutine(SetIsBurningForDuration(5f));
                burningStatus.StartBurning();
                Debug.Log("Enemy burning!");
            }
            if (frostStatus != null)
            {
                frostStatus.RemoveFrostEffect();
                chasePlayer.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = originalMoveSpeed;
            }
        }
        else if (other.CompareTag("Blade"))
        {
            TakeDamage(3);
        }
        else
        {
            // regular bullet damage
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
