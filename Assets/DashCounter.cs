using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCounter : MonoBehaviour
{
    public int maxDashes = 1; // maximum number of dashes
    public float dashSpeed = 20f; // speed of the dash
    private int currentDashes; // current number of dashes
    private bool isDashing; // check if the player is dashing
    private float dashDuration = 0.2f; // duration of the dash

    void Start()
    {
        currentDashes = maxDashes;
    }

    public bool CanDash()
    {
        return currentDashes > 0;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public void Dash()
    {
        if (CanDash())
        {
            isDashing = true;
            currentDashes--;
            StartCoroutine(DashCoroutine());
        }
    }
    IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        Vector3 direction = transform.forward;
        while (Time.time < startTime + dashDuration)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f); // add a small delay here
        isDashing = false;
    }


    public void ResetDashes()
    {
        currentDashes = maxDashes;
    }

    public void StartDash()
    {
        if (CanDash())
        {
            Dash();
        }
    }
}
