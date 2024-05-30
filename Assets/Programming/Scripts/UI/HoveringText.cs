using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoveringText : MonoBehaviour
{
    [SerializeField] TMP_Text ButtonText;
    Color BaseColor;
    [SerializeField] Color HoverColor;

    // Start is called before the first frame update
    void Start()
    {
        BaseColor = ButtonText.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterColorChange()
    {
        ButtonText.color = HoverColor;
    }

    public void ExitColorChange()
    {
        ButtonText.color = BaseColor;
    }
}
