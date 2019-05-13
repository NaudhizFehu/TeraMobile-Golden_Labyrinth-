using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitScript : MonoBehaviour {

    public CruelTorturerControl Cruel;
    public KaidunsElteWarriorControl Kaidun;
    public JomporiControl Jompori;
    public ImpServantControl ImpServant;
    public FuryMaligothControl Maligoth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HPMinus(int Damage)
    {
        if (Cruel) Cruel.HPM(Damage);
        else if (Kaidun) Kaidun.HPM(Damage);
        else if (Jompori) Jompori.HPM(Damage);
        else if (ImpServant) ImpServant.HPM(Damage);
        else if (Maligoth) Maligoth.HPM(Damage);
    }
}
