using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// 具体的选服按钮的脚本
/// </summary>
public class ButtonChooseServer:MonoBehaviour
{
    //按钮本身
    public Button btnSelf;
    //是否为新服
    public Image newServer;
    //服务器状态
    public Image ServerState;
    //服务器名称
    public Text ServerName;

    /// <summary>
    /// 当前按钮代表的服务器
    /// </summary>
    public ServerInfo currentServerInfo;

    private void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //记录当前选择的服务器ID
            LoginManager.Instance.LoginData.previousServerID = currentServerInfo.id;

            //隐藏 选服面板
            UIManager.Instance.HidePanel<ChooseServerPanel>();

            //显示 服务器面板
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }

    /// <summary>
    /// 初始化更新服区按钮信息显示
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(ServerInfo info)
    {
        //记录数据
        currentServerInfo = info;
        //更新服务区按钮上的信息
        ServerName.text = info.id + "区  " + info.name;
        //是否为新服
        newServer.gameObject.SetActive(info.isNew);
        //一开始就显示服区状态图组件
        ServerState.gameObject.SetActive(true);
        //从图集加载服务器状态图
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("ServerState");
        switch (info.state)
        {
            case 0:
                ServerState.gameObject.SetActive(false);
                break;
            case 1://流畅
                ServerState.sprite = spriteAtlas.GetSprite("ui_DL_liuchang_01");
                break;
            case 2://繁忙
                ServerState.sprite = spriteAtlas.GetSprite("ui_DL_fanhua_01");
                break;
            case 3://火爆
                ServerState.sprite = spriteAtlas.GetSprite("ui_DL_huobao_01");
                break;
            case 4://维护
                ServerState.sprite = spriteAtlas.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}