using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("The zone that only opens when the player has all the flames")]
    [SerializeField] private GameObject endZone;

    [Header("Must be equal to the number of flames GameObject inside the game")]
    [SerializeField] private int totalNumberOfFlames;

    [Header("Game Over Window for the prototype")]
    [SerializeField] GameObject gameOverWindow; //added for the prototype

    private int numberOfFlamesCollected = 0;

    public int numberOfKeysCollected = 0; //added for the prototype

    public int NumberOfFlamesCollected { get { return numberOfFlamesCollected; } }
    public int TotalNumberOfFlames { get { return totalNumberOfFlames; } }


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(numberOfKeysCollected);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(numberOfKeysCollected);

        if (Input.GetKeyDown(KeyCode.Escape)) // added for the prototype
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over, You win");
        gameOverWindow.SetActive(true); //added for the prototype
    }

    public void CollectFlame()
    {
        numberOfFlamesCollected++;
    }

    public void CollectKey()
    {
        numberOfKeysCollected++;
    }
}
