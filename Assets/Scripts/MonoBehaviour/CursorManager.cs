using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public bool IsCursorLocked { get; private set; } = true;

    void Awake()
    {
        // Ensure there is only one instance of CursorManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the CursorManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        HandleCursorLock();
    }

    void HandleCursorLock()
    {
        // Toggle cursor lock state when Esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsCursorLocked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsCursorLocked = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        IsCursorLocked = false;
    }
}
