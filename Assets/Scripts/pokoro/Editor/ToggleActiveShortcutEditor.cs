 using UnityEditor;
 using UnityEngine;
 
 public class ToggleActiveShortcutEditor : Editor
 {
   [MenuItem("GameObject/ActiveToggle _/")]   //Shortcut is whatever [x] is _[x]
   static void ToggleActivationSelection()
   {
     var go = Selection.activeGameObject;
     go.SetActive(!go.activeSelf);
   }
 }