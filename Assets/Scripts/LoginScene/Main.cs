using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        //显示提示面板
        UIManager.Instance.ShowPanel<LoginBKPanel>();
        UIManager.Instance.ShowPanel<LoginPanel>();
    }
}
