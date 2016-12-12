using UnityEngine;
using UnityEditor;
using System.Collections;

public class ConversationAssetCreator : MonoBehaviour {

    [MenuItem("Assets/Create/Conversation")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Conversation>();
    }
}
