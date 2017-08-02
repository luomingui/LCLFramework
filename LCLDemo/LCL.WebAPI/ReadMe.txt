ASP.NET Domain-Driven Design
AND
RESTful API 设计最佳实践


GET  /member      # 获取ticket列表
GET  /member/12   # 查看某个具体的ticket
GET  /member?id=1&username=admin&sort=&order=desc&page=1&rows=10

POST     /member    //member添加操作
PUT      /member/1  //对member下id为1的资源进行修改操作：完全替换
PATCH    /member/1  //对member下id为1的资源进行修改操作：局部更新
DELETE   /member/1  //对member下id为1的资源进行删除操作



api
  api/v1/{area}/{controller}/{id}
  api/v1/{area}/{category}/{controller}/{id}