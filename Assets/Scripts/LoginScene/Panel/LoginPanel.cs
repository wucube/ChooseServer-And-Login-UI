using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 登录面板类
/// </summary>
public class LoginPanel : BasePanel
{
    //注册 确定登录按钮
    public Button btnRegister;
    public Button btnSure;

    //密码密码输入框
    public InputField inputUN;
    public InputField inputPW;

    //记住密码和自动登录 多选框
    public Toggle togRememberPW;
    public Toggle togAutoLogin;

    public override void Init()
    {
        //监听 注册按钮 事件的处理函数
        btnRegister.onClick.AddListener(() =>
        {
            //显示注册面板
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //隐藏登录面板
            UIManager.Instance.HidePanel<LoginPanel>();
        });

        // 监听 确定登录按钮 事件的处理函数
        btnSure.onClick.AddListener(() =>
        {
            //点击登录后要验证判断输入的账号密码是否合法
            if (inputPW.text.Length<=6 || inputUN.text.Length <= 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("账号和密码都必须大于6位");
                return;
            }

            //验证用户名与密码 是否通过
            if (LoginManager.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                //登录成功

                //记录数据
                LoginManager.Instance.LoginData.userName = inputUN.text;
                LoginManager.Instance.LoginData.passWord = inputPW.text;
                LoginManager.Instance.LoginData.rememberPW = togRememberPW.isOn;
                LoginManager.Instance.LoginData.autoLogin = togAutoLogin.isOn;
                //存储账户密码到本地
                LoginManager.Instance.SavaLoginData();

                //根据服务器信息，判断显示哪个面板
                if (LoginManager.Instance.LoginData.previousServerID <= 0)
                {
                    //如果从来没有选择过服务器，id为-1时，就直接打开选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                
                //隐藏登录面板
                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else
            {
                //登录失败
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码错误");
            }

        });

        //监听 记住密码选框的 事件处理函数
        togRememberPW.onValueChanged.AddListener((isOn) =>
        {
            //没有记住密码时，自动登录也不应勾选
            if (!isOn)
                togAutoLogin.isOn = false;
        });
        //监听 自动登录选框的 事件处理函数
        togAutoLogin.onValueChanged.AddListener((isOn) =>
        {
            //勾选自动登录时，如果记住密码没有勾选，就应选中
            if (isOn)
                togRememberPW.isOn = true;
        });

    }

    public override void ShowMe()
    {
        base.ShowMe();

        //显示登录面板时，根据数据更新面板中的数据

        //得到登录面板的数据
        LoginData loginData = LoginManager.Instance.LoginData;

        //更新记住密码与自动登录是否勾选
        togRememberPW.isOn = loginData.rememberPW;
        togAutoLogin.isOn = loginData.autoLogin;

        //更新账号
        inputUN.text = loginData.userName;
        //根据上次是否勾选记住密码 决定是否更新密码
        if (togRememberPW.isOn)
            inputPW.text = loginData.passWord;

        //如果勾选自动登录
        if(togAutoLogin.isOn)
        {
            //自动验证账号密码相关
            if (LoginManager.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                //根据上次的选服信息 判断要显示的面板
                if (LoginManager.Instance.LoginData.previousServerID <= 0)
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                else
                    UIManager.Instance.ShowPanel<ServerPanel>();
            }
            else
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("账号密码错误");
            }
            //隐藏登录面板
            UIManager.Instance.HidePanel<LoginPanel>(false);
        }
        
    }

    /// <summary>
    /// 提供给外部 快捷设置用户名与密码的方法
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    public void SetInfo(string userName,string passWord)
    {
        inputUN.text = userName;
        inputPW.text = passWord;
    }
}
