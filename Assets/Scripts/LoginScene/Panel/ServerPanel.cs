using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnStart;
    public Button btnChange;
    public Button btnBack;

    public Text txtServerName;

    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            //避免 自动登录时返回登录界面出现问题
            if (LoginManager.Instance.LoginData.autoLogin)
                LoginManager.Instance.LoginData.autoLogin = false;

            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
            //隐藏服务器面板
            UIManager.Instance.HidePanel<ServerPanel>();
        });

        btnStart.onClick.AddListener(() =>
        {
            //进入游戏
            //加载场景时Canvas对象不被移除，所以要隐藏面板
            UIManager.Instance.HidePanel<ServerPanel>();
            //隐藏登录背景图面板
            UIManager.Instance.HidePanel<LoginBKPanel>();

            //存储当前选择的服务器ID到本地
            LoginManager.Instance.SavaLoginData();

            SceneManager.LoadScene("GameScene");
        });
        btnChange.onClick.AddListener(() =>
        {
            //显示服务器列表面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();

            //隐藏服务器面板
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //显示服务器器面板时，更新当前选择的服务器名字
        //之后通过记录的上一次登录的服务器ID 更新

        int id = LoginManager.Instance.LoginData.previousServerID;

        if (id <= 0)
            txtServerName.text = "无选择";
        else
        {
            ServerInfo info = LoginManager.Instance.ServerData[id - 1];
            txtServerName.text = info.id + "区  " + info.name;
        }
        
    }
}
