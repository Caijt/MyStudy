#### 增加远程仓库

```
git remote add origin git@github.com:Caijt/itsys-ui.git
```

#### 推送分支

```
git push origin 本地分支:远程分支
```

####   用户初始化

```
git config --global user.name "Caijt" //全局设置你的名字（必须设置，不然无法commit）

git config --global user.email "59001731@qq.com" //全局设置你的邮箱（必须设置，不然无法commit）

ssh-keygen -t rsa -C "59001731@qq.com" //创建SSH KEY
```

git init //将一个目录初始化为仓库

git add 123.txt //将文件123.txt增加到暂存区
git add . //将所有修改的文件增加到暂存区

git commit -m "提交的标题" //将暂存区的文件提交到分支
git commit -a -m "提交的标题"//相关于git add .后再commit）

git diff [文件名]//查看工作区跟暂存区的文件差异 
git diff HEAD //查看暂存区跟最新提交的所有文件差异

git rm //删除工作区的文件交提交到暂存区

git log //查看提交日志（加上参数 --pretty=oneline 缩小单行输出，--graph查看分支合并图）
git log -3 //查看最近3个日志

git reset --hard HEAD^ //分支提交回退到上一个版本并覆盖工作区及暂存区文件（HEAD代表当前版本，HEAD^代表上一个版本，HEAD^^代表上上一个版本，HEAD~100代表前100个版本）
git reset --hard commit_id //根据commit_id回退到对应版本
git reset --soft HEAD^ //分支提交回退到上一个版本，但工作区的文件不覆盖，回退的修改提交到暂存区
git reset HEAD^ //分支回退到上一个版本，暂存区清空，工作区文件不覆盖

git reflog //列出所有命令日志



git remote add origin git@github.com:Caijt/git1.git //在本地仓库添加远程仓库（github）的关联，其中远程仓库名origin可以自定义
git remote -v 查看远程仓库

git push origin <本地分支>:<远程分支> //将本地分支推送到远程分支上 
git push origin :master //删除远程分支，相同于推送一个本地空分支到远程分支上，等同于 git push origin --delete master

git clone git@github.com:Caijt/git2.git //远程克隆远程仓库到本地

git merge dev //将当前分支跟dev分支进行合并（默认使用快速合并方式，--no-ff参数不使用快速合并方式）

git branch dev //以当前分支克隆并创建分支dev
git branch -d dev //将分支dev删除（-D强制删除）
git branch -r //查看远程分支
git branch -vv //查看本地分支与远程分支的关联关系
git branch --set-upstream-to origin/dev 设置本地分支与远程分支的关联

git checkout -- 123.txt //撤销工作区123.txt文件的修改，恢复到提交暂存区之前的状态
git checkout dev //切换到dev分支
git checkout -b dev origin/dev  //创建远程分支dev到本地dev分支

git stash //将工作区跟暂存区的修改内容先保存起来
git stash list //查看暂存列表
git stash apply xxxx //恢复暂存列表
git stash drop //xxxx 删除暂存列表
git stash pop //将最近的暂存推出来并删除

git pull origin 远程分支:本地分支 //将远程分支文件拉取到本地

git tag v1.0 //创建标签 默认为HEAD指向的分支 
git tag v1.0 commit_id //为commit_id版本创建标签
git tag //查看所有标签
git tag -d v1.0 //删除本地仓库标签
git push origin :refs/tags/v1.0 //删除远程仓库标签
git push origin v1.0 //推送v1.0标签到远程仓库
git push origin --tags //推送所有标签到远程仓库