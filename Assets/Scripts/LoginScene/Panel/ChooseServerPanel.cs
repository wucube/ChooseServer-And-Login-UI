using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// 选服面板类脚本
/// </summary>
public class ChooseServerPanel : BasePanel
{
    //左右滚动视图
    public ScrollRect svLeft;
    public ScrollRect svRight;

    //上一次登录的服务器信息
    public Text perviousServerName;
    public Image perviousServerState;

    //当前服区的选择范围 
    public Text ServerRange;

    //存储右侧选服按钮的列表
    private List<GameObject> buttonServerList = new List<GameObject>();


    // 先动态创建左侧的 服务器区间按钮
    public override void Init()
    {
        //获取服务器列表的数据
        List<ServerInfo> serverInfoList = LoginManager.Instance.ServerData;


        //得到一共要循环创建区间按钮的数量
        //向下取整，所以要 +1，结果代表服务器数量平分为num个区间按钮，区间长度为5 
        int serverRangeNum = serverInfoList.Count / 5 + 1;

        //循环创建出所有区间按钮
        for (int i = 0; i < serverRangeNum; i++)
        {
            //动态创建预制体对象并设置位置
            GameObject btnServerRange = Instantiate(Resources.Load<GameObject>("UI/btnServerRange"));
            btnServerRange.transform.SetParent(svLeft.content, false);

            //初始化服务器区间按钮
            ButtonServerRange ServerRange = btnServerRange.GetComponent<ButtonServerRange>();
            int beginIndex = i * 5 + 1;
            int endIndex = 5 * (i + 1);
            
            //判断区间最大的服务器是否超过服务器总数
            if (endIndex > serverInfoList.Count)
                endIndex = serverInfoList.Count;

            ServerRange.InitInfo(beginIndex, endIndex);
        }
    }

    // 再动态创建右侧的 选服按钮
    public override void ShowMe()
    {
        base.ShowMe();

        //显示选服面板时，应初始化上一次选择的服务器
        int id = LoginManager.Instance.LoginData.previousServerID;

        if (id <= 0) //上次的服务器ID小于等于0，就代表没有上次没有选服
        {
            perviousServerName.text = "无";
            perviousServerState.gameObject.SetActive(false);
        }
        else
        {   //服务器ID从 1 开始，服务器数据的List索引从 0 开始 
            ServerInfo serverInfo = LoginManager.Instance.ServerData[id - 1];
            //拼接显示上次登录的服务器名称
            perviousServerName.text = serverInfo.id + "区  " + serverInfo.name;
            //先显示服务器状态图
            perviousServerState.gameObject.SetActive(true);

            //从图集中加载服务器状态图
            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("ServerState");
            switch (serverInfo.state)
            {
                case 0:
                    perviousServerState.gameObject.SetActive(false);
                    break;
                case 1://流畅
                    perviousServerState.sprite = spriteAtlas.GetSprite("ui_DL_liuchang_01");
                    break;
                case 2://繁忙
                    perviousServerState.sprite = spriteAtlas.GetSprite("ui_DL_fanhua_01");
                    break;
                case 3://火爆
                    perviousServerState.sprite = spriteAtlas.GetSprite("ui_DL_huobao_01");
                    break;
                case 4://维护
                    perviousServerState.sprite = spriteAtlas.GetSprite("ui_DL_weihu_01");
                    break;
            }
        }
        UpdatePanel(1, 5 > LoginManager.Instance.ServerData.Count ? LoginManager.Instance.ServerData.Count : 5);

    }
    /// <summary>
    /// 更新 选择当前区间时的右侧选服按钮信息
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void UpdatePanel(int beginIndex,int endIndex)
    {
        //更新服务器区间显示
        ServerRange.text = "服务器 " + beginIndex + "-" + endIndex;

        //先删除之前的单个服区器按钮
        for (int i = 0; i < buttonServerList.Count; i++)
        {
            Destroy(buttonServerList[i]);
        }
        //清空存储右侧服务器按钮的List
        buttonServerList.Clear();

        //再创建新的右侧服务器按钮
        for(int i = beginIndex; i <= endIndex; i++)
        {
            //服务器的ID从1开始，List的索引从0开始，所以要 -1
            ServerInfo currentInfo = LoginManager.Instance.ServerData[i - 1];

            //动态创建服务器信息按钮
            GameObject buttonServer = Instantiate(Resources.Load<GameObject>("UI/btnServer"));
            buttonServer.transform.SetParent(svRight.content, false);

            //根据信息，更新按钮数据
            ButtonChooseServer btnChooseServer = buttonServer.GetComponent<ButtonChooseServer>();
            btnChooseServer.InitInfo(currentInfo);

            //创建完成后，将按钮对象记录到List中
            buttonServerList.Add(buttonServer);

        }
    }
}
