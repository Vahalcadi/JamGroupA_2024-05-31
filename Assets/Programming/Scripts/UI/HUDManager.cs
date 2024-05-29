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
    public List<Image> dashImages;

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

    public void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount >= 1)
            _image.fillAmount = 0;
    }

    public IEnumerator CheckCooldownOf(Image _image, float _cooldown)
    {
        while (_image.fillAmount < 1)
        {
            _image.fillAmount += 1 / _cooldown * Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}




