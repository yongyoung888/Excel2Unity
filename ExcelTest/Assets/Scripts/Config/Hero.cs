
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class HeroCfg
{
    public int ID;    //		Id
    public string Name;    //		名字
    public string HeadIcon;    //		头像
    public string Model;    //		模型

    public void Deserialize (DynamicPacket packet)
    {
        ID = packet.PackReadInt32();
        Name = packet.PackReadString();
        HeadIcon = packet.PackReadString();
        Model = packet.PackReadString();
    }
}

public class HeroCfgMgr
{
    private static HeroCfgMgr mInstance;
    
    public static HeroCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new HeroCfgMgr();
            }
            
            return mInstance;
        }
    }

    private Dictionary<int, HeroCfg> mDict = new Dictionary<int, HeroCfg>();
    
    public Dictionary<int, HeroCfg> Dict
    {
        get {return mDict;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            HeroCfg item = new HeroCfg();
            item.Deserialize(packet);
            if (mDict.ContainsKey(item.ID))
            {
                mDict[item.ID] = item;
            }
            else
            {
                mDict.Add(item.ID, item);
            }
        }
    }
    
    public HeroCfg GetDataByID(int id)
    {
        if(mDict.TryGetValue(id, out var heroCfg))
        {
            return heroCfg;
        }
        
        return null;
    }
}
