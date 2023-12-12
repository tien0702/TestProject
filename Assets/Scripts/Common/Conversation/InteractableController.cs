using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using UnityEngine.UI;
using System;
using TMPro;

public class InteractableController : SingletonBehaviour<InteractableController>
{
    [SerializeField] protected Button _responseButtonPrefab;
    Dictionary<string, Button> _interacts = new Dictionary<string, Button>();

    public void AddInteract(string interactName, Action callbackOnSelect)
    {
        if (_interacts.ContainsKey(interactName))
        {
            _interacts.Remove(interactName);
        }
        _interacts.Add(interactName, CreateButton(interactName, callbackOnSelect));
    }

    public void RemoveInteract(string interactName)
    {
        if (_interacts.ContainsKey(interactName))
        {
            Destroy(_interacts[interactName].gameObject);
            _interacts.Remove(interactName);
        }
    }

    public void ClearInteracts()
    {
        foreach (var button in _interacts.Values)
        {
            Destroy(button.gameObject);
        }
        _interacts.Clear();
    }

    protected virtual Button CreateButton(string content, Action callback)
    {
        Button button = Instantiate(_responseButtonPrefab, transform);
        button.onClick.AddListener(() => callback());
        button.GetComponentInChildren<TextMeshProUGUI>().text = content;

        return button;
    }
}
