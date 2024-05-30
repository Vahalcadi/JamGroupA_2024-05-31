
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoveringText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] TMP_Text ButtonText;
    Color BaseColor;
    [SerializeField] Image cursor;
    [SerializeField] Color HoverColor;

    // Start is called before the first frame update
    void Start()
    {
        BaseColor = ButtonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonText.color = HoverColor;
        cursor?.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonText.color = BaseColor;
        cursor?.gameObject.SetActive(false);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonText.color = BaseColor;
        cursor?.gameObject.SetActive(false);
    }
}
