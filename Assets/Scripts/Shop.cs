using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurretBlueprint StanderdTurret;
    public TurretBlueprint RocketTurret;
    public TurretBlueprint LaserTurret;
    public TurretBlueprint BarrleTurret;
    public TurretBlueprint OilPump;
    public TurretBlueprint Mortar;


    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStanderdTurret()
    {
        Debug.Log("Standerd Turret Purchased");
        buildManager.SelectTurretToBuild(StanderdTurret);
    }
    public void SelectRocketTurret()
    {
        Debug.Log("Rocket Turret Purchased");
        buildManager.SelectTurretToBuild(RocketTurret);
    }
    public void SelectLaserTurret()
    {
        Debug.Log("Laser Turret Purchased");
       buildManager.SelectTurretToBuild(LaserTurret);
    }
    public void SelectBarrleGunTurret()
    {
        Debug.Log("Barrle Gun Turret Purchased");
       buildManager.SelectTurretToBuild(BarrleTurret);
    }
    public void SelectOilPump()
    {
        Debug.Log("Oil Pump Purchased");
        buildManager.SelectTurretToBuild(OilPump);
    }
    public void SelectMortar()
    {
        Debug.Log("Mortar Purchased");
        buildManager.SelectTurretToBuild(Mortar);
    }
}

