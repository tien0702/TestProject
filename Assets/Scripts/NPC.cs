using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    JSONNode json;
    private void Awake()
    {
        var data = Resources.Load<TextAsset>("Data/NPC").text;
        json = JSONObject.Parse(data);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableController.Instance.AddInteract(gameObject.name, () => {
            ConversationController.Instance.EnterConversation(JsonUtility.FromJson<ConversationNode>(json["data"].ToString()));
        });

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableController.Instance.RemoveInteract(gameObject.name);
    }
}
