https://ts.xcatliu.com/

https://zhongsp.gitbooks.io/typescript-handbook/content/

```powershell
npm -g install typescript
tsc test.ts //编译成js文件
```

# 基础知识

### 数据类型

基本类型：number,string,boolean,Array<number>,number[],tupleType

动态类型：any,unknown

```typescript
let name:string = "caijt";
let message:string = `my name is ${name}`; //模版字符串
let age:number = 10; //
let isBoy:boolean = true;
let persons:string[] = ["caijt","chenxu"];
let persons:Array<string> = ["caijt","checnxu"]; //泛型写法，跟string[]是一样的
let x:[string,number] = ["caijt",31];
console.log(x[0]);

let unusable:void = undefined; //void类型的变量没有什么用，只能为它赋予undefined和null：
let u:undefined = undefined;
let n:null = null;
//undefined 和 null 是所有类型的子类型
let age:number = null;//不会报错

let anyType:any = "caijt"; //动态类型
anyType = true; //可以任意修改类型
anyType = 1; 
```

### 联合类型

```typescript
//传入的值可以为string 或 undefined
const sayHello = (name:string | undefined)=>{
    
}
type type1 = string | number | boolean;//定义一个类型别名，可以string
let unionType:type1 = false;
```

### 接口（对象类型）

```typescript
interface IPerson{
    name:string;
    readonly address:string;//只读属性
    age?:number; //可选属性
    [propName:string] :any; //任意属性
}
let person:IPerson = {
    name:"Caijt",
    address:"广东汕头",
    age:31,
    school:"鮀浦中学"
};
```

### 函数

```typescript
let sumFunc : (x:number,y:number)=>number = function sum(x:number,y:number):number{
    return x+y;
}
//用接口定义函数类型
interface ISumFunc {
    (x:number,y:number):number;
}
let sumFunc:ISumFunc = function sum(x:number,y:number):number{
    return x+y;
}
//可选参数，必须在最后
function createUser(name:string,age?:number):string{
	return `my name is ${name},age is ${age||0}`;
}

//函数重载
function add(x: number, y: number): number;
function add(left: string, right: string): string;
function add(left: number | string, right: string | number) :string|number{
    if (typeof left === "number") {
        return left as number + <number>right;
    } else {
        return left.toString() + right.toString();
    }
}
```

### Enum枚举

```typescript
enum Color{
    Red = 1,
    Green = "2",
    Blue = 3
}
let c:Color = Color.Red;
```

### 断言

```typescript
//断言不是类型转换，只是为了能使用断言后类型的方法跟通过typescript编译
let someValue : any = "this is string";
let stringLength : number = (<string>someValue).length;
let stringLength2: number = (someValue as string).length;//推荐使用这种
```



### 声明文件

```typescript
declare var //声明全局变量
declare const //推荐这样使用
declare var jQuery : (select:string) => any;
//声明文件以.d.ts为后缀

declare class  //声明全局类
declare enum	//声明全局枚举
declare namespace  //可以用来声明一个拥有多个子属性的全局变量。
declare namesapce jQuery{
    function ajax(url:string):void;
    namespace fn{
        function extend(object:any):void;
    }
}
interface //声明全局类型

//npm包声明文件
export //导出变量
export namespace //导出有子属性的对象
export default //默认导出
export = //commonjs导出模块
    
//foo.js
export const name:string;
//也可以这样写
declare const name:string;
export { name };
//这样使用
import {name,getName} from "foo";

//默认导出，只有 function、class 和 interface 可以直接默认导出，其他的变量需要先定义出来，再默认导出19：
export default function foo():string{}
import foo from "foo";
foo(); 
    
declare module

//声明文件这部分不是太明白
```



# 进阶

### 类型别名

```typescript
type nameType = string;//跟联合类型一样
```

### 字符串字面量类型

```typescript
type name = "a" | "b" | "c";
let name :nameType = "a";
```

### 元组

```typescript
let tupleType:[string,boolean] = ["caijt",true];
```

### 类

```typescript
class MyClass{
    public name:string;
    private age:number;
    protect age2:number;
    //存取器
    get name2(){
        return "Caij";
    }
    //存取器
    set name2(value){
        this.name2 = value;
    }
    //构造函数
    constructor(name:string,age:number,public school:string){
        
    };
    //静态方法
    static isPerson(a){
        return a instanceof MyClass;
    }
    say(){
        return "my name is "+this.name;
    }
}

var myClass = new MyClass("Caijt",30);
console.log(myClass.say());

class MyClass2 extends MyClass{
    name2:string;
    cconstructor(name:string,age:number,name2:string){
        super(name,age);
        this.name2 = name2;
    };
    say(){
        
    };
    superSay(){
        return super.say();
    }
}

abstract M{
    
}
```

### 接口

```typescript
interface IPerson{
    name:string;
    age:number;
    alert():void;
}
interface IPerson2:extends IPerson{
    
}
class Person implements IPerson{
    alert():void{
     	   
    }
}
//接口可以继承类
interface Person2 extends Person{
    
}
```

### 泛型

```typescript
interface GenericIdentityFn<T>{
    (arg:T):T;
}
//泛型约束
function testFunc<T extends IType>(arg:T):T{
    return arg;
}

//使用泛型定义函数
interface CreateArrayFunc{
    <T>(length:number,value:T):Array<T>;
}
let createArray:CreateArrayFunc;
createArray = function<T>(length:number,value:T):Array<T>{
    
};
createArray(3,"x");
//泛型类
class GenericNumber<T>{
    zeroValue:T;
    add:(x:T,y:T)=>T;
}
let myGenericNumber = new GenericNumber<number>();
myGenericNumbe.zeroValue = 0;
//泛型默认类型
function createArray<T = string>(length:number,value:T):Array<T>{
    
}
```

### 声明合并

```typescript
interface Alarm{
    price:number;
}
interface Alarm{
    weight:number;
}
```



### 泛型变量

```
T（Type）：表示一个TypeScript类型
K（Key）：表示对象中的键类型
V（Value）：表示对象中的值类型
E（Element）：表示元素类型
```

# 泛型工具类型

```
typeof 可以获取一个变量声明或对象类型
keyof 可以获取对象所有key值
in 遍历枚举
type keys = "a" | "b" |"c";
type Obj = {
	[p in keys]:any
} // ->{a:any,b:any:c:any}
infer 不懂
extends 泛型编带
function loggingIdentity<T extends IPerson>(arg:T):T{
	return arg;
}
```

# 

## 类型批注

```typescript
function Add(left:number,right:number):number{
    return left + right;
}
//无返回值
function test():void{
    
}
```



# 类型守护

```typescript
if("name" in p){
	//判断p里是否有name属性
}
typeof p ==="string"
p instanceof  MyClass //判断p是否是MyClass类或子类

//这个不懂
function isNumber(x:any): x is number {
    return typeof x === "number";
}
```

# 交叉类型

```typescript
interface IPerson{
    name:string;
    age:number;
}
interface IWorker{
    companyId:string;
}
type IStaff = IPerson & IWorker;
const staff : IStaff = {
  	id:"",
    age:30,
    companyId:"abc"
};
```

# Partial

Partial<T> 的作用就是将某个类型里的属性全部变为可选项 ?。

```typescript
type Partial<T> = {
  [P in keyof T]?: T[P];
};
```

# 装饰器

- 类装饰器（Class decorators）
- 属性装饰器（Property decorators）
- 方法装饰器（Method decorators）
- 参数装饰器（Parameter decorators）



```typescript
//类装饰器
function Greeter(target: Function): void {
  target.prototype.greet = function (): void {
    console.log("Hello Semlinker!");
  };
}

@Greeter
class Greeting{
    
}

//属性装饰器
function logProperty(target: any, key: string) {
}
//方法装饰器
function LogOutput(tarage: Function, key: string, descriptor: any) {
}
//参数装饰器
function Log(target: Function, key: string, parameterIndex: number) {
  let functionLogged = key || target.prototype.constructor.name;
  console.log(`The parameter in position ${parameterIndex} at ${functionLogged} has
 been decorated`);
}
```

### tsconfig.json

```json
{
    "compilerOptions": {
        "allowUnreachableCode": true, // 不报告执行不到的代码错误。
    	"allowUnusedLabels": false,	// 不报告未使用的标签错误
    	"alwaysStrict": false, // 以严格模式解析并为每个源文件生成 "use strict"语句
    	"baseUrl": ".", // 工作根目录
    	"experimentalDecorators": true, // 启用实验性的ES装饰器
    	"jsx": "react", // 在 .tsx文件里支持JSX
    	"sourceMap": true, // 是否生成map文件
    	"module": "commonjs", // 指定生成哪个模块系统代码
    	"noImplicitAny": false, // 是否默认禁用 any
    	"removeComments": true, // 是否移除注释
    	"types": [ //指定引入的类型声明文件，默认是自动引入所有声明文件，一旦指定该选项，则会禁用自动引入，改为只引入指定的类型声明文件，如果指定空数组[]则不引用任何文件
      		"node", // 引入 node 的类型声明
    	],
    	"paths": { // 指定模块的路径，和baseUrl有关联，和webpack中resolve.alias配置一样
            "src": [ //指定后可以在文件之直接 import * from 'src';
                "./src"
            ],
    	},
    	"target": "ESNext", // 编译的目标是什么版本的
    	"outDir": "./dist", // 输出目录
    	"declaration": true, // 是否自动创建类型声明文件
    	"declarationDir": "./lib", // 类型声明文件的输出目录
    	"allowJs": true, // 允许编译javascript文件。
    	"lib": [ // 编译过程中需要引入的库文件的列表
          "es5",
          "es2015",
          "es2016",
          "es2017",
          "es2018",
          "dom"
        ]
    },
    "files":[
        
    ],
    "include":[
        "src/**/*"
    ],
    "exclude":[
        "node_modules"
    ]
}

    //* 匹配0或多个字符（不包括目录分隔符）
    //? 匹配一个任意字符（不包括目录分隔符）
    //**/ 递归匹配任意子目录

```

