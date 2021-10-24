using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour
{
    public float regenPerMinute = 1;
    public float maxEnergy = 1000;
    public float currentEnergy = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getCurrentEnergy(NewCharacterData data) {
        return data.currentEnergy;
    }

    public float getMaxEnergy(){
        return maxEnergy;
    }

    public void Drain(NewCharacterData data,float drain){
        if (data.currentEnergy > 0) data.currentEnergy -= drain;
        if (data.currentEnergy < 0) data.currentEnergy = 0;
    }
    public void Regenerate(NewCharacterData data){
        if (data.currentEnergy < maxEnergy) data.currentEnergy += regenPerMinute;
    }
}
