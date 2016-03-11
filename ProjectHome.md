创建该项目的目的是有两个：

1.让想学习ORM工具的开发人员有一个完整的例子来学习。可以理解UnitOfWork模式，Repository模式等等。

2.希望有空闲时间的开发或者测试人员能够提供多个工具的性能对比测试数据。

你可以在上面的download菜单下下载源代码，或者为了更方便的获取最新代码，你可以使用svn checkout。
需要注意的是，代码中的MVC3 project使用了IIS Expre,如果未安装IIS Express请去掉.csproj文件中的

&lt;UseIISExpress&gt;

true

&lt;/UseIISExpress&gt;

节点

计划：

> @再加入几个ORM工具的例子；

> @增加关系表的操作例子；

> @优化UnitOfWork模式;

> @优化并深入IoC工具的使用说明;

> 想更多的了解这个项目的东西，请阅读： http://www.cnblogs.com/n-pei/archive/2011/09/06/2168433.html