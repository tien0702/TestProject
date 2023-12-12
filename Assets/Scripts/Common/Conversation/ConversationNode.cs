using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ConversationNode
{
    public string EntityName;
    public string Content;
    public ConversationNode[] Responses;

    private int currentIndex = -1;
    public ConversationNode NextNode
    {
        get
        {
            if (currentIndex + 1 < Responses.Length)
            {
                return Responses[++currentIndex];
            }
            return null;
        }
    }
}