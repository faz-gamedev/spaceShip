using UnityEngine;


[System.Serializable]
public class SaveData 
{
    public string lastSaveTime;

    public int coin=1000;


    public int airshipActive=0;

    //airShip Unlock
    public bool[] airShipUnlock = {true,false,false,false,false,false};
}
