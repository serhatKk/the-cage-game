using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace MEET_AND_TALK
{
    public class DialogueUIManager : MonoBehaviour
    {
        public static DialogueUIManager Instance;

        [Header("Dialogue Canvas")]
        public GameObject dialogueUI;

        [Header("Dialogue Static Element")]
        public TextMeshProUGUI textBox;
        public Slider TimerSlider;

        [Header("Dialogue Dynamic Element")]
        public Button button01;
        public TextMeshProUGUI buttonText01;
        public Button button02;
        public TextMeshProUGUI buttonText02;
        public Button button03;
        public TextMeshProUGUI buttonText03;
        public Button button04;
        public TextMeshProUGUI buttonText04;

        private List<Button> buttons = new List<Button>();
        private List<TextMeshProUGUI> buttonsTexts = new List<TextMeshProUGUI>();



        private void Awake()
        {
            Instance = this;

            buttons.Add(button01);
            buttons.Add(button02);
            buttons.Add(button03);
            buttons.Add(button04);

            buttonsTexts.Add(buttonText01);
            buttonsTexts.Add(buttonText02);
            buttonsTexts.Add(buttonText03);
            buttonsTexts.Add(buttonText04);
        }

        public void SetButtons(List<string> _texts, List<UnityAction> _unityActions, bool showTimer)
        {
            buttons.ForEach(button => button.gameObject.SetActive(false));

            for (int i = 0; i < _texts.Count; i++)
            {
                buttonsTexts[i].text = _texts[i];
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick = new Button.ButtonClickedEvent();
                buttons[i].onClick.AddListener(_unityActions[i]);
                Debug.Log(_unityActions[i]);
            }

            TimerSlider.gameObject.SetActive(showTimer);
        }

    }
}
