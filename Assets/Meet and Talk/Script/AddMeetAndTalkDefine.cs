using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace MEET_AND_TALK
{
#if UNITY_EDITOR

    [InitializeOnLoad]
    public class AddMeetAndTalkDefine : Editor
    {
        public static readonly string[] Symbols = new string[] {
         "MEET_AND_TALK"
     };

        static AddMeetAndTalkDefine()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> allDefines = definesString.Split(';').ToList();
            allDefines.AddRange(Symbols.Except(allDefines));
            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", allDefines.ToArray()));
        }
    }

#endif
}
