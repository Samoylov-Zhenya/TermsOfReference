using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIClosse : MonoBehaviour
{
    public Button myButton;

    private void Awake()
    {
        myButton ??= GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        EventManager.State = GameState.ClosseApp;
    }
}
