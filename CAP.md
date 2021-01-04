http://cap.dotnetcore.xyz/ 

https://github.com/dotnetcore/CAP

# 概念

微服务分布式事务解决方案

可以搭建不同站点，使用CAP，连接同一个RabbitMQ，部署在不同的服务器上，实现分布式部署

需要一个数据库来记录事件

需要一个队列来存放事件消息



安装包 DotNetCore.CAP

安装包 DotNetCore.CAP.RabbitMQ 

在Startup.cs的ConfigureServices方法中注入

```csharp
services.AddCap(o=>{
    o.UseSqlServer("");
    o.UseRabbitMQ(mq => {
        mq.HostName = "";//RabbitMQ服务器地址
        mq.Port=5672;
        mq.UserName = "admin";
        mq.Password = "admin";
    });
    o.UseDashboard(); //添加监控仪表盘，通过http://localhost/cap访问

    o.FailedRetryInterval = 30;//失败后的重拾间隔，默认60秒
    o.FailedRetryCount = 10;//失败后的重试次数，默认50次；在FailedRetryInterval默认60秒的情况下，即默认重试50*60秒(50分钟)之后放弃失败重试
    o.SucceedMessageExpiredAfter = 60 * 60; //设置成功信息的删除时间默认24*3600秒
});
```

## 发布与订阅代码示例

```csharp
public class TestCap{
    private readonly ICapPublisher _capPublisher;
    public TestCap(ICapPublisher capPublisher){
        _capPublisher = capPublisher;
    }
    //发布事件
    public void TestPublish(){
    	await _capPublisher.Publish<string>("Test.Event","hello world","回调事件")
	}
    //订阅事件
    [CapSubscribe("Test.Event")]
    public string TestCapSubscribe(string message){
		Console.WriteLine(message);
        return "OK";
    }
    [CapSubscribe("回调事件")]
    public void TestCallback(string result){
		Console.WriteLine(result);
    }
}

```

