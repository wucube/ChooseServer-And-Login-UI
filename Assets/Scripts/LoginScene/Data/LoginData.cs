using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 需要存储的用户登录数据
/// </summary>
public class LoginData
{
    //用户名
    public string userName;
    //用户密码
    public string passWord;

    //是否记住密码
    public bool rememberPW;
    //是否自动登录
    public bool autoLogin;

    //上次登录的服务器ID，0表示上次没有登录服务器
    public int previousServerID = 0;

}
