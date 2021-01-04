### 指令

`ng-app` 应用



`ng-init` 初始化变量

```html
<div ng-app="" ng-init="param1='这是一个变量';params2='这也是一个变量'">
    {{param1}}
    {{param2}}
</div>
```

`ng-bind` 绑定变量输出



`ng-repeat`循环

```html
<div ng-repeat="x in arr">
    {{x}}
</div>
```

`ng-disabled` 绑定元素的`disabled`属性

`ng-show` 显示

`ng-hide` 隐藏

`ng-model` 双向绑定变量

`ng-controller` 控制器

```html
<div ng-app="myApp" ng-controller="myCtrl">
    <p>
        {{param}}
    </p>
    <p>
        {{sayHello()}}
    </p>
</div>
<script>
    var app = angular.module("myApp",[]);
    app.controller("myCtrl",function($scope){
        $scope.param="这是在控制器里定义的变量";
        $scope.sayHello = function(){
            return "Hello World";
        }
    });
</script>
```

### 创建指令

```javascript
var app = angular.module("myApp", []);
app.directive("runoobDirective", function() {
    return {
        template : "<h1>自定义指令!</h1>"
    };
});
```

$scope 控制器作用域内

$rootScope 整个应用作用域内



### 过滤器

```javascript

var app = angular.module('myApp', []);
app.controller('myCtrl', function($scope) {
    $scope.msg = "Runoob";
});
app.filter('reverse', function() { //可以注入依赖
    return function(text) {
        return text.split("").reverse().join("");
    }
});
```

### 服务

$http

```javascript
$http({
    method:"GET",
    url:"/"
}).then((res)=>{
    //成功时
},(res)=>{
    //失败时
})
$http.get("/",config).then(()=>{},()=>{});
$http.post("/",data,config).then(()=>{},()=>{});
```



$location

$timeout

$interval

```javascript
//创建自定义服务
app.service('myService', function() {
    this.myFunc = function (x) {
        return x.toString(16);
    }
});
//使用，以注入的形式
app.controller("myCtrl",function($scope,myService){
    
});
```

### Select

```html
<select ng-model="" ng-options="x for x in arr">
    
</select>
```

### 事件

ng-click 点击

### 模块

```
angular.module("myApp",[]);//后面的中括号为依赖模块
```

### 表单



### 包含

ng-include="1.html"



### 依赖注入

```javascript
var app = angular.module("myApp",[])
app.value("myValue",5);
app.factory("myFactory",function(){
   var factory = {};   
   factory.multiply = function(a, b) {
      return a * b
   }
   return factory;
})
//在控制器上注入
app.controller("myCtrl",function($scope,myValue,myFactory){
    
});
```

### 路由

```html
<body>
    <div ng-view></div>
</body>

<script>
	var myApp = angular.module('myApp',['ngRoute']);
    myApp.config(["$routeProvider",function($routeProvider){
        $routeProvider.when("/",{ 
            templateUrl:"pages/home.html"
        })
        .otherwise({redirectTo:"/"})
        
    }])
</script>

```

