```powershell
#以下命令不区分大小写
Get-VM	#获取全部虚拟机
Get-VMProcessor -VMName xxx | fl	#查看虚拟机属性，ExposeVirtualizationExtensions属性代表嵌套虚拟化
Set-VMProcessor -ExposeVirtualizationExtensions $true -VMName xxx	#开启嵌套虚拟化
Stop-VM -Name xxx	#将虚拟机关机
```

