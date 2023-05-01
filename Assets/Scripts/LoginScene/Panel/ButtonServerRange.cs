using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 服务器区间按钮的脚本
/// </summary>
public class ButtonServerRange : MonoBehaviour
{
    public Button btnSelf;
    public Text txtInfo;

    //服区的范围
    public int beginIndex;
    public int endIndex;


    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(()=>{
            //通知选服面板 改变右侧的区间内容
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();

            panel.UpdatePanel(beginIndex, endIndex);
        });

    }

    /// <summary>
    /// 初始化服区按钮的显示范围
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void InitInfo(int beginIndex,int endIndex)
    {
        //记录当前区间按钮的区间值
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        //更新区间显示的内容
        txtInfo.text = beginIndex + " - " + endIndex + "区";
    }
}
