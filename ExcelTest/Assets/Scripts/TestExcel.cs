using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExcel : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        string configpath = Application.streamingAssetsPath + "/Config/Config.data";
        ConfigManager.LoadConfig(configpath);
        
        HeroCfg heroCfg = HeroCfgMgr.Instance.GetDataByID(1001);
        Debug.Log("HeroCfg ID: " + heroCfg.ID + " Name: " + heroCfg.Name + " HeadIcon: " + heroCfg.HeadIcon + " Model: " + heroCfg.Model);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
