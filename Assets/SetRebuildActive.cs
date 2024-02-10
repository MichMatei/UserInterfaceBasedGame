using UnityEngine;

public class SetRebuildActive : MonoBehaviour
{
    public GameObject theScript;
    public GameObject rebuiltPiecesTextUI;
    public int rebuiltPieces = 0;

    [SerializeField]GameObject part1;
    [SerializeField] GameObject part2;
    [SerializeField] GameObject part3;
    [SerializeField] GameObject part4;

    // Start is called before the first frame update
    void Start()
    {
        part1 = GetComponentInChildren<RebuildScript>().part1;
        part2 = GetComponentInChildren<RebuildScript>().part2;
        part3 = GetComponentInChildren<RebuildScript>().part3;
        part4 = GetComponentInChildren<RebuildScript>().part4;

        part1.SetActive(false);
        part2.SetActive(false);
        part3.SetActive(false);
        part4.SetActive(false);

        theScript.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //compares the gameobject's tag with the corect tag to have in order to spawn the rebuilding chunk
        if (gameObject.CompareTag(OnScreenManager.onScreenManagerInstance.rebuiltSection.ToString()))
        {
            theScript.SetActive(true);
            rebuiltPiecesTextUI.SetActive(true);
            TurnPartsOn();
            OnScreenManager.onScreenManagerInstance.rebuiltNumberText.text = rebuiltPieces.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (rebuiltPieces == 4)
        {
            OnScreenManager.onScreenManagerInstance.rebuiltSection++;
            rebuiltPieces = 0;
        }

        theScript.SetActive(false);
        rebuiltPiecesTextUI.SetActive(false);
        TurnPartsOff();
    }

    void TurnPartsOn()
    {
        part1.SetActive(true);
        part2.SetActive(true);
        part3.SetActive(true);
        part4.SetActive(true);
    }

    void TurnPartsOff()
    {
        if (part1.tag != "Rebuilt")
        {
            part1.SetActive(false);
        }
        
        if (part2.tag != "Rebuilt")
        {
            part2.SetActive(false);
        }

        if (part3.tag != "Rebuilt")
        {
            part3.SetActive(false);
        }

        if (part4.tag != "Rebuilt")
        {
            part4.SetActive(false);
        }
    }  
}
