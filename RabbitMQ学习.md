# 概念

- 生产者：发布消息，发送给交换器；
- 消费者：从队列中拿到消息进行处理并返回应答；
- 交换器：可能连接多个队列，将生产者产生的消息发送到队列中；
- 队列：存放生产者发布的消息，供消费者进行处理；

> 生产者发布消息给交换器（传递一个key值），交换器在跟它绑定的队列中根据key值及交换器模式找到匹配的队列发送消息，对应队列的消费者获取消息进行处理，并返回应答；
>
> 生产者可以直接发布消息到队列中；
>
> 如果交换器没有绑定任何队列，那发布的消息将直接丢弃；
>
> 一个消息只能被一个消费者获取，要实现消息同时被多个消费者获取，要使用交换器绑定多个队列；
>
> 消费者获取消息后，如果处理过程中失败了没有返回应答，那消息会在队列中重新发送；



# 生产者发布

```csharp
//连接工厂
var factory = new ConnectionFactory(){
    UserName="",
    Password="",
    HostName="",
    Port=0
};
//创建连接
var connection = factory.CreateConnection();
//创建通道
var channel = connection.CreateModel();
//声明交换器 direct为
channel.ExchangeDeclare("exchangeName","direct");
//声明队列
channel.QueueDeclare("queueName",durable:true);
//将交换器跟队列进行绑定
channel.QueueBind("queueName","exchangeName","routeKey",null);
//发布消息
channel.BasicPublish("exchangeName","routeKey",null,Encoding.UTF8.GetBytes("hello world"));

channel.Close();
connection.Close();
```



# 消费者订阅

```csharp
var factory = new ConnectionFactory
{
    UserName = "",
    Password = "",
    HostName = "",
};
//创建个连接
var connection = factory.CreateConnection();
//创建个通道
var channel = connection.CreateModel();
var consumer = new EventingBasicConsumer(channel);
//定义事件消费者，及消费接收事件（返回应答）
consumer.Received += (o, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"收到消息：{message}");
    channel.BasicAck(e.DeliveryTag, false);

};
//启动消费者
channel.BasicConsume("hello", false, consumer);

Console.WriteLine("消费者已启动");
Console.ReadKey();
channel.Close();
connection.Close();
```



# 交换器

- direct 消息发送到RouteKey完全匹配的队列中
- fanout 消息转发到交换器绑定的所有队列中
- topic 消息发送到RouteKey模糊匹配的队列中
- header 会用headers属性来进行匹配，性能最差（实际使用中很少）

### topic匹配规则

队列的key为TestRouteKey.#，可以匹配到 TestRouteKey.A.B

队列的key为TestRouteKey.*，可以匹配到TestRouteKey.A