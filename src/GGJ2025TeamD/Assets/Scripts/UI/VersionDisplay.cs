using TMPro;
using UnityEngine;

public class VersionDisplay : MonoBehaviour {
    public TMP_Text text;

    void OnEnable() {
        text.text = Application.version;
    }
}
