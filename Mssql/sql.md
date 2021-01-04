

# DDL 数据定义语言

## Create 创建对象

```sql
create database TestDb --创建数据库
create table TestTable(
	id int primary key
) --创建表
create procedure 
@p1
as
```

## Alter 修改对象

```sql

```



# DML 数据操纵语言

## Select查询数据

```mssql
Select * from Test_Table where value='1';
Select * from Test_Table t1 inner join Test_Table2 t2 on t1.value = t2.value where id='1';
```

## Insert创建数据

```mssql
Insert into Test_Table(field1,field2) values('123','456');
Insert into Test_Table(field1,field2) select value1,value2 from Test_Table; //批量创建
Select * into Test_Table from Test_Table2;//批量创建
```

## Update更新

```mssql
Update Test_Table set value = '123' where id = '1';
Update t1 set value = t2.value from Test_Table1 t1 inner join Test_Table2 t2 on t1.value1 = t2.value2 wher t1.id='1';
```



## Delete删除

```mssql
Delete from Test_Table where id='1';
```



# DCL 数据库控制语言



# 语法

```mssql
declare @p1 int = 5; --声明局部变量
if(1=1 and 2=2)
begin
	print '1'
end 
else
begin
	print '2'
end

while @p1<10
begin
	print @p1;
	set @p1 = @p1+1
end
```

# 全局变量

| 变量名       | 说明                       |
| ------------ | -------------------------- |
| @@VERSION    | SQL Server 数据库版本号    |
| @@IDENTITY   | 返回最后插入行的标识列的值 |
| @@ROWCOUNT   | 上一语句影响的行数         |
| @@SERVERNAME |                            |

# 运算符

| 运算符  |      |
| ------- | ---- |
| all     |      |
| and     |      |
| any     |      |
| between |      |
| exists  |      |
| in      |      |
| like    |      |
| not     |      |
| or      |      |
| some    |      |

# 函数 

| 函数名                                       |                |
| -------------------------------------------- | -------------- |
|                                              |                |
| substring(str,startIndex,len)                |                |
| getdate()                                    |                |
| datename(yy,date)                            |                |
| datepart()                                   |                |
| day                                          |                |
| month                                        |                |
| year                                         |                |
| datediff                                     |                |
| dateadd                                      |                |
| isdate('2020-09-05')                         |                |
| cast('123' as int)                           |                |
| convert(int,'123')                           |                |
| isnumeric('123')                             |                |
| newid()                                      |                |
| rank() over(partition by dep_id order by id) | 排名           |
| row_number()                                 | 行序号         |
| @@CURSOR_ROWS                                | 游标函数       |
| @@FETCH_STATUS                               | 上一条游标状态 |
| CURSOR_STATUS                                | 标量函数       |

```sql
select field1,field2 into table  from table2 --批量创建表，并插入数据
```

# 游标

## 声明游标

```sql
DECLARE cursor1 CURSOR FOR [keyset | ]
	select * from table1
```

## 打开游标

```sql
OPEN cursor1
```

## 获取游标数据

```sql
FETCH NEXT from cursor1
@@fetch_status -- 0为成功 -1不在结果集中 -2提取的行不存在
FOR UPDATE OF
where  CURRENT OF cursor1 -- 更新或删除当前游标的行
```

## 关闭游标

```sql
CLOSE c1
```

## 删除游标

```sql
DEALLOCATE c1
```

## 范例

```mssql
DECLARE @TEMP_VALUE char(36)

DECLARE cursor1 CURSOR for select  value from TEST_TABLE
OPEN cursor1
FETCH NEXT from cursor1 into @TEMP_VALUE
while @@FETCH_STATUS=0
begin
	
	FETCH NEXT FROM cursor1 into  @TEMP_VALUE
end

close cursor1
DEALLOCATE cursor1
```

