using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnScreenManager : MonoBehaviour
{
    public static OnScreenManager onScreenManagerInstance;

    public Slider exhaustedSlider;
    public Slider bodyHeatSlider;
    public Slider healthSlider;
    public TextMeshProUGUI stoneResourceText;
    public TextMeshProUGUI woodResourceText;
    public TextMeshProUGUI tipText;
    public TextMeshProUGUI rebuiltNumberText;
    public GameObject number1ForAxe;
    public GameObject number2ForPickaxe;
    public GameObject tipUI;
    public GameObject tipUILocation;
    public GameObject pressE;
    Vector3 originalUILocation;
    [HideInInspector]public string[] availableTexts;
    public int woodResourceAmmount;
    public int stoneResourceAmmount;
    public GameObject woodImage;
    public GameObject stoneImage;
    public GameObject axeImage;
    public GameObject pickaxeImage;
    public GameObject selectedImage;
    public GameObject selectedTool;
    public GameObject axeFrameHighlight;
    public GameObject pickaxeFrameHighlight;
    public GameObject quitGameUI;
    public GameObject gameFinishedText;
    public Image flashDamage;
    public Image fadeToBlack;

    public AudioSource errorSound;
    public AudioSource switchToolSound;
    public AudioSource farmingResourceSound;
    public AudioSource resourceSound;
    public AudioSource pickToolUpSound;
    public AudioSource playerHurtingSound;
    public AudioSource rebuildSound;
    public AudioSource gameCompletedSound;
    public AudioSource gameOverSound;
    [HideInInspector] public Vector3 scaledUp;

    GameObject movement;
    GameObject mouseSens;

    public Slider circularSlider;
    [HideInInspector] public bool playerDied = false;
    [HideInInspector] public bool fillSlider = false;
    [HideInInspector] public bool lerpUp = false;
    [HideInInspector] public bool lerpDown = false;
    [HideInInspector] public bool lerpUpTool = false;
    [HideInInspector] public bool lerpDownTool = false;
    [HideInInspector] public bool axeToolSelected = false;
    [HideInInspector] public bool pickaxeToolSelected = false;
    [HideInInspector] public bool lerpTip = false;
    [HideInInspector] public bool gameFinished = false;
    [HideInInspector] bool canSwitchTool = true;
    [HideInInspector] bool displayTooTired = true;
    [HideInInspector] bool displayTooCold = true;
    [HideInInspector] Vector3 originalScale;
    float tooTiredTimer = 0f;
    float tooColdTimer = 0f;
    float fadeToBlackValue = 0f;

    //Weird place to declare this variable but had to so all instances of "SetRebuildActive.cs" can "update" it to the right value (maybe I could have done smth else but...)
    public int rebuiltSection = 0;

    private void Awake()
    {
        if (onScreenManagerInstance == null)
        {
            onScreenManagerInstance = this;
            DontDestroyOnLoad(onScreenManagerInstance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scaledUp = Vector3.one * 1.2f;

        //different texts that will appear as error messages or tips
        availableTexts = new string[] { "You can't carry more resources...", "You are too tired to perform this action...", "You don't have enough resources..."
                                      , "You're getting too cold...", "You're growing too tired", "You need to have/select an axe for that...",
                                        "You need to have/select a pickaxe for that..."};
        originalUILocation = tipUI.transform.position;

        movement = GameObject.Find("FirstPersonPlayer");
        mouseSens = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        exhaustedSlider.value -= 0.5f * Time.deltaTime;
        bodyHeatSlider.value -= 0.5f * Time.deltaTime;

        if (!displayTooCold)
        {
            tooColdTimer += Time.deltaTime;
            if (tooColdTimer > 60f)
            {
                displayTooCold = true;
                tooColdTimer = 0f;
            }
        }

        if (!displayTooTired)
        {
            tooTiredTimer += Time.deltaTime;
            if (tooTiredTimer > 60f)
            {
                displayTooTired = true;
                tooTiredTimer = 0f;
            }
        }

        if (bodyHeatSlider.value < 20f && displayTooCold)
        {
            lerpTip = true;
            displayTooCold = false;
            tipText.text = availableTexts[3];
            errorSound.Play();
        }


        if (exhaustedSlider.value < 20f && displayTooTired)
        {

            lerpTip = true;
            displayTooTired = false;
            tipText.text = availableTexts[4];
            errorSound.Play();
        }

        //the two if statements handled the lerping of different images, such as the wood resources
        if (lerpUp)
        {
            originalScale = Vector3.one * 0.8f;
            selectedImage.transform.localScale = Vector3.Lerp(selectedImage.transform.localScale, scaledUp, 4f * Time.deltaTime);

            if (selectedImage.transform.localScale.x > 1.15f)
            {
                lerpUp = false;
                lerpDown = true;
            }
        }

        if (lerpDown)
        {
            selectedImage.transform.localScale = Vector3.Lerp(selectedImage.transform.localScale, originalScale, 8f * Time.deltaTime);

            if (selectedImage.transform.localScale.x < 0.85f)
            {
                selectedImage.transform.localScale = Vector3.one * 0.8f;
                lerpUp = false;
                lerpDown = false;
            }
        }

        //the two if statements handled the tool image lerping when the player selects them
        if (lerpUpTool)
        {
            selectedTool.transform.localScale = Vector3.Lerp(selectedTool.transform.localScale, Vector3.one * 0.7f, 4f * Time.deltaTime);

            if (selectedTool.transform.localScale.x < 0.75f)
            {
                selectedTool.transform.localScale = Vector3.one * 0.7f;
                lerpUpTool = false;
                lerpDownTool = true;
            }
        }

        if (lerpDownTool)
        {
            selectedTool.transform.localScale = Vector3.Lerp(selectedTool.transform.localScale, Vector3.one, 4f * Time.deltaTime);

            if (selectedTool.transform.localScale.x > 0.95f)
            {
                selectedTool.transform.localScale = Vector3.one;
                lerpUpTool = false;
                lerpDownTool = false;
                canSwitchTool = true;

                if (selectedTool == axeImage)
                {
                    axeToolSelected = true;
                    axeFrameHighlight.SetActive(true);
                }
                else if (selectedTool == pickaxeImage)
                {
                    pickaxeToolSelected = true;
                    pickaxeFrameHighlight.SetActive(true);
                }
            }
        }


        if (axeImage.activeInHierarchy && Input.GetKeyDown(KeyCode.Alpha1) && canSwitchTool)
        {
            axeFrameHighlight.SetActive(false);
            pickaxeFrameHighlight.SetActive(false);
            selectedTool = axeImage;
            lerpUpTool = true;
            canSwitchTool = false;
            axeToolSelected = false;
            pickaxeToolSelected = false;

            fillSlider = false;
            circularSlider.value = circularSlider.minValue;
            switchToolSound.Play();
        }
        
        if (pickaxeImage.activeInHierarchy && Input.GetKeyDown(KeyCode.Alpha2) && canSwitchTool)
        {
            axeFrameHighlight.SetActive(false);
            pickaxeFrameHighlight.SetActive(false);
            selectedTool = pickaxeImage;
            lerpUpTool = true;
            canSwitchTool = false;
            axeToolSelected = false;
            pickaxeToolSelected = false;

            fillSlider = false;
            circularSlider.value = circularSlider.minValue;
            switchToolSound.Play();
        }

        //lerps error message/tooltip into place
        if (lerpTip)
        {
            tipUI.SetActive(true);
            tipUI.transform.position = Vector3.Lerp(tipUI.transform.position, tipUILocation.transform.position, 4f * Time.deltaTime);
            if (Vector3.Distance(tipUI.transform.position,tipUILocation.transform.position) < 1f)
            {
                tipUI.transform.position = tipUILocation.transform.position;
                lerpTip = false;
            }
        }

        //dismisses the tooltip
        if (tipUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Return) && !lerpTip)
        {
            tipUI.SetActive(false);
            tipUI.transform.position = originalUILocation;
        }

        if (playerDied)
        {
            movement.GetComponent<PlayerMovement>().enabled = false;
            mouseSens.GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //mouseSens.GetComponent<MouseLook>().enabled = false;
            quitGameUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        if (gameFinished)
        {
            fadeToBlackValue += Time.deltaTime;
            fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, fadeToBlackValue);
            movement.GetComponent<PlayerMovement>().enabled = false;
            mouseSens.GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (fadeToBlackValue > 2f)
            {
                gameFinishedText.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }
}
