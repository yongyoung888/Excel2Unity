
//-----------------------------------------------
//              生成代码不要修改
//-----------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ChatCfg
{
    public int id;    //		Unnamed: 0
    public List<int> rewards;    //		奖励物品，JSON
    public string sceanid;    //		场景图片地址
    public string description;    //		Unnamed: 3

    public void Deserialize (DynamicPacket packet)
    {
        id = packet.PackReadInt32();
        rewards = SheetGenCommonFunc.GetListInt(packet.PackReadString());
        sceanid = packet.PackReadString();
        description = packet.PackReadString();
    }
}

public class ChatCfgMgr
{
    private static ChatCfgMgr mInstance;
    
    public static ChatCfgMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new ChatCfgMgr();
            }
            
            return mInstance;
        }
    }

    private Dictionary<int, ChatCfg> mDict = new Dictionary<int, ChatCfg>();
    
    public Dictionary<int, ChatCfg> Dict
    {
        get {return mDict;}
    }

    public void Deserialize (DynamicPacket packet)
    {
        int num = (int)packet.PackReadInt32();
        for (int i = 0; i < num; i++)
        {
            ChatCfg item = new ChatCfg();
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
    
    public ChatCfg GetDataByid(int id)
    {
        if(mDict.TryGetValue(id, out var chatCfg))
        {
            return chatCfg;
        }
        
        return null;
    }
}
