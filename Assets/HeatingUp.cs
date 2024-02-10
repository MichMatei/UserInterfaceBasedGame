using UnityEngine;

public class HeatingUp : MonoBehaviour
{
    bool inRange = false;
    float timer;
    [SerializeField]ParticleSystem fire;
    
    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && fire.startSize < 0.5f && inRange)
        {
            if (OnScreenManager.onScreenManagerInstance.woodResourceAmmount > 10)
            {
                fire.startSize += 0.10f;

                OnScreenManager.onScreenManagerInstance.woodResourceAmmount -= 10;
                OnScreenManager.onScreenManagerInstance.woodResourceText.text = OnScreenManager.onScreenManagerInstance.woodResourceAmmount.ToString();
                OnScreenManager.onScreenManagerInstance.exhaustedSlider.value -= 2f;
                OnScreenManager.onScreenManagerInstance.selectedImage = OnScreenManager.onScreenManagerInstance.woodImage;
                OnScreenManager.onScreenManagerInstance.lerpUp = true;
                OnScreenManager.onScreenManagerInstance.rebuildSound.Play();
            }
            else
            {
                OnScreenManager.onScreenManagerInstance.lerpTip = true;
                OnScreenManager.onScreenManagerInstance.tipText.text = OnScreenManager.onScreenManagerInstance.availableTexts[2];
                OnScreenManager.onScreenManagerInstance.errorSound.Play();
            }
            
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > 15f)
        {
            timer = 0;
            if (fire.startSize>0.1f)
            {
                fire.startSize -= 0.10f;
            }
        }

        if (inRange)
        {
            OnScreenManager.onScreenManagerInstance.bodyHeatSlider.value += (fire.startSize * 10) * Time.deltaTime;
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
/*
 * https://stackoverflow.com/questions/55562829/how-to-fix-nullreferenceexception-do-not-create-your-own-module-instances-get
 * https://stackoverflow.com/questions/57127545/cannot-modify-the-return-value-of-particlesystem-shape-because-it-is-not-a-var
 */