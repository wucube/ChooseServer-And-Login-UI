using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用户注册管理器
/// </summary>
public class LoginManager
{
    private static LoginManager instance = new LoginManager();
    public static LoginManager Instance => instance;

    /// <summary>
    /// 用户登录数据
    /// </summary>
    private LoginData loginData;
    public LoginData LoginData => loginData;

    //用户注册数据
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;

    //所有服务器数据
    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData => serverData;


    private LoginManager() 
    {
        //通过JSON管理器 读取对应数据
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");

        //读取注册数据
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");

        //读取服务器数据
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region 登录数据
    /// <summary>
    /// 存储登录数据
    /// </summary>
    public void SavaLoginData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }

    /// <summary>
    /// 注册成功后，清除上个账号的部分登录数据
    /// </summary>
    public void ClearLoginData()
    {
        loginData.previousServerID = 0;
        loginData.autoLogin = false;
        loginData.rememberPW = false;
    }
    #endregion

    #region 注册数据

    /// <summary>
    /// 存储登录数据
    /// </summary>
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }

    /// <summary>
    /// 注册新用户
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    /// <returns></returns>
    public bool RegisterUser(string userName,string passWord)
    {
        //判断要注册的账户是否已存在
        if (registerData.registerInfo.ContainsKey(userName))
            return false;

        //要注册的账户不在字典中就能注册为新账户
        registerData.registerInfo.Add(userName, passWord);
        //存储账户信息到本地
        SaveRegisterData();

        //注册成功
        return true;
    }

    /// <summary>
    /// 验证用户名密码是否成功登录
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    /// <returns></returns>
    public bool CheckInfo(string userName, string passWord)
    {
        //判断是否存在该账户
        if (registerData.registerInfo.ContainsKey(userName))
        {
            //密码相同，证明登录成功
            if (registerData.registerInfo[userName] == passWord)
                return true;
        }

        //用户名与密码不合法
        return false;
    }
    #endregion

}
