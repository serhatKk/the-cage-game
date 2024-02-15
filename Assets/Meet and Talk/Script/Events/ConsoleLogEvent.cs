using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MEET_AND_TALK
{
    [CreateAssetMenu(menuName = "Dialogue/Event/Console Log")]
    [System.Serializable]
    public class ConsoleLogEvent : DialogueEventSO
    {
        public override void RunEvent()
        {
            DialogueEventManager.Instance.ConsoleDebug();
            base.RunEvent();
        }
    }
}
