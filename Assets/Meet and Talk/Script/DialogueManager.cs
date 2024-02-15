using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MEET_AND_TALK
{
    public class DialogueManager : DialogueGetData
    {
        public static DialogueManager Instance;
        public LocalizationManager _manager;

        public DialogueUIManager dialogueController;
        public AudioSource audioSource;

        public UnityEvent StartDialogueEvent;
        public UnityEvent EndDialogueEvent;

        private BaseNodeData currentDialogueNodeData;
        private BaseNodeData lastDialogueNodeData;

        private DialogueNodeData _nodeDialogueInvoke;
        private DialogueChoiceNodeData _nodeChoiceInvoke;

        float Timer;

        private void Awake()
        {
            Instance = this;
            dialogueController = DialogueUIManager.Instance;
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            Timer -= Time.deltaTime;
            if (Timer > 0) dialogueController.TimerSlider.value = Timer;
        }

        public void StartDialogue(DialogueContainerSO dialogue)
        {
            dialogueController = DialogueUIManager.Instance;
            dialogueContainer = dialogue;
            CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[0]));
            dialogueController.dialogueUI.SetActive(true);
            StartDialogueEvent.Invoke();
        }
        public void StartDialogue()
        {
            dialogueController = DialogueUIManager.Instance;
            CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[0]));
            dialogueController.dialogueUI.SetActive(true);
            StartDialogueEvent.Invoke();
        }

        public void CheckNodeType(BaseNodeData _baseNodeData)
        {
            switch (_baseNodeData)
            {
                case StartNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case DialogueNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case DialogueChoiceNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case EndNodeData nodeData:
                    RunNode(nodeData);
                    break;
                default:
                    break;
            }
        }


        private void RunNode(StartNodeData _nodeData)
        {
            CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[0]));
        }
        private void RunNode(DialogueNodeData _nodeData)
        {
            lastDialogueNodeData = currentDialogueNodeData;
            currentDialogueNodeData = _nodeData;

            dialogueController.textBox.text = $"<color={_nodeData.Character.HexColor()}>{_nodeData.Character.characterName.Find(text => text.languageEnum == _manager.SelectedLang()).LanguageGenericType}: </color>";
            dialogueController.textBox.text += $"{_nodeData.TextType.Find(text => text.languageEnum == _manager.SelectedLang()).LanguageGenericType}";

            MakeButtons(new List<DialogueNodePort>());

            audioSource.PlayOneShot(_nodeData.AudioClips.Find(clip => clip.languageEnum == _manager.SelectedLang()).LanguageGenericType);

            _nodeDialogueInvoke = _nodeData;

            IEnumerator tmp() { yield return new WaitForSeconds(_nodeData.Duration); DialogueNode_NextNode(); }
            StartCoroutine(tmp());
        }
        private void RunNode(DialogueChoiceNodeData _nodeData)
        {
            lastDialogueNodeData = currentDialogueNodeData;
            currentDialogueNodeData = _nodeData;

            dialogueController.textBox.text = $"<color={_nodeData.Character.HexColor()}>{_nodeData.Character.characterName.Find(text => text.languageEnum == _manager.SelectedLang()).LanguageGenericType}: </color>";
            dialogueController.textBox.text += $"{_nodeData.TextType.Find(text => text.languageEnum == _manager.SelectedLang()).LanguageGenericType}";

            MakeButtons(new List<DialogueNodePort>());

            _nodeChoiceInvoke = _nodeData;

            IEnumerator tmp() { yield return new WaitForSeconds(_nodeData.Duration); ChoiceNode_GenerateChoice(); }
            StartCoroutine(tmp());

            audioSource.PlayOneShot(_nodeData.AudioClips.Find(clip => clip.languageEnum == _manager.SelectedLang()).LanguageGenericType);
        }
        private void RunNode(EndNodeData _nodeData)
        {
            switch (_nodeData.EndNodeType)
            {
                case EndNodeType.End:
                    dialogueController.dialogueUI.SetActive(false);
                    EndDialogueEvent.Invoke();
                    break;
                case EndNodeType.Repeat:
                    CheckNodeType(GetNodeByGuid(currentDialogueNodeData.NodeGuid));
                    break;
                case EndNodeType.GoBack:
                    CheckNodeType(GetNodeByGuid(lastDialogueNodeData.NodeGuid));
                    break;
                case EndNodeType.ReturnToStart:
                    CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[0]));
                    break;
                default:
                    break;
            }
        }

        private void MakeButtons(List<DialogueNodePort> _nodePorts)
        {
            List<string> texts = new List<string>();
            List<UnityAction> unityActions = new List<UnityAction>();

            foreach (DialogueNodePort nodePort in _nodePorts)
            {
                Debug.Log("Test");
                texts.Add(nodePort.TextLanguage.Find(text => text.languageEnum == _manager.SelectedLang()).LanguageGenericType);
                UnityAction tempAction = null;
                tempAction += () =>
                {
                    CheckNodeType(GetNodeByGuid(nodePort.InputGuid));
                };
                unityActions.Add(tempAction);
            }

            dialogueController.SetButtons(texts, unityActions, false);
        }

        void DialogueNode_NextNode() { CheckNodeType(GetNextNode(_nodeDialogueInvoke)); }
        void ChoiceNode_GenerateChoice() { MakeButtons(_nodeChoiceInvoke.DialogueNodePorts); }
    }
}
