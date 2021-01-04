##### 其它

```bash
which 命令	#查看命令在哪
whereis 命令
ifup eth0	#启用网卡
ifdown eth0	#关闭网卡
netstat -antp	#查看端口，a所有,n以数字显示端口，t显示tcp，u显示udp，p显示pid，l显示正在监听
ss		#跟netstat差不多，参数也差不多，比netstat更详细，s汇总，a所有，n以数字显示端口，
free -h	#查看内存占用率
top			#实时查看CPU及内存的使用情况，us用户占用，sy核心占用，ni不知，id空闲
hostnamectl	#主机台管理
uname -sr	#查看内核版本
arch	#查看CPU架构平台，也可以uname -m
cat /etc/centos-release	#查看系统版本
```

##### 包管理

```bash
yum list installed | grep xxx #查看通过yum安装的包
yum search xxx			#通过关键字查找包
yum install xxx	
yum remove xxx
yum install --downloadonly --downloaddir=/root/rpms postgresql	#仅下载不安装
yum localinstall *.rpm		#安装本地rpm包，可以配合上面的命令下载到本地的包
yum grouplist
yum list available		#显示可安装的包
yum remove xxx			#删除某包
yum repository-packages base list			#只显示某个源下的包
yum repository-packages base install xxx	#指定在哪个源下安装包
yum deplist xxx			#查看包依赖关系

rpm -qa | grep xxx 		#查看通过rpm安装的包
rpm -ivh xxx 			#安装并显示详情及进度
rpm -Uvh xxx			#更新并显示详情及进度
rpm -e xxx				#卸载
rpm -ql xxx				#查看包安装位置
rpm -V					#检查rpm文件有没有被修改过
rpm -qR xxx				#查看包的依赖关系
```

##### 服务

```bash

systemctl enable xxx	#将服务设置为自动启动
systemctl disable xxx	#禁止服务自启动
systemctl start xxx		#启动某个服务
systemctl restart xxx 	#重启某服务
systemctl stop xxx 		#停止服务
systemctl status xxx 	#查看服务状态
systemctl list-units | grep xx
systemctl list-unit-files | grep xxx #查看服务启动状态
systemctl --failed		#查看启动失败的服务
systemctl daemon-reload	#重载服务内容，修改服务内容后需进行这个操作

systemctl get-default	#查看当前启动默认交互类型
systemctl set-default  multi-user.target	#设置启动默认命令行
```

##### 进程

```bash
ps aux #以bsd样式显示所有进程，a显示所有终端进程，u显示用户及内存，x显示没有控制终端的进程
lsmod	#已加载的模块
```

##### 硬盘管理

```bash
lsblk	#以树型查看硬盘结构
df -Th	#查看分区文件系统，T是类型，h是以适合的单位显示容量

fdisk -l	#查看mbr硬盘
fdisk /dev/sda	#管理mbr硬盘
gdisk -l	#查看gpt硬盘
gdisk /dev/sda	#管理gpt硬盘
mkfs -t xfs /dev/sdb1	#以xfs格式化分区
```

##### LVM存储管理

```bash
pvdisplay	#查看物理分区
lvdisplay	#查看逻辑分区
vgdisplay	#查看卷组
lvextend -l +100%FREE /dev/centos/root	#将lvm里剩余的空间全部加到root
xfs_growfs /dev/centos/root	#在线调整分区大小
pvcreate /dev/sda3	
```

##### 防火墙

```bash
firewall-cmd --zone=public --add-port=80/tcp --permanent #添加80端口
firewall-cmd --zone=public --remove-port=80/tcp --permanent #添删除
firewall-cmd --zone=public --add-service= --permanent	#增加服务
firewall-cmd --zone=public --list-ports #查看开放的端口
firewall-cmd --zone=public --list-services
firewall-cmd --get-services
firewall-cmd --reload #重新加载配置
firewall-cmd --state	#查看防火墙状态
```

##### Kvm虚拟机管理

```bash
#相关工具包
libvirt 		#必装的核心管理工具
qemu-kvm 		#必装
virt-install	#基于libvirt服务虚拟机创建命令，用命令创建虚拟机
bridge-utils 	#管理桥接，centos7本来就有了

qemu-kvm-tools 
qemu-img 		
virt-manager 	#虚拟机图形化管理，宿主机有桌面环境可考虑安装
libvirt-python 
libvirt-client 
virt-viewer 

#以下命令依赖包virt-install
ctrl+] #切换到宿主机，跳出虚拟机
virsh list --all	#查看所有虚拟机，加all列出关机状态的
virsh console xxx	#以控件台连接到指定虚拟机
virsh start xxx		#启动虚拟机
virsh shutdown xxx	#关闭虚拟机，一般关不了
virsh destroy xxx	#强制关闭虚拟机
virsh autostart xxx	#设置虚拟机随机启动
virsh undefine xxx	#删除虚拟机，只会删除对应的xml，硬盘文件不会删除
virsh autostart xxx	#设置虚拟机自动启动
virsh edit xxx		#编辑虚拟机配置
virsh suspend xxx	#挂起虚拟机
virsh resume xxx	#恢复挂起的虚拟机
virsh attach-disk	#附加硬盘
virt-clone -o xxx -n newvm -f /data/vm/new.qcow2	#克隆虚拟机

#virt-install参数
--name=xxx						#虚拟机唯一名称
--memory=1024[,maxmemory=2048]	#虚拟机内存，单位为mb --memory=1024,maxmemory=2048
--vcpus=1[,maxvcpus=4]			#虚拟机CPU数量
--cdrom=/xxx/xxx				#指定安装源文件
--location=/xxx/xxx				#指定安装源文件，跟cdrom二选一，如果要用控制台安装得用这个，配合--extra-args参数
--disk path=/xx/xxx[,size=10,format=raw]	#存储文件及格式
--graphics vnc,port=xxx,listen=xxx #图形化，用vnc安装，不用图形化用--graphics none --extra-args="console=ttyS0"
--network bridge=br0	#网络连接方式
--os-variant=xxx #对应的系统值，可以osinfo-query os这个查对应值
--virt-type=kvm		#虚拟机类型
--noautoconsole	#不自动连接，默认是安装时用virt-viewer或者virsh console去连接虚拟机

#vnc图形连接安装命令示例
virt-install --name=c7 --memory=1024 --vcpus=1 --cdrom=/data/iso/CentOS-7-x86_64-Minimal-1810.iso --disk path=/data/vm/c7.qcow2,size=5,format=qcow2 --network bridge=br0 --virt-type=kvm --os-variant=centos7.0 --graphics vnc,listen=0.0.0.0,port=5900 --noautoconsole

#无图形控制台命令安装
virt-install --name=c7-2 --memory=1024 --vcpus=1 --location=/data/iso/CentOS-7-x86_64-Minimal-1810.iso --disk path=/data/vm/c7-2.qcow2,size=5,format=qcow2 --network bridge=br0 --virt-type=kvm --os-variant=centos7.0 --graphics none --extra-args 'console=ttyS0'
```

##### Selinux

```bash
sestatus	#查看selinux状态
setenforce 0	#临时关闭selinux
将/etc/selinux/config里的改成disabled	#永久关闭selinux
```

##### Vnc远程连接

```bash
yum install tigervnc-server		#安装远程桌面服务
vncpasswd	#创建密码
vncserver	#启动服务
cp /lib/systemd/system/vncserver@.service /etc/systemd/system/vncserver@:1.service
把里面的<USER>改成root
systemctl enable vncserver@:1.service	#将vnc服务设置为自动启动
```

##### 网卡桥接

```bash
yum install bridge-untils	#依赖包
brctl show			#查看桥接网卡
brctl addbr br0		#增加桥接网卡
brctl addif br0 eth0	#将eth0绑定到桥接网卡br0
brctl delif br0 eth0	#将eth0从br0中取消绑定
```

##### 解压缩包

```bash
tar -zxvf xxx.tar.gz 	#解压一般用法，z:用于tar.gz类型，x:是解压，v:显示过程，f:必须的，后面接文件名
```

##### 源代码编译安装

```
./configure
make clean;make
make install
```

##### 挂载

```bash
mount -o loop /data/iso/xxx.iso /mnt/cdrom	#挂载iso文件
```

##### Nginx

```bash
nginx -t 	#检查配置文件是否有错误
nginx -s reload	#重启服务
nginx -s stop	#强制退出
nginx -s quit	#正常退出
nginx -c xxx	#使用配置文件
```

##### mysql

```bash
mysql -uroot -p [数据库名]	#连接数据库
mysql --user=root --password [数据库名]	#同上
grant all on *.* to user@"localhost" identified by "123456";
```

