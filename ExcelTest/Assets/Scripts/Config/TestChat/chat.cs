
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class chatCfg
{
    public int id;    //		
    public List<int> rewards;    //		奖励物品，JSON
    public string sceanid;    //		场景图片地址
    public string description;    //		

    public void Deserialize (DynamicPacket packet)
    {
        id = packet.PackReadInt32();
        rewards = SheetGenCommonFunc.GetListInt(packet.PackReadString());
        sceanid = packet.PackReadString();
        description = packet.PackReadString();
    }
}

public class chatCfgMgr
{
    private static chatCfgMgr mInstance;
    
    public static chatCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new chatCfgMgr();
            }
            
            return mInstance;
        }
    }

    private Dictionary<int, chatCfg> mDict = new Dictionary<int, chatCfg>();
    
    public Dictionary<int, chatCfg> Dict
    {
        get {return mDict;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            chatCfg item = new chatCfg();
            item.Deserialize(packet);
            if (mDict.ContainsKey(item.id))
            {
                mDict[item.id] = item;
            }
            else
            {
                mDict.Add(item.id, item);
            }
        }
    }
    
    public chatCfg GetDataByid(int id)
    {
        if(mDict.ContainsKey(id))
        {
            return mDict[id];
        }
        
        return null;
    }
}
