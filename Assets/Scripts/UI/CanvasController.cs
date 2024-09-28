using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _welcome;
    [SerializeField] private GameObject _victory;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _defeat;
    [SerializeField] private GameObject _gameMain;

    // The current state of the game
    private GameState _currentState;

    private void Awake()
    {
        EventManager.OnGameStateChanged += ChangeGameState;
    }

    // Method to change the game state and show the appropriate canvas (GameObject)
    public void ChangeGameState(GameState newState)
    {
        _currentState = newState;

        try
        {
            // Disable all canvas GameObjects first
            DisableAllCanvases();

            // Enable the correct canvas GameObject based on the game state
            switch (_currentState)
            {
                case GameState.Welcome: _welcome.SetActive(true); break;
                case GameState.Start: _gameMain.SetActive(true); break;
                case GameState.Pause: _pause.SetActive(true); break;
                case GameState.Victory: _victory.SetActive(true); break;
                case GameState.Defeat: _defeat.SetActive(true); break;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    // Method to disable all canvas GameObjects
    private void DisableAllCanvases()
    {
        _welcome.SetActive(false);
        _gameMain.SetActive(false);
        _pause.SetActive(false);
        _victory.SetActive(false);
        _defeat.SetActive(false);
    }
}
