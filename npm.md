### 更新npm 

npm install -g npm

### 查看NPM版本

npm -v

### 安装包

npm install 包名 -
npm install 包名@2.1 安装指定版本的包
npm install --registry=https://registry.npm.taobao.org 用淘宝镜像安装依赖包

npm install -g cnpm --registry=https://registry.npm.taobao.org

npm install xxx --save
npm install xxx --save-dev

### 更新包并记录到开发依赖中

npm update 包名 -D

### 更新包并记录到生产依赖中

npm update 包名 -S

### 删除依赖包

npm remove 包名

### 列出安装的所有包

npm list
npm list -g --depth 0

### 查看当前包的安装路径

npm root

### 查看全局的包的安装路径

npm root -g  

vue-cli2.x安装方法
npm install -g vue-cli

### 用vue-cli创建一个webpack项目

vue init webpack 项目名
vue init webpack-simple 项目名

### vue-cli3.0安装方法

npm install -g @vue/cli
vue create hello-world//创建项目

### 项目初始化

npm install

### 项目调试

npm run dev

### 查看本地模块

npm list 包名

### 查看远程模块版本

npm view <包名> version

### 版本号符号的意义

~ 会匹配最近的小版本依赖包，比如~1.2.3会匹配所有1.2.x版本，但是不包括1.3.0
^ 会匹配最新的大版本依赖包，比如^1.2.3会匹配所有1.x.x的包，包括1.3.0，但是不包括2.0.0

*这意味着安装最新版本的依赖包