using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GateFinal : MonoBehaviour
{
    public int requiredPoints = 100;
    public float messageDisplayDuration = 3f;

    public TextMeshProUGUI messageText;

    private bool isGateActive = true;
    private bool isPlayerNearby = false;
    private bool isMessageDisplayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (!isMessageDisplayed && isGateActive) // Check if the gate is active
            {
                DisplayMessage();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            HideMessage();
        }
    }

    private void Update()
    {
        if (isPlayerNearby && PointManager.instance.currentPoints >= requiredPoints)
        {
            isGateActive = false;
            gameObject.SetActive(false);
            messageText.gameObject.SetActive(false); // Hide the messageText object
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    private void DisplayMessage()
    {
        if (messageText != null && isGateActive) // Check if the gate is active
        {
            int pointsNeeded = requiredPoints - PointManager.instance.currentPoints;
            string message = "You need " + pointsNeeded + " more points to open the gate.";
            messageText.text = message;
            messageText.gameObject.SetActive(true);
            StartCoroutine(HideMessageDelayed());
            isMessageDisplayed = true;
        }
    }

    private IEnumerator HideMessageDelayed()
    {
        yield return new WaitForSeconds(messageDisplayDuration);
        HideMessage();
    }

    private void HideMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
            isMessageDisplayed = false;
        }
    }
}
