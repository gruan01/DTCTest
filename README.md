# DTCTest
分布式事务实例
> .NETCore 目前（1.1) 不支持System.Transactions , 所以本示例中的 .NETCore 项目都是无效代码。
# 运行
* 还数据迁移命令还原数据库（SQLServer） Data Source=.;Initial Catalog=Test;Integrated Security=True
* 在VS 中运行 WebApi2 或 WebForm 项目。
* 编译 Client 项目，运行 Client.exe 
* 选择是模拟提交还是模板回滚。
* 如果是模拟提交，会在 Tests 和 Logs 两个表中插入输出的 Guid 值。
* 如果是模拟回滚，在屏幕上输出的 GUID 不会插入这两个表中。
# MSDTC 设置
* 控制面板\所有控制面板项\管理工具
![设置](https://github.com/gruan01/DTCTest/blob/master/DTC.png)
