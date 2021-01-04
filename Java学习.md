Spring基础包下载： https://repo.spring.io/simple/libs-release-local/org/springframework/spring/

commons.logging包下载：http://commons.apache.org/proper/commons-logging/download_logging.cgi 

下载以上5个包后，放在项目的lib目录

# IOC容器

两种容器

### 1、BeanFactory 基础类型

```java
BeanFactory beanFactory = new XmlBeanFactory(new FileSystemResource("d://applicationContext.xml"));
```

### 2、ApplicationContext 加强版（一般用这个）

```java
ApplicationContext applicationContext = new ClassPathXmlApplicationContext("d://applicationContext.xml");//一般使用这种

ApplicationContext applicationContext = new FileSystemXmlApplicationContext("d://applicationContext.xml");
```



applicationContext.xml 是定义IOC所有类的文件

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<beans xmlns="http://">
	<bean id="personDao" class="com.mengma.ioc.PersonDaoImpl"></bean>
 </beans>
```

```java
public interface PersonDao{
    public void add();
}
public class PersonDaoImpl implements PersonDao{
    @Override
    public void add(){
        System.out.println("执行了");
    }
}
```



```java
string xmlPath = "applicationContext.xml";
ApplicatonContext context = new ClassPathXmlApplicationContext(xmlPath);
PersonDao personDao = context.getBean("personDao");
```

