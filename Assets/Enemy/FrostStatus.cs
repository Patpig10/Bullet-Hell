using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostStatus : MonoBehaviour
{
    public float frostDuration = 5f;
    public float frostDamage = 5f;

    private float frostEndTime;

    public void ApplyFrostEffect()
    {
        frostEndTime = Time.time + frostDuration;
        StartCoroutine(ApplyDamage());
    }

    private IEnumerator ApplyDamage()
    {
        while (Time.time < frostEndTime)
        {
            yield return new WaitForSeconds(1f);
            //GetComponent<Enemy>().TakeDamage(frostDamage);
        }
        ClearFrostEffect();
    }

    public void ClearFrostEffect()
    {
        StopAllCoroutines();
        Destroy(this);
    }

    public void RemoveFrostEffect()
    {
        ClearFrostEffect();
    }
}
