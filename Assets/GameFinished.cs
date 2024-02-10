using UnityEngine;

public class GameFinished : MonoBehaviour
{
    bool inRange = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inRange)
        {
            OnScreenManager.onScreenManagerInstance.gameFinished = true;
            OnScreenManager.onScreenManagerInstance.gameCompletedSound.Play();
}
    }

    private void OnTriggerEnter(Collider other)
    {
        OnScreenManager.onScreenManagerInstance.pressE.SetActive(true);
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        OnScreenManager.onScreenManagerInstance.pressE.SetActive(false);
        inRange = false;
    }
}
