using System;

public partial class HandleLeaderBorder
{
    // 排行榜查询
    // 协议参数：无
    // 返回协议：-1表示失败，0表示成功
    public void MsgLeaderBoard(Conn conn, ProtocolBase protoBase)
    {
        ProtocolBytes protocol = (ProtocolBytes) protoBase;
        string strFormat = "[收到请求排行榜协议] " + conn.GetAdress();
        Console.WriteLine(strFormat);
        // 构建返回协议
        ProtocolBytes protocolRet = new ProtocolBytes();
        protocolRet.AddString("LeaderBoard");
        // 获取排行榜
        conn.player.list = DataMgr.instance.GetRank();
        if (conn.player.list.Count == 0)
        {
            protocolRet.AddInt(-1);
            conn.Send(protocolRet);
            return;
        }
        // 返回
        protocolRet.AddInt(0);
        protocolRet.AddList(conn.player.list);
        conn.Send(protocolRet);
    }

    // 排行榜插入
    // 协议参数：无
    // 返回协议：-1表示失败，0表示成功
    public void MsgInsertLeaderBoard(Conn conn, ProtocolBase protoBase)
    {
        ProtocolBytes protocol = (ProtocolBytes) protoBase;
        string strFormat = "[收到请求插入排行榜协议] " + conn.GetAdress();
        Console.WriteLine(strFormat);
        // 构建返回协议
        ProtocolBytes protocolRet = new ProtocolBytes();
        protocolRet.AddString("InsertLeaderBoard");
        // 获取数值
        int start = 0;
        string protoName = protocol.GetString(start, ref start);
        string time = protocol.GetString(start, ref start);
        // 插入操作
        bool flag = DataMgr.instance.SaveRank(time);
        // 返回
        if (flag)        
            protocolRet.AddInt(0);
        else
            protocolRet.AddInt(-1);
        // 发送
        conn.Send(protocolRet);
    }
}