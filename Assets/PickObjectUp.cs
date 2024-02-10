using UnityEngine;

public class PickObjectUp : MonoBehaviour
{
    bool inRange = false;

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.tag == "Axe")
            {
                OnScreenManager.onScreenManagerInstance.axeImage.SetActive(true);
                OnScreenManager.onScreenManagerInstance.number1ForAxe.SetActive(true);
                OnScreenManager.onScreenManagerInstance.exhaustedSlider.value -= 2f;
                Destroy(gameObject);
                OnScreenManager.onScreenManagerInstance.pressE.SetActive(false);
                OnScreenManager.onScreenManagerInstance.pickToolUpSound.Play();
            }

            if (gameObject.tag == "Pickaxe")
            {
                OnScreenManager.onScreenManagerInstance.pickaxeImage.SetActive(true);
                OnScreenManager.onScreenManagerInstance.number2ForPickaxe.SetActive(true);
                OnScreenManager.onScreenManagerInstance.exhaustedSlider.value -= 2f;
                Destroy(gameObject);
                OnScreenManager.onScreenManagerInstance.pressE.SetActive(false);
                OnScreenManager.onScreenManagerInstance.pickToolUpSound.Play();
            }

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
