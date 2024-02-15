using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace MEET_AND_TALK
{
    public class BaseNode : Node
    {
        public string nodeGuid;
        protected DialogueGraphView graphView;
        protected DialogueEditorWindow editorWindow;
        protected Vector2 defualtNodeSize = new Vector2(200, 250);

        protected string NodeGrid { get => NodeGrid; set => nodeGuid = value; }

        public BaseNode()
        {
            /*switch (Resources.Load<MeetAndTalkSettings>("MeetAndTalkSettings").Theme)
            {
                case MeetAndTalkTheme.Dark:
                    styleSheets.Add(Resources.Load<StyleSheet>("Themes/DarkTheme"));
                    break;
                default:
                    styleSheets.Add(Resources.Load<StyleSheet>("Themes/LightTheme"));
                    break;
            }*/
            StyleSheet styleSheet = Resources.Load<StyleSheet>("Themes/DefualtTheme");
            styleSheets.Add(styleSheet);
        }

        public void AddOutputPort(string name, Port.Capacity capality = Port.Capacity.Single)
        {
            Port outputPort = GetPortInstance(Direction.Output, capality);
            outputPort.portName = name;
            outputContainer.Add(outputPort);
        }

        public void AddInputPort(string name, Port.Capacity capality = Port.Capacity.Single)
        {
            Port inputPort = GetPortInstance(Direction.Input, capality);
            inputPort.portName = name;
            inputContainer.Add(inputPort);
        }

        public Port GetPortInstance(Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }

        public virtual void LoadValueInToField()
        {

        }
    }
}