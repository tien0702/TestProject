using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using UnityEngine.UI;
using System;
using TMPro;

public class ResponseController : MonoBehaviour
{
    [SerializeField] protected Button _reponseButtonPrefab;
    Dictionary<string, Button> _responses = new Dictionary<string, Button>();

    public void AddInteract(string interactName, Action callbackOnSelect)
    {
        if (_responses.ContainsKey(interactName))
        {
            _responses.Remove(interactName);
        }
        _responses.Add(interactName, CreateButton(interactName, callbackOnSelect));
    }

    public void RemoveInteract(string interactName)
    {
        if (_responses.ContainsKey(interactName))
        {
            Destroy(_responses[interactName].gameObject);
            _responses.Remove(interactName);
        }
    }

    public void ClearInteracts()
    {
        foreach (var button in _responses.Values)
        {
            Destroy(button.gameObject);
        }
        _responses.Clear();
    }

    protected virtual Button CreateButton(string content, Action callback)
    {
        Button button = Instantiate(_reponseButtonPrefab, transform);
        button.onClick.AddListener(() => callback());
        button.GetComponentInChildren<TextMeshProUGUI>().text = content;

        return button;
    }
}
