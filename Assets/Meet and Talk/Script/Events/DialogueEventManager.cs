using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MEET_AND_TALK
{
    public class DialogueEventManager : MonoBehaviour
    {
        public static DialogueEventManager Instance { get; private set; }

        public Action ConsoleDebug;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ConsoleDebug += DoConsoleLogEvent;
        }

        private void OnDestroy()
        {
            ConsoleDebug -= DoConsoleLogEvent;
        }

        private void DoConsoleLogEvent()
        {
            Debug.Log("Dialogue Event Log");
        }

        public void CallConsoleDebug()
        {
            ConsoleDebug?.Invoke();
        }
    }
}
