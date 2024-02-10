using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGame : MonoBehaviour
{
    public GameObject userInterface;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject pauseOptions;
    public TextMeshProUGUI texts;

    GameObject movement;
    GameObject mouseSens;
    float changeSensitivity;
    float originalPlayerSpeed;
    float originalMouseSens;
    [SerializeField]Slider sensitivitySlider;

    bool gamePaused = false;

    string[] randomTexts =
    {
        "You can chill now...", "The world is standing still...",
        "Deeeeep breaths...", "Please don't uninstall...", "Awesome GAME! At least if you ask me..."
    };


    // Start is called before the first frame update
    void Start()
    {
        movement = GameObject.Find("FirstPersonPlayer");
        mouseSens = GameObject.Find("Main Camera");

        originalPlayerSpeed = movement.GetComponent<PlayerMovement>().speed;
        originalMouseSens = mouseSens.GetComponent<MouseLook>().mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && userInterface.activeInHierarchy && !OnScreenManager.onScreenManagerInstance.playerDied && !OnScreenManager.onScreenManagerInstance.gameFinished)
        {
            gamePaused = true;
            userInterface.SetActive(false);
            pauseMenu.SetActive(true);
            mouseSens.GetComponent<MouseLook>().mouseSensitivity = 0;
            movement.GetComponent<PlayerMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            texts.text = randomTexts[Random.Range(0, randomTexts.Length)];
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !userInterface.activeInHierarchy)
        {
            gamePaused = false;
            userInterface.SetActive(true);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
            mouseSens.GetComponent<MouseLook>().mouseSensitivity = originalMouseSens;
            movement.GetComponent<PlayerMovement>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetSensitivity(float sens)
    {
        sens = sensitivitySlider.value;
        originalMouseSens = sens;
    }

    public void ReturnToGame()
    {
        gamePaused = false;
        userInterface.SetActive(true);
        pauseMenu.SetActive(false);
        movement.GetComponent<PlayerMovement>().enabled = true;
        mouseSens.GetComponent<MouseLook>().mouseSensitivity = originalMouseSens;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }    
}
