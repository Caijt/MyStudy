

Window版本：https://github.com/microsoftarchive/redis/releases

redis是单线程，所以不存在并发的问题

redis的命令都是原子性

https://www.redis.com.cn/commands/bitcount.html

# 持久化类型

- AOF 日志，将命令记录下来，重新启动后，将命令重新执行一遍
- RDB 定时将数据进行存储

# Key

```
keys * //查找所有的缓存key
keys h?llo //?匹配一个，例如hello hallo
keys h*llo //* 匹配多个，例如heello habllo
keys h[ab]llo //[]匹配其中一个，hallo，hbllo
keys h[^e]llo //[^]不匹配，不匹配hello
keys h[a-b]llo //hallo，hbllo

type key1 //获取类型，分别为string list set zset hash stream

del key1 key2 //删除缓存键值
dump key //序列化key,返回被序列化的值
exists key1 //判断键是否存在，可以批量判断，返回存在的数量
expire key1 30 //设置缓存时间，单位为秒，设置负数，会马上删除
expireat key1 1293840000//设置缓存绝对时间，以unix时间戳
pexpire key1 30 //跟expire一样，只是单位为毫秒
pexpireat key1 30 //跟expireat一样，只是精确到毫秒
ttl key1 //获取缓存剩余时间
pttl key1 //以毫秒获取剩余过期时间
persist key1 //清除缓存时间
migrate //迁移别的redis上
move key1 1 //将key1移到数据库db1
object //不知有什么用
randomkey //从数据库中随机返回一个key
rename key1 key2 //修改key名称
renamenx key1 key2 //用起来跟rename一起，不知有没有区别
restore key1 xxxxxxxx //将dump序列化的值，反序列化
//以下四个命令用法差不多，只是scan没有Key参数, match为匹配式，count是每次返回多少个，默认为10个，返回结果，有两个元素，第一个是要下次继续迭代的数字，第二个是本次迭代返回的key
scan 0 match f* count 10 //会返回两个元素，
sscan key1 0 match f* count 10//扫描set里面的键名，每次返回10个，默认为10
hscan //扫描set里面的键名，每次返回10个，默认为10
zscan //扫描set里面的键名，每次返回10个，默认为10

sort mylist //排序后返回，不影响原来的list，如果里面是数字，可以这样排序
sort mylist mylist2 //排序后存储在另一个list里

touch key1 key2 //修改最后访问时间
unlink key1//跟del差不多，只是这个命令不是阻塞的
wait 0 1000  //阻塞当前客户端
```



# 数据类型

## String

```
append key1 value //追加到key1原来的值后面，对不存在的key，相当于set

decr key1 //自减1，如果键 key 不存在， 那么键 key 的值会先被初始化为 0 ， 然后再执行 DECR 操作。
decrby key1 10 //key1值减去10

get key1 //获取一个string类型的值
getbit key1 0 1 //对 key 所储存的字符串值，获取指定偏移量上的位(bit)
getrange key1 0 1 //获取key1值指定范围
getset key1 value//将键 key1 的值设为 value ， 并返回键 key1 在被设置之前的旧值。

incr key1 //自增1
incrby key1 10 //自增10
incrbyfloat key1 10.5//给key1值加上浮点数

set key1 value //设置值，ex:设置缓存时间，单位为秒，px：设置缓存时间，单位为毫秒
setbit key1 7 1 //设置或清除指定偏移量上的位

mget key1 key2 //获取所有key值
mset key1 a key2 b //批量设置值
msetnx key1 a key2 b //批量设置值，如果其中一个有值就会出错

bitcount key1 1 1//统计字符串被设置为1的bit数
bitfield //有点复杂，不懂怎么用
bitop //位操作，不懂
bitpos //返回字符串里面第一个被设置为1或者0的bit位

setex key1 30 //设置值，并将生存时间设置为30秒
psetex key1 1000 value //以毫秒设置key1的生存时间
setnx key1 value //如果key1值不存在，才设置不成功
setrange key1 0 value //从偏移量offset 开始，用value参数覆盖键key1储存的字符串值
stralgo //用来实现基于字符串的复杂算法
strlen key1//获取指定key1所储存的字符串值的长度
```

## Hash

```bash
hset hash1 field1 value1 //设置字段值
hmset hash1 field1 value1 field2 value2 //批量设置字段值
hget hash1 field1 //返回字段值
hgetall hash1 //返回表所有的字段值
hmget hash1 field1 field2 //获取多个字段值
hkeys hash1 //返回存储在 hash1 中哈希表的所有域
hvals hash1 //返回所有的字段值
hdel hash1 field1 field2 //删除表中某些字段
hexists hash1 field1//判断字段是否存在

hincrby hash1 field1 10 //field1字段值自增10
hincrbyfloat hash1 field1 10.5 //field1字段值自增10.5

hlen hash1 //返回字段数量

hscan hash1 0 match k* count 10 //遍历表中的键值
hsetnx hash1 field1 value1 //field1不存在才设置成功
hstrlen hash1 field1 //返回field1值的长度
```

## List 列表

```
linsert list1 BEFORE value value1 //用于把value1插入到列表list1中参考值value的前面或后面
llen list1 //返回列表长度
lmove list1 list2 RIGHT LEFT  //将list1列表最右边一个移动到list2列表最左边的位置
lpop list1 //删除并返回list1列表（左边）第一个元素
lpos list1 value1//在list1列表中查找value1元素的索引值，参数rank返回第几个匹配，参数count返回前几个匹配的成员
lindex list1 0 //返回对应索引位置的元素
lpush list1 value1 value2//往list1列表左侧（第一个）插入元素，插入的顺序从左到右
lpushx list1 value1//跟lpush一样，区别就是只有当list1存在时才会插入
lrange list1 0 10 //返回索引0到10的元素
lrem list1 2 hello//从列表list1中删除前 2 个值等于 hello 的元素
lset list1 0 hello //设置对应索引位置上的值
ltrim list1 0 2 //只保留0到2的三个元素，其余删除
rpop list1 //移除并返回list1列表中最后一个（右边）元素
rpush list1 value1 value2//往list1列表最后（右侧）插入元素
rpushx list1 value1 //跟rpush一样，区别是list1列表存在才成功
rpoplpush list1 list2//移除list1列表最后一个元素，并插入到list2列表第一个元素（已废弃，用lmove替代）

blmove //是lmove的阻塞版本
blpop //是lpop的阻塞版本
brpop //是rpop的阻塞版本
brpoplpush //rpoplpush的阻塞版本
```

## Set 不重复的集合

```
sadd set1 value1 value2 value2 //将一个、多个元素添加到set1集合中
scard set1 //返回set1集合中元素数量
sdiff set1 set2 set3//返回第一个集合与其他集合之间的差异
sdiffstore set4 set1 set2 set3//命令的作用和SDIFF类似，不同的是它将结果保存到 destination 集合
sinter set1 set2 //获取两集合交集，两集合都存在的元素
sinterstore set3 set1 set2 //获取两集合交集，将结果存在set3
sismember set1 value1 //判断value1是否存在于set1集合中
smembers set1 //显示集合中所有的元素
smismember set1 value1 value2 //判断value1、value2是否存在于set1集合中
smove set1 set2 value1 //从set1集合中移动value1到set2集合中
spop set1 3 //命令用于从集合set1中删除并返回一个或多个随机元素
srandmember set1 2//从set1集合中随机返回一个或多个元素
srem set1 value1 value2//从set1集合中删除value1 value2
sscan set1 0 match * count 10 //遍历集合
sunion set1 set2 //获取并集
sunionstore set3 set1 set2 //获取并集，将结果存在set3集合中
```

## Sorted Set 有顺序的不重复的集合

```
zadd zset1 1 value1 2 value2 3 value3 //将一个或多个元素添加到有序集合中
zcard zset1 //返回zset1集合中元素数量
zcount zset1 1 2 //获取集合中分值为1到2之间的元素数量
zincrby zset1 2 value1 //zset1集合中的value1的分值自增2
zinter 2 zset1 zset2//交集
zinterstroe zset3 2 zset1 zset2 //交集，结果存储在zset3
zlexcount zset1 - +//
zmscore zset1 value1 value2//返回元素的分值
zpopmax zset1 //删除分数最大的元素
zpopmin zset1 //删除分数最小的元素
zrange zset1 0 -1 withscores //返回集合范围内的元素
zrangebylex zset1  [value1 [value2//
zrangebyscore zset1 1 3 //返回分值在1到3之间的元素
zrangebyscore zset1 (1 (3 // 1 < score < 3
zrank zset1 value1 //返回value1的排名
zrem zset1 value1 value2 //删除集合中的元素
zremrangebylex zset1 [value1 [value2 //删除在value1跟value2之间的元素
zremrangebyrank zset1 1 2 //删除排名在1到2的所有元素
zremrangebyscore zset1 1 2 //删除分值在1到2之间的元素
zrevrange zset1 0 -1//跟zrange一样，区别是从高到低排序
zrevrangebylex zset1 [value1 (value2 //跟zrangebylex一样，区别是从高到低排序
zrevrangebyscore zset1 1 3 //跟zrangebyscore一样，区别是从高到低排序
zrevrank zset1  value1//同zrank，区别是排名从高到低
zscan zset1 0
zscore zset1 value1 //返回成员的分值
zunion zset1 //并集
zunionstore zset3 2 zset1 zset2 //并集，结果存储在zset3中
带有lex的命名 (：代表小于、大于不等于 [：代表小于等于、大于等于
```

## 订阅发布

```
psubscribe test_channel //订阅信道
publish test_channel "hello world" //发布消息到某个信道中
pubsub channels //列出当前活跃的信道
punsubscribe //退订
subscribe test_channel //订阅信道
unsubscribe //退订
```

## 事务

```
multi //事务开始
exec //执行事务内所有命令

discard //取消事务
watch key1 //监控一个或多个键，一旦修改或删除，之后的事务就不会执行
unwatch //取消所有监控
```

## 连接

```
ping //测试服务器是否正常，正常就返回参数或pong
quit //关闭连接
select 1//切换数据库
echo hello //测试输出
```

