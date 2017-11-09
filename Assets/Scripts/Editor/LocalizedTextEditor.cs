using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// Simple text editor to modify localization data files, without scrolling system
/// </summary>

//public class LocalizedTextEditor : EditorWindow {

//    public LocalizationData localizationData;

//    [MenuItem("Window/Localized Text Editor")]
//    static void Init() {
//        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
//    }

//    private void OnGUI() {
//        if(localizationData != null) {
//            SerializedObject serializedObject = new SerializedObject(this);
//            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
//            EditorGUILayout.PropertyField(serializedProperty, true);
//            serializedObject.ApplyModifiedProperties();
//            if(GUILayout.Button("Save localization data")) {
//                SaveGameData();
//            }
//        }
//        if(GUILayout.Button("Load localization data")) {
//            LoadGameData();
//        }
//        if (GUILayout.Button("Create new localization data file")) {
//            CreateNewData();
//        }
//    }

//    private void LoadGameData() {
//        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
//        if(!string.IsNullOrEmpty(filePath)) {
//            string dataAsJson = File.ReadAllText(filePath);
//            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
//        }
//    }

//    private void SaveGameData() {
//        string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");
//        if(!string.IsNullOrEmpty(filePath)) {
//            string dataAsJson = JsonUtility.ToJson(localizationData);
//            File.WriteAllText(filePath, dataAsJson);
//        }
//    }

//	private void CreateNewData() {
//        localizationData = new LocalizationData();
//    }
//}
