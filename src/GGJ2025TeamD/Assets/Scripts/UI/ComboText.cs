using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ComboText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text? text;
    [SerializeField]
    private string scoreText = "Combo: ";

    private void Start()
    {
        GlobalEvent.OnComboChange += SetCombo;
    }

    private void OnDestroy()
    {
        GlobalEvent.OnComboChange -= SetCombo;
    }

    private void SetCombo(int combo)
    {
        if (text != null)
        {
            text.text = scoreText + combo.ToString();
        }
    }
}
