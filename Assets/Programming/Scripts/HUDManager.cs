using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] public FriendlyFlames[] flamesArray = new FriendlyFlames[3];
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

    public void SpriteUpdate()
    {
        for(int i = 0; i < flamesArray.Length; i++)
        {
            if (flamesArray[i].isCollected == true)
            {
                flamesImagesArray[i].SetActive(true);
            }
        }
    }

    public void HealthBarUpdate()
    {
        fillHealth.fillAmount = playerCall.playerHealth / startHealth;
    }

    public void KeyTextUpdate()
    {
        keysText.text = gameManager.numberOfKeysCollected.ToString();
    }
}




