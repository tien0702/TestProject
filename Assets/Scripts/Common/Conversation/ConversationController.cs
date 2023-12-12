using TMPro;
using TT;
using UnityEngine;
using UnityEngine.UI;

public class ConversationController : SingletonBehaviour<ConversationController>
{
    #region Events
    public enum ConversationEventType { OnEnter, OnPause, OnEnd }
    ObserverEvents<ConversationEventType, ConversationNode> _events = new ObserverEvents<ConversationEventType, ConversationNode>();
    public ObserverEvents<ConversationEventType, ConversationNode> Events => _events;
    #endregion

    public enum ConversationState { None, Playing, Pause }

    [SerializeField] TextMeshProUGUI _content;
    [SerializeField] TextMeshProUGUI _entityName;
    [SerializeField] Button _nextButton;
    [SerializeField] ConversationState _state;

    ConversationNode _curNode;

    protected virtual void Start()
    {
        _state = ConversationState.None;
        gameObject.SetActive(false);
        if (_nextButton != null)
        {
            _nextButton.onClick.AddListener(() => { NextNode(); });
        }
    }

    public virtual bool EnterConversation(ConversationNode conversationNode)
    {
        if (_state == ConversationState.Playing) return false;
        gameObject.SetActive(true);
        _state = ConversationState.Playing;

        _curNode = conversationNode;
        NextNode();
        _events.Notify(ConversationEventType.OnEnter, _curNode);
        return true;
    }

    public virtual void NextNode()
    {
        ConversationNode nextNode = _curNode.NextNode;
        if (nextNode != null)
        {
            ShowConversationNode(nextNode);
        }
        else
        {
            EndConversation();
        }
    }

    protected virtual void EndConversation()
    {
        _curNode = null;
        _state = ConversationState.None;
        gameObject.SetActive(false);
        InteractableController.Instance.ClearInteracts();
        _events.Notify(ConversationEventType.OnEnd, _curNode);
    }

    protected virtual void ShowConversationNode(ConversationNode node)
    {
        _entityName.text = node.EntityName;
        _content.text = node.Content;
        InteractableController.Instance.ClearInteracts();

        if (node.Responses != null && node.Responses.Length > 0)
        {
            _nextButton.gameObject.SetActive(false);
            foreach (ConversationNode response in node.Responses)
            {
                InteractableController.Instance.AddInteract(response.Content, () =>
                {
                    SelectResponse(response);
                });
            }
        }
    }

    protected virtual void SelectResponse(ConversationNode node)
    {
        _curNode = node;
        _nextButton.gameObject.SetActive(true);
        NextNode();
    }
}
