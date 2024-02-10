using UnityEngine;
using UnityEngine.UI;

public class InteractWithStone : MonoBehaviour
{
    public Camera mainCamera;
    float range = 6f;

    public GameObject objectCanvas;
    public GameObject resourcePrefab;
    public GameObject circularSlider;

    public Slider objectHpSlider;

    float time;
    bool showUI = false;

    public GameObject target;
    public GameObject firstPosition;
    public GameObject secondPosition;
    public GameObject thirdPosition;
    public GameObject fourthPosition;
    public GameObject fifthPosition;

    OnScreenManager onScreenManager;

    bool ifStatementCanRunOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        objectCanvas.SetActive(false);
        onScreenManager = OnScreenManager.onScreenManagerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        //if object active, set the object's UI to active
        if (gameObject.activeInHierarchy)
        {
            objectCanvas.SetActive(true);
            onScreenManager.circularSlider.gameObject.SetActive(true);
        }

        //if object hp is 0 destroy it and start the timer for the respawn
        if (objectHpSlider.value == 0)
        {
            resourcePrefab.SetActive(false);
            objectCanvas.SetActive(false);

            time += Time.deltaTime;
            ifStatementCanRunOnce = true;
            objectHpSlider.value = objectHpSlider.maxValue;
            circularSlider.SetActive(false);
        }

        //if the gameobject's prefab is active and we can enter this if, respawn and set hp to max, also if the player is still in the collider set the UI to visible
        if (resourcePrefab.activeInHierarchy && ifStatementCanRunOnce)
        {
            resourcePrefab.SetActive(true);

            time = 0;
            objectHpSlider.value = objectHpSlider.maxValue;

            if (showUI)
            {
                objectCanvas.SetActive(true);
                onScreenManager.circularSlider.gameObject.SetActive(true);
            }
            ifStatementCanRunOnce = false;
        }

        //if player presses mouse0 adn conditions are true, fill the slider up, else, play error sound and display error message
        if (Input.GetKeyDown(KeyCode.Mouse0) && objectCanvas.activeInHierarchy && onScreenManager.pickaxeToolSelected && onScreenManager.circularSlider.value == 0)
        {
            onScreenManager.fillSlider = true;
            onScreenManager.farmingResourceSound.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && objectCanvas.activeInHierarchy && !onScreenManager.pickaxeToolSelected)
        {
            onScreenManager.lerpTip = true;
            onScreenManager.tipText.text = onScreenManager.availableTexts[6];
            onScreenManager.errorSound.Play();
        }

        //if slider is now full and right tool is selected...
        if (onScreenManager.fillSlider && onScreenManager.pickaxeToolSelected)
        {
            onScreenManager.circularSlider.value += 100 * Time.deltaTime;

            if (onScreenManager.circularSlider.value == onScreenManager.circularSlider.maxValue)
            {
                onScreenManager.circularSlider.value = onScreenManager.circularSlider.minValue;
                onScreenManager.fillSlider = false;
                HitRay();
            }
        }
    }


    void HitRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            bool myStatement = hit.transform.name == "TargetImage" && gameObject.activeInHierarchy && objectCanvas.activeInHierarchy;

            if (myStatement && onScreenManager.stoneResourceAmmount < 990 && onScreenManager.exhaustedSlider.value > 5f)
            {
                int a = Random.Range(0, 3);

                if (a == 0)
                {
                    target.transform.position = firstPosition.transform.position;
                }
                else if (a == 1)
                {
                    target.transform.position = secondPosition.transform.position;
                }
                else if (a == 2)
                {
                    target.transform.position = thirdPosition.transform.position;
                }
                else if (a == 3)
                {
                    target.transform.position = fourthPosition.transform.position;
                }
                else
                {
                    target.transform.position = fifthPosition.transform.position;
                }

                objectHpSlider.value -= 1f;
                onScreenManager.stoneResourceAmmount += 10;
                onScreenManager.exhaustedSlider.value -= 2f;
                onScreenManager.stoneResourceText.text = onScreenManager.stoneResourceAmmount.ToString();
                onScreenManager.lerpUp = true;
                onScreenManager.selectedImage = onScreenManager.stoneImage;
                onScreenManager.resourceSound.Play();
            }
            else if (myStatement && onScreenManager.stoneResourceAmmount <= 990 && onScreenManager.exhaustedSlider.value < 5f)
            {
                onScreenManager.lerpTip = true;
                onScreenManager.tipText.text = onScreenManager.availableTexts[1];
                onScreenManager.errorSound.Play();
                //too tired
            }
            else if (myStatement && onScreenManager.stoneResourceAmmount == 990 && onScreenManager.exhaustedSlider.value > 5f)
            {
                onScreenManager.lerpTip = true;
                onScreenManager.tipText.text = onScreenManager.availableTexts[0];
                onScreenManager.errorSound.Play();
                //max resources
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objectCanvas.SetActive(true);
        showUI = true;
    }

    private void OnTriggerExit(Collider other)
    {
        objectCanvas.SetActive(false);
        showUI = false;
    }
}
