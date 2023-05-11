using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;

    public TextMeshProUGUI pointsText;
    public int currentPoints = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
        UpdatePointsText();
    }

    public void DeductPoints(int points)
    {
        currentPoints -= points;
        UpdatePointsText();
    }

    public void ResetPoints()
    {
        currentPoints = 0;
        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + currentPoints.ToString();
        }
    }
}
