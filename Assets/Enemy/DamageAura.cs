using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAura : MonoBehaviour
{
    public Vector3 auraSize = new Vector3(3f, 3f, 3f);
    public int auraDamage = 1;
    public float auraDuration = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply damage aura effect to enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                StartCoroutine(ApplyDamageAura(enemy));
            }
        }
    }

    public IEnumerator ApplyDamageAura(Enemy enemy)
    {
        // Apply damage aura effect to enemy
        enemy.TakeDamage(auraDamage);

        // Wait for the duration of the aura
        float elapsedTime = 0f;
        while (elapsedTime < auraDuration)
        {
            elapsedTime += Time.deltaTime;

            // Check if enemy is still within aura range
            if (!IsEnemyWithinAura(enemy))
            {
                yield break; // Exit coroutine if enemy is out of range
            }

            yield return null; // Wait for next frame
        }

        // Reapply damage aura effect
        StartCoroutine(ApplyDamageAura(enemy));
    }

    private bool IsEnemyWithinAura(Enemy enemy)
    {
        // Check if enemy is within aura range
        Collider[] colliders = Physics.OverlapBox(transform.position, auraSize / 2f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == enemy.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, auraSize);
    }
}
