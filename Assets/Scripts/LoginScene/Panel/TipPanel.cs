using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    //确定按钮
    public Button btnSure;
    //提示文字
    public Text txtInfo;

    public override void Init()
    {
        //初始化 按钮事件监听
        btnSure.onClick.AddListener(() => {
            //隐藏自己
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    /// <summary>
    /// 提示内容改变 提供给外部使用
    /// </summary>
    /// <param name="info">提示内容</param>
    public void ChangeInfo(string info)
    {
        txtInfo.text = info;
    }
}
