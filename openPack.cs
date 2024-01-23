using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class openPack : MonoBehaviour
{
    public GameObject packImage;
    public GameObject quickSellBtn;
    bool packIsOpen = false;
    int pick = 0;
    int coins = 0;

    public TMP_Text coinCounter;
    public GameObject[] objects;

    // Update is called once per frame
    
    public void SavePrefs()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }
    
    public void LoadPrefs()
    {
        coins = PlayerPrefs.GetInt("Coins", 0); 
    }

    void Start()
    {
        LoadPrefs();
        coinCounter.SetText("Coins:\n" + coins.ToString());
    }

    void Update()
    {
        objects = GameObject.FindGameObjectsWithTag("card");
        

        if (Input.GetMouseButtonDown(0) && !packIsOpen)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (touchPos.y > -4 && touchPos.y < 4)
            {
                packImage.SetActive(false);
                quickSellBtn.SetActive(true);
                pick = Random.Range(1,23);
                if (pick > 12 && pick < 18) {
                    pick = Random.Range(1, 18);
                }
                else if (pick > 17 && pick < 20) {
                    pick = Random.Range(1, 20);
                }
                else if (pick > 19) {
                    pick = Random.Range(1, 23);
                }
                foreach (var obj in objects)
                {
                    int num = int.Parse(obj.name);
                    if (num == pick)
                    {
                        obj.transform.position = new Vector3 (0, 0, 0);
                    }
                }
                packIsOpen = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && packIsOpen)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (touchPos.y > -3 && touchPos.y < 4)
            {
                packImage.SetActive(true);
                quickSellBtn.SetActive(false);
                hideAllCards();
                packIsOpen = false;
            }
        }

    }

    public void hideAllCards()
    {
        foreach (var obj in objects)
        {
            obj.transform.position = new Vector3 (-10, 0, 0);
        }
    }
    
    public void quickSell()
    {
        hideAllCards();
        quickSellBtn.SetActive(false);
        coins += pick*100;
        coinCounter.SetText("Coins:\n" + coins.ToString());
        SavePrefs();
    }

    
}
