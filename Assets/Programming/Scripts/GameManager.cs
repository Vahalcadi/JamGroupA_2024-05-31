using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("The zone that only opens when the player has all the flames")]
    [SerializeField] private GameObject endZone;

    [Header("Must be equal to the number of flames GameObject inside the game")]
    [SerializeField] private int totalNumberOfFlames;

    private int numberOfFlamesCollected = 0;

    public int NumberOfFlamesCollected { get { return numberOfFlamesCollected; } }
    public int TotalNumberOfFlames { get { return totalNumberOfFlames; } }


    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        Debug.Log("Game Over, You win");  
    }

    public void CollectFlame()
    {
        numberOfFlamesCollected++;
    }
}
