using UnityEngine;

public class PinResetter : MonoBehaviour
{
    private Vector3[] pinPositions;
    private Quaternion[] pinRotations;
    public GameObject[] pins;
    private int score = 0;

    void Start()
    {
        pinPositions = new Vector3[pins.Length];
        pinRotations = new Quaternion[pins.Length];

        for (int i = 0; i < pins.Length; i++)
        {
            pinPositions[i] = pins[i].transform.position;
            pinRotations[i] = pins[i].transform.rotation;
        }
    }

    public void CalculateScore()
    {
        int frameScore = 0;
        foreach (GameObject pin in pins)
        {
            if (IsPinKnockedOver(pin))
            {
                frameScore++;
            }
        }
        score += frameScore;
        Debug.Log("Frame Score: " + frameScore + " | Total Score: " + score);
    }

    private bool IsPinKnockedOver(GameObject pin)
    {
        return Vector3.Angle(Vector3.up, pin.transform.up) > 30f;
    }

    public void ResetPins()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].transform.position = pinPositions[i];
            pins[i].transform.rotation = pinRotations[i];
            Rigidbody rb = pins[i].GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
