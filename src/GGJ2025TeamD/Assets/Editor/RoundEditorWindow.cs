using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class RoundEditorWindow : UnityEditor.EditorWindow
    {
        [UnityEditor.MenuItem("Tools/Round Editor")]
        private static void ShowWindow()
        {
            var window = GetWindow<RoundEditorWindow>();
            window.titleContent = new UnityEngine.GUIContent("Round");
            window.Show();
        }

        private void CreateGUI()
        {
            
            var roundEndAmount = new Slider("Round End Amount")
                { value = 0, lowValue = 0, highValue = 100, showInputField = true };
            rootVisualElement.Add(roundEndAmount);
            
            
            
            var roundEndButton = new Button(() =>
                {
                    GlobalEvent.RaiseRoundEnd();
                })
                { text = "Raise Round End" };
            rootVisualElement.Add(roundEndButton);
        }
    }
}