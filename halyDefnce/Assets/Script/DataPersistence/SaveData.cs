using UnityEngine;


[System.Serializable]
public class SaveData 
{
    public string lastSaveTime;

    public int coin=1000;


    public int airshipActive=0;
    public bool seasonLock=false;

    //airShip Unlock

    public bool[] airShipUnlock = {true,false,false,false,false,false};
    public float[] rcord = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public float[] rcord2 = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public int[] star = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public int[] star2 = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
}
