using UnityEngine;
using UnityEngine.UI;

public class UIStart : MonoBehaviour
{
    public Button myButton;

    private void Awake()
    {
        myButton ??= GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        EventManager.State = GameState.Start;
    }
}
