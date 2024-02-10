using UnityEngine;
using UnityEngine.UI;

public class SleepScript : MonoBehaviour
{
    bool inRange = false;
    bool sleeping = false;
    bool wakingUp = false;
    public Image fadeToBlack;
    public GameObject onScreenUI;
    private float time;

    GameObject movement;
    GameObject mouseSens;
    float originalPlayerSpeed;
    float originalMouseSens;

    private void Start()
    {
        movement = GameObject.Find("FirstPersonPlayer");
        mouseSens = GameObject.Find("Main Camera");

        originalPlayerSpeed = movement.GetComponent<PlayerMovement>().speed;
        originalMouseSens = mouseSens.GetComponent<MouseLook>().mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && !sleeping)
        {
            sleeping = true;
            movement.GetComponent<PlayerMovement>().speed = 0;
            mouseSens.GetComponent<MouseLook>().mouseSensitivity = 0;
        }

        if (sleeping)
        {
            time += 0.5f * Time.deltaTime;
            fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, time);

            Debug.Log(time);

            if (time > 2f)
            {
                sleeping = false;
                wakingUp = true;
            }
        }

        if (wakingUp)
        {
            Debug.Log(time);
            time -= 0.5f * Time.deltaTime;
            fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, time);
            OnScreenManager.onScreenManagerInstance.exhaustedSlider.value += 20 * Time.deltaTime;
            if (time < -0.5f)
            {
                time = 0f;
                sleeping = false;
                wakingUp = false;
                onScreenUI.SetActive(true);
                movement.GetComponent<PlayerMovement>().speed = originalPlayerSpeed;
                mouseSens.GetComponent<MouseLook>().mouseSensitivity = originalMouseSens;
            }
        }
    }

    private void FixedUpdate()
    {
        if (inRange)
        {
            OnScreenManager.onScreenManagerInstance.pressE.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        OnScreenManager.onScreenManagerInstance.pressE.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        OnScreenManager.onScreenManagerInstance.pressE.SetActive(false);
    }
}
