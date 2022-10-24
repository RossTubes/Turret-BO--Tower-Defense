using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class nodeUi : MonoBehaviour
{
    public GameObject ui;

    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;

    private Node target;

    public void Settarget (Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
        upgradeCost.text = "$" + target.turretBlueprint.cost;
            upgradeButton.interactable = true;
        }else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }
        

        ui.SetActive(true);
    }

    public void Hide ()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
}
