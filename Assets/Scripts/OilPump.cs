using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPump : MonoBehaviour
{
    private bool canSelect;
    [HideInInspector] public bool isSelected;
    public bool canMine;
    public float TimeBetweenDrill = 15.25f;
    // Start is called before the first frame update
    void Start()
    {
        RandomInvoke();
        Invoke("EnableSelect", 1f);
    }
    public void RandomInvoke()
    {
        if(canMine)
        {
            Invoke("MineGold", Random.Range(10,12));
        }
    }
    public void MineGold()
    {
        FindObjectOfType<PlayerStats>().AddMoney(25);
        RandomInvoke();
    }

    public void SelectTurret()
    {
        if (canSelect)
        {
            isSelected = !isSelected;
        }
    }

    private void EnableSelect()
    {
        canSelect = true;
    }

    public void DisableSelect()
    {
        isSelected = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
