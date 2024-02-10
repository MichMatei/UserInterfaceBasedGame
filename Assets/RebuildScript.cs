using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebuildScript : MonoBehaviour
{
    OnScreenManager onScreenManager;
    public Camera mainCamera;
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;

    public GameObject rebuildUI;
    public TextMeshProUGUI costOfRebuild;
    public Image imageOfResource;

    public Material stoneRebuilt;
    public Material woodenRebuilt;

    float range = 10f;

    public int rebuiltSection = 0;
    public int rebuiltPieces = 0;

    // Start is called before the first frame update
    void Start()
    {
        onScreenManager = OnScreenManager.onScreenManagerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        HitRay();
    }

    void HitRay()
    {
        RaycastHit hit;
        //we shoot a raycast and if it hits an object with the correct tag while input is E we rebuild that part
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.tag == "WoodBaseTag")
            {
                rebuildUI.SetActive(true);
                costOfRebuild.text = "Costs  :  20 x";
                imageOfResource.sprite = onScreenManager.woodImage.GetComponent<Image>().sprite;

                if (Input.GetKeyDown(KeyCode.E) && onScreenManager.woodResourceAmmount >= 20 && onScreenManager.exhaustedSlider.value > 5f)
                {
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material = woodenRebuilt;
                    onScreenManager.woodResourceAmmount -= 20;
                    onScreenManager.exhaustedSlider.value -= 2f;
                    onScreenManager.woodResourceText.text = onScreenManager.woodResourceAmmount.ToString();
                    hit.transform.GetComponent<Collider>().isTrigger = false;
                    hit.transform.gameObject.tag = "Rebuilt";
                    gameObject.GetComponentInParent<SetRebuildActive>().rebuiltPieces++;
                    onScreenManager.rebuiltNumberText.text = gameObject.GetComponentInParent<SetRebuildActive>().rebuiltPieces.ToString();
                    onScreenManager.rebuildSound.Play();
                }
                else if (Input.GetKeyDown(KeyCode.E) && onScreenManager.woodResourceAmmount < 20 && onScreenManager.exhaustedSlider.value > 5f)
                {
                    onScreenManager.lerpTip = true;
                    onScreenManager.tipText.text = onScreenManager.availableTexts[2];
                    onScreenManager.errorSound.Play();
                }
                else if (Input.GetKeyDown(KeyCode.E) && onScreenManager.woodResourceAmmount >= 20 && onScreenManager.exhaustedSlider.value < 5f)
                {
                    onScreenManager.lerpTip = true;
                    onScreenManager.tipText.text = onScreenManager.availableTexts[1];
                    onScreenManager.errorSound.Play();
                }
            }
            else if (hit.transform.gameObject.tag == "PlankTag")
            {
                rebuildUI.SetActive(true);
                costOfRebuild.text = "Costs  :  10 x";
                imageOfResource.sprite = onScreenManager.woodImage.GetComponent<Image>().sprite;

                if (Input.GetKeyDown(KeyCode.E) && onScreenManager.woodResourceAmmount >= 10 && onScreenManager.exhaustedSlider.value > 5f)
                {
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material = woodenRebuilt;
                    onScreenManager.woodResourceAmmount -= 10;
                    onScreenManager.exhaustedSlider.value -= 2f;
                    onScreenManager.woodResourceText.text = onScreenManager.woodResourceAmmount.ToString();
                    hit.transform.GetComponent<Collider>().isTrigger = false;
                    hit.transform.gameObject.tag = "Rebuilt";
                    gameObject.GetComponentInParent<SetRebuildActive>().rebuiltPieces++;
                    onScreenManager.rebuiltNumberText.text = gameObject.GetComponentInParent<SetRebuildActive>().rebuiltPieces.ToString();
                    onScreenManager.rebuildSound.Play();
                }
                else if (Input.GetKeyDown(KeyCode.E) && onScreenManager.woodResourceAmmount < 10 && onScreenManager.exhaustedSlider.value > 5f)
                {
                    onScreenManager.lerpTip = true;
                    onScreenManager.tipText.text = onScreenManager.availableTexts[2];
                    onScreenManager.errorSound.Play();
                }
                else if (Input.GetKeyDown(KeyCode.E) && onScreenManager.woodResourceAmmount >= 10 && onScreenManager.exhaustedSlider.value < 5f)
                {
                    onScreenManager.lerpTip = true;
                    onScreenManager.tipText.text = onScreenManager.availableTexts[1];
                    onScreenManager.errorSound.Play();
                }
            }
            else if (hit.transform.gameObject.tag == "StoneBaseTag")
            {
                rebuildUI.SetActive(true);
                costOfRebuild.text = "Costs  :  35 x";
                imageOfResource.sprite = onScreenManager.stoneImage.GetComponent<Image>().sprite;

                if (Input.GetKeyDown(KeyCode.E) && onScreenManager.stoneResourceAmmount >= 35 && onScreenManager.exhaustedSlider.value > 5f)
                {
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material = stoneRebuilt;
                    onScreenManager.stoneResourceAmmount -= 35;
                    onScreenManager.exhaustedSlider.value -= 2f;
                    onScreenManager.stoneResourceText.text = onScreenManager.stoneResourceAmmount.ToString();
                    hit.transform.GetComponent<Collider>().isTrigger = false;
                    hit.transform.gameObject.tag = "Rebuilt";
                    gameObject.GetComponentInParent<SetRebuildActive>().rebuiltPieces++;
                    onScreenManager.rebuiltNumberText.text = gameObject.GetComponentInParent<SetRebuildActive>().rebuiltPieces.ToString();
                    onScreenManager.rebuildSound.Play();
                }
                else if (Input.GetKeyDown(KeyCode.E) && onScreenManager.stoneResourceAmmount < 35 && onScreenManager.exhaustedSlider.value > 5f)
                {
                    onScreenManager.lerpTip = true;
                    onScreenManager.tipText.text = onScreenManager.availableTexts[2];
                    onScreenManager.errorSound.Play();
                }
                else if (Input.GetKeyDown(KeyCode.E) && onScreenManager.stoneResourceAmmount >= 35 && onScreenManager.exhaustedSlider.value < 5f)
                {
                    onScreenManager.lerpTip = true;
                    onScreenManager.tipText.text = onScreenManager.availableTexts[1];
                    onScreenManager.errorSound.Play();
                }
            }
            else
            {
                rebuildUI.SetActive(false);
            }
        }
        else
        {
            rebuildUI.SetActive(false);
        }
    }
}
