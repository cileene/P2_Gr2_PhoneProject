using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Display the value of a slider in a TextMeshProUGUI component
public class SliderToText : MonoBehaviour
{
    [SerializeField, Range(0, 99)] public int number;
    public Slider slider;
    public TextMeshProUGUI text;
    
    private void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        if (slider) number = (int)slider.value;

        if (text) text.text = number.ToString("F0");
    }
}