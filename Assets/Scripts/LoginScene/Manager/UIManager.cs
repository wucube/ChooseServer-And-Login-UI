using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager
{
    //构造单例
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    /// <summary>
    /// 存储所有面板类的字典
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="BasePanel"></typeparam>
    /// <returns></returns>
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    //一开始就得到场景中的Canvas对象 
    private Transform canvasTrans;

    private UIManager()
    {
        //得到场景上创建好的 Canvas对象
        canvasTrans = GameObject.Find("Canvas").transform;
        //让 Canvas对象 过场景 不移除 
        //通过 动态创建 和 动态删除 来显示 隐藏面板的 所以不删除它 影响不大
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">必须继承BasePanel</typeparam>
    /// <returns></returns>
    public T ShowPanel<T>() where T : BasePanel
    {
        //只需要保证 泛型T的类型 和 面板名称 一致  定一个这样的规则 就非常方便使用
        string panelName = typeof(T).Name;

        //如果已经存在显示着的该面板，就不用创建直接返回给外部使用
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        //显示面板 就是 动态的创建面板预设体 设置父对象
        //根据得到的 类名 就是预设体面板名称 直接 动态创建它 即可
        GameObject panelObj = Object.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);

        //接着 得到对应的面板脚本 存储起来
        T panel = panelObj.GetComponent<T>();
        //把面板脚本存储到对应容器中 方便之后获取
        panelDic.Add(panelName, panel);
        //调用显示自己的逻辑
        panel.ShowMe();

        return panel;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade">如果希望 淡出 就默认传true 如果希望直接隐藏（删除）面板 那么就传false</param>
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        //根据 泛型类型 得到面板 名字
        string panelName = typeof(T).Name;
        //判断当前显示的面板 有没有该名字的面板类
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(() =>
                {
                    //面板 淡出成功后 希望删除面板
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //删除面板后 从 字典中移除
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                //删除面板
                GameObject.Destroy(panelDic[panelName].gameObject);
                //删除面板后 从 字典中移除
                panelDic.Remove(panelName);
            }
        }
    }

    //获得面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        //如果没有 直接返回空
        return null;
    }
}