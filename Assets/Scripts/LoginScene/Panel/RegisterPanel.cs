using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 注册面板类脚本
/// </summary>
public class RegisterPanel : BasePanel
{
    //确实 取消 按钮
    public Button btnSure;
    public Button btnCancel;

    //账号密码输入框
    public InputField inputUN;
    public InputField inputPW;

    public override void Init()
    {
        //监听 取消按钮的事件处理函数
        btnCancel.onClick.AddListener(() =>
        {
            //隐藏注册面板
            UIManager.Instance.HidePanel<RegisterPanel>();
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
        });

        //监听 确定按钮的事件处理函数
        btnSure.onClick.AddListener(() =>
        {
            //判断输入的账号密码是否合法
            if (inputPW.text.Length <= 6 || inputUN.text.Length <= 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("账号和密码都必须大于6位");
                return;
            }

            //注册账号密码
            if (LoginManager.Instance.RegisterUser(inputUN.text, inputPW.text))
            {
                //清理上个账户的登录数据，用于新账号的数据重置
                LoginManager.Instance.ClearLoginData();

                //注册成功 显示登录面板
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                //更新登录面板上的用户名与密码
                loginPanel.SetInfo(inputUN.text, inputPW.text);

                //隐藏注册面板
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //提示 用户名已存在
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示内容
                tipPanel.ChangeInfo("用户名已存在");

                //清空输入框的内容，方便重新输入
                inputUN.text = "";
                inputPW.text = "";
            }


        });
    }
}
