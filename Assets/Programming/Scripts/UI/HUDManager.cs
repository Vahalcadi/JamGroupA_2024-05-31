using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] private TextMeshProUGUI keyCount;
    public static HUDManager Instance;
    private Player playerInstance;

    [SerializeField] List<Image> friendlyFlames;

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;

        playerInstance = PlayerManager.Instance.Player;
    }

    private void Start()
    {
        hpSlider.maxValue = playerInstance.MaxHealth;
        hpSlider.value = hpSlider.maxValue;
    }

    public void UpdateHealthBar()
    {
        hpSlider.value = playerInstance.CurrentHP;
    }

    public void KeyTextUpdate()
    {
        keyCount.text = $"{GameManager.Instance.NumberOfKeysCollected}";
    }

    public void UpdateFlameSpriteUI(int flameNumber)
    {
        int index = flameNumber - 1;
        friendlyFlames[index].color = Color.white;
    }

    /*public void HealthBarUpdate()
    {
        fillHealth.fillAmount = playerCall.playerHealth / startHealth;
    }*/


    /*[SerializeField] public FriendlyFlames[] flamesArray = new FriendlyFlames[3];
    [SerializeField] public GameObject[] flamesImagesArray = new GameObject[3];
    [SerializeField] public Image fillHealth;
    [SerializeField] public Player playerCall;
    [SerializeField] public TMP_Text keysText;
    [SerializeField] GameManager gameManager;

    float startHealth;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = playerCall.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteUpdate();
        HealthBarUpdate();
        KeyTextUpdate();
    } 

    */
}




