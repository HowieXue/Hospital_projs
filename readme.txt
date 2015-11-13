nuget命令用法：
获取列表
Get-Package -ListAvailable

添加组件
Install-Package 组件名

添加特定版本组件
Install-Package 组件名 -Version 版本号

移除组件
Uninstall-Package 组件名

强制移除组件
Uninstall-Package 组件名 ?Force

升级组件
Update-Package 组件名
