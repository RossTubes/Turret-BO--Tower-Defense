using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color StartColor;

    BuildManager buildmanager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartColor = rend.material.color;

        buildmanager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildmanager.SelectNode(this);
           // Debug.Log("can't build there! - TODO: Display on screen.");
            return;
        }

        if (!buildmanager.CanBuild)
            return;

        buildTurret(buildmanager.GetTurretToBuild());
        
        //buildmanager.BuildTurretOn(this);
        //build a turret

        //GameObject turretToBuild = BuildManager,instance.GetTurretToBuild();
        //turret = (GameObject)Instantiate(TurretToBuild, transform.position + positionOffset, transform.rotation);

        void buildTurret (TurretBlueprint blueprint)
        {
            if (PlayerStats.Money < blueprint.cost)
            {
                Debug.Log("Not enough money!");
                return;
            }

            PlayerStats.Money -= blueprint.cost;

            //get rid of the old turret

            //building an upgrade
            GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            turretBlueprint = blueprint;

            Debug.Log("Turret build! Money left " + PlayerStats.Money);
        }
    }

    public Vector3 GetBuildposition ()
    {
        return transform.position + positionOffset;
    }

    public void UpgradeTurret ()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;


        Destroy(turret);
        //get rid of the old turret

        //building an upgrade
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);

        turret = _turret;

        isUpgraded = true;

        Debug.Log("Turret upgraded! Money left " + PlayerStats.Money);
    }
    // Start is called before the first frame update
    void OnMouseEnter ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildmanager.CanBuild)
            return;
        rend.material.color = hoverColor;

        if (buildmanager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit ()
    {
        rend.material.color = StartColor;
    }
}