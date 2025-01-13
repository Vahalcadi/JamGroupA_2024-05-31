using System.Data.Common;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Must be equal to the number of flames GameObject inside the game")]
    [SerializeField] private int totalNumberOfFlames;

    private int numberOfFlamesCollected = 0;

    private int numberOfKeysCollected = 0;

    public int NumberOfFlamesCollected { get { return numberOfFlamesCollected; } }
    public int NumberOfKeysCollected { get { return numberOfKeysCollected; } }

    public int TotalNumberOfFlames { get { return totalNumberOfFlames; } }


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        PauseGame(false);
    }


    private void Update()
    {
        //DEBUG: send player at the boss door and give him all flames
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerManager.Instance.Player.transform.position = new Vector3(-6, 0.58f, 156.4f);
            numberOfFlamesCollected = 3;
            HUDManager.Instance.UpdateFlameSpriteUI(1);
            HUDManager.Instance.UpdateFlameSpriteUI(2);
            HUDManager.Instance.UpdateFlameSpriteUI(3);
        }
    }

    public void EndGame(string dialog)
    {
        PauseGame(true);

        QuestionDialogUI.Instance.ShowQuestion(dialog,
            () =>
            {
                PauseGame(false);
                SceneManager.LoadScene(GameMenu.Instance.currentSceneName);
            },
            () =>
            {
                QuestionDialogUI.Instance.ShowQuestion("This will close the game, are you sure?",
                    () =>
                    {
                        Application.Quit();
                    },
                    () =>
                    {
                        EndGame(dialog);
                    });
            });
    }

    public virtual void PauseGame(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0;
            InputManager.Instance.OnDisable();
            Cursor.visible = true;
        }
        else
        {
            InputManager.Instance.OnEnable();
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }

    public void CollectFlame()
    {
        numberOfFlamesCollected++;
    }

    public void UseKey()
    {
        numberOfKeysCollected--;
        HUDManager.Instance.KeyTextUpdate();
    }

    public void CollectKey()
    {
        numberOfKeysCollected++;
        HUDManager.Instance.KeyTextUpdate();
    }
}
