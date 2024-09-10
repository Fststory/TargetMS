using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

public class PhoneController : MonoBehaviour
{
    public GameObject bankScreen;
    public GameObject homeScreen;
    public GameObject phone;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                Debug.Log("Raycast hit: " + result.gameObject.name);
            }
        }
    }

    public void GotoBank()
    {
        if (phone != null)
        {
            homeScreen.SetActive(false);
            bankScreen.SetActive(true);
        }
    }

    public void GotoHome()
    {
        if (phone != null)
        {
            homeScreen.SetActive(true);
            bankScreen.SetActive(false);
        }
    }

    public void Saving()
    {
        if (bankScreen != null)
        {
            Debug.Log("적금");
        }
    }
    
    public void Stock()
    {
        if (bankScreen != null)
        {
            Debug.Log("주식");
        }
    }
    
    public void Fund()
    {
        if (bankScreen != null)
        {
            Debug.Log("펀드");
        }
    }
    
}
