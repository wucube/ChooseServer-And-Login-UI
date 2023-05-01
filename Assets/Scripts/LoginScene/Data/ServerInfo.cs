using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个服务器数据
/// </summary>
public class ServerInfo
{
    //数据字段要与JSON数据中的key值对应，因为要字段接收json中的数据

    //服务器区别 ID
    public int id;
    //服务器名称
    public string name;
    //服务器状态 0~4 为5种状态
    public int state;
    //是否为新服
    public bool isNew;
}
