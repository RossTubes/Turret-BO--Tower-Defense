using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //public cuz you want to acces it without the class and static cuz you want it to be shared with all build manegers
    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogError("More Then One BuildManager in Scene!");
            return;
        }
        instance = this;
    }

    public GameObject StandardTurretPrefab;
    public GameObject RocketTurretPrefab;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public nodeUi nodeUi;

    public GameObject LaserTurretPrefab;
    public GameObject BarrleTurretPrefab;
    public GameObject OilPump;
    public GameObject Mortar;


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    //GameObject turretToBuild = buildmanager.SelectTurretToBuild(turret: TurretBlueprint);
    //turret = (GameObject) Instantiate(turretToBuild, transform.position, transform.rotation);
    public void BuildTurretOn (Node node)
    {   
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(),Quaternion.identity);

        node.turret = turret;

        Debug.Log("Turret Build! Money left " + PlayerStats.Money);
    }
    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUi.Settarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUi.Hide();
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild ()
    {
        return turretToBuild;
    }
}
