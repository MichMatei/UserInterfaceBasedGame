using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    bool canBeDamaged = false;
    bool flashUp = true;
    bool flashDown = false;
    bool playSoundOnce = true;
    float value = 0f;
    float timer = 0f;

    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        //if the player is colliding with harmfull object and can move do the following
        if(canBeDamaged == true && playerMovement.isActiveAndEnabled)
        {
            OnScreenManager.onScreenManagerInstance.healthSlider.value -= 0.1f;
            OnScreenManager.onScreenManagerInstance.flashDamage.color = new Color (OnScreenManager.onScreenManagerInstance.flashDamage.color.r, 
               OnScreenManager.onScreenManagerInstance.flashDamage.color.g, OnScreenManager.onScreenManagerInstance.flashDamage.color.b, value);

            //makes the red flash increase its alpha
            if (flashUp)
            {
                value += 1 * Time.deltaTime;
                if (value > 0.5f)
                {
                    flashUp = false;
                    flashDown = true;
                }
            }//makes the red flash decrease its alpha
            else if (flashDown)
            {
                value -= 1 * Time.deltaTime;
                if (value < 0f)
                {
                    flashUp = true;
                    flashDown = false;
                }
            }
            
            if (timer>1)
            {
                OnScreenManager.onScreenManagerInstance.playerHurtingSound.Play();
                timer = 0;
            }
            timer += Time.deltaTime;
            Debug.Log(timer);
        }

        //if the player died
        if (OnScreenManager.onScreenManagerInstance.healthSlider.value == 0)
        {
            OnScreenManager.onScreenManagerInstance.playerDied = true;
            if (playSoundOnce)
            {
                playSoundOnce = false;
                OnScreenManager.onScreenManagerInstance.gameOverSound.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canBeDamaged = true;
        timer = 0f;
        OnScreenManager.onScreenManagerInstance.playerHurtingSound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        canBeDamaged = false;
        OnScreenManager.onScreenManagerInstance.flashDamage.color = new Color(OnScreenManager.onScreenManagerInstance.flashDamage.color.r,
               OnScreenManager.onScreenManagerInstance.flashDamage.color.g, OnScreenManager.onScreenManagerInstance.flashDamage.color.b, 0);
        flashUp = true;
        flashDown = false;
        value = 0;
        timer = 0f;
    }
}
