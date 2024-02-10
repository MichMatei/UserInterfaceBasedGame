using UnityEngine;

public class SetResScriptActive : MonoBehaviour
{

    public GameObject theScript;
    public GameObject thePrefab;
    public GameObject circularSlider;

    float myTime;

    bool inRange = false;

    // Update is called once per frame
    void Update()
    {
        //if the object is not active, start a timer to respawn it
        if (!thePrefab.activeInHierarchy)
        {
            Debug.Log(myTime);
            myTime += Time.deltaTime;

            if (myTime > 5f)
            {
                myTime = 0;
                thePrefab.SetActive(true);
                theScript.SetActive(false);
            }
        }

        if (inRange && thePrefab.activeInHierarchy)
        {
            theScript.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (thePrefab.activeInHierarchy)
        {
            theScript.SetActive(true);
            circularSlider.SetActive(true);
        }
        
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        theScript.SetActive(false);
        circularSlider.SetActive(false);
        OnScreenManager.onScreenManagerInstance.circularSlider.value = OnScreenManager.onScreenManagerInstance.circularSlider.minValue;
        OnScreenManager.onScreenManagerInstance.fillSlider = false;
    }
}
