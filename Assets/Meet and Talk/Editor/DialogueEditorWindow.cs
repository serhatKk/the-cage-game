#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;

namespace MEET_AND_TALK
{
    [ExecuteInEditMode]
    public class DialogueEditorWindow : EditorWindow
    {
        private DialogueContainerSO currentDialogueContainer;
        private DialogueGraphView graphView;
        private DialogueSaveAndLoad saveAndLoad;

        private LocalizationEnum languageEnum = LocalizationEnum.English;
        private Label nameOfDialogueContainer;
        private ToolbarMenu toolbarMenu;
        private ToolbarMenu toolbarMenu2;

        public LocalizationEnum LanguageEnum { get => languageEnum; set => languageEnum = value; }

        [OnOpenAsset(1)]
        public static bool ShowWindow(int _instanceId, int line)
        {
            UnityEngine.Object item = EditorUtility.InstanceIDToObject(_instanceId);

            if (item is DialogueContainerSO)
            {
                DialogueEditorWindow window = (DialogueEditorWindow)GetWindow(typeof(DialogueEditorWindow));
                window.titleContent = new GUIContent("Dialogue Editor", EditorGUIUtility.FindTexture("d_Favorite Icon"));
                window.currentDialogueContainer = item as DialogueContainerSO;
                window.minSize = new Vector2(500, 250);
                window.Load();
            }

            return false;
        }

        private void OnEnable()
        {
            ContructGraphView();
            GenerateToolbar();
            Load();
        }
        private void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        private void ContructGraphView()
        {
            graphView = new DialogueGraphView(this);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);

            saveAndLoad = new DialogueSaveAndLoad(graphView);
        }
        private void GenerateToolbar()
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("Themes/DefualtTheme");
            rootVisualElement.styleSheets.Add(styleSheet);
            //rootVisualElement.styleSheets.Clear();
            /*switch (Resources.Load<MeetAndTalkSettings>("MeetAndTalkSettings").Theme)
            {
                case MeetAndTalkTheme.Dark:
                    rootVisualElement.styleSheets.Add(Resources.Load<StyleSheet>("Themes/DarkTheme"));
                    break;
                default:
                    rootVisualElement.styleSheets.Add(Resources.Load<StyleSheet>("Themes/LightTheme"));
                    break;
            }*/

            Toolbar toolbar = new Toolbar();

            ToolbarButton saveBtn = new ToolbarButton()
            {
                text = "Save Dialog"
            };
            saveBtn.clicked += () =>
            {
                Save();
            };
            toolbar.Add(saveBtn);

            ToolbarButton loadBtn = new ToolbarButton()
            {
                text = "Load Last Save",
            };
            loadBtn.clicked += () =>
            {
                Load();
            };
            toolbar.Add(loadBtn);

            toolbarMenu = new ToolbarMenu();
            foreach (LocalizationEnum language in (LocalizationEnum[])Enum.GetValues(typeof(LocalizationEnum)))
            {
                toolbarMenu.menu.AppendAction(language.ToString(), new Action<DropdownMenuAction>(x => Language(language, toolbarMenu)));
            }
            toolbar.Add(toolbarMenu);

            nameOfDialogueContainer = new Label("");
            toolbar.Add(nameOfDialogueContainer);
            nameOfDialogueContainer.AddToClassList("nameOfDialogueContainer");

            rootVisualElement.Add(toolbar);
        }
        private void Load()
        {
            if (currentDialogueContainer != null)
            {
                Language(LocalizationEnum.English, toolbarMenu);
                nameOfDialogueContainer.text = "" + currentDialogueContainer.name;
                saveAndLoad.Load(currentDialogueContainer);
            }
        }
        private void Save()
        {
            if (currentDialogueContainer != null)
            {
                saveAndLoad.Save(currentDialogueContainer);
            }
            Debug.Log("Save");
        }
        private void Language(LocalizationEnum _language, ToolbarMenu _toolbarMenu)
        {
            toolbarMenu.text = "Language: " + _language.ToString() + "   ";
            languageEnum = _language;
            graphView.LanguageReload();
        }
    }
}
#endif