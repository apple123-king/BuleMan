# BuleMan

BuleMan 是一个使用 Unity 制作的 2D 横版平台跳跃游戏。玩家需要控制角色通过关卡，收集樱桃，躲避机关陷阱，并到达终点进入下一关。

## 项目信息

- 引擎版本：Unity 2022.3.62f3c1
- 项目类型：2D 平台跳跃游戏
- 主要玩法：移动、跳跃、收集、闯关、生命值判定
- 当前关卡：Level1、Level2
- 结算场景：Victory、Defeat

## 运行方式

1. 使用 Unity Hub 打开本项目目录。
2. 推荐使用 Unity 2022.3.62f3c1 或兼容的 Unity 2022.3 LTS 版本。
3. 等待 Unity 自动导入资源并恢复 `Packages/manifest.json` 中的依赖。
4. 打开 `Assets/Scenes/Start.unity`。
5. 点击 Unity 编辑器中的 Play 按钮开始游戏。

## 操作说明

- 左右移动：`A` / `D` 或方向键左右
- 跳跃：`Space`
- 收集物：碰到樱桃后增加分数
- 陷阱：碰到带有 `Trap` 标签的机关会扣除生命并回到出生点
- 终点：到达检查点后进入下一关

## 场景结构

项目当前构建场景顺序如下：

1. `Start`：开始菜单
2. `Level1`：第一关
3. `Level2`：第二关
4. `Victory`：胜利界面
5. `Defeat`：失败界面

## 主要目录

```text
Assets/
  Animation/        动画片段和动画控制器
  Audios/           背景音乐和音效
  Background/       背景图片和相关资源
  Font/             字体资源
  Items/            樱桃、检查点、箱子等道具资源
  Main Characters/  角色素材
  Prefab/           可复用预制体
  Scenes/           Unity 场景文件
  Scripts/          游戏逻辑脚本
  Terrain/          地形瓦片和地图资源
  Traps/            尖刺、锯子、弹簧等陷阱资源
Packages/           Unity 包依赖配置
ProjectSettings/    Unity 项目设置
```

## 核心脚本

- `PlayerMove.cs`：玩家移动、跳跃和动画状态切换
- `HeroLife.cs`：玩家生命值、死亡和重生逻辑
- `ItemCollect.cs`：樱桃收集和分数显示
- `CheckPoint.cs`：关卡完成和加载下一关
- `GameManager.cs`：跨场景保存分数、生命值和当前关卡
- `StartMenu.cs`：开始游戏入口
- `EndMenu.cs`：胜利或失败后的重新开始逻辑
- `MoveIteams.cs`：移动平台或移动机关逻辑
- `Trampoline.cs`：弹簧交互逻辑

## 依赖包

项目依赖由 Unity 自动管理，主要包括：

- Unity 2D Feature
- TextMeshPro
- Unity UI
- Visual Scripting
- Unity Test Framework
- Visual Studio / Rider IDE 支持

## 版本控制说明

仓库已配置 `.gitignore`，会忽略 Unity 自动生成的缓存和本地配置，例如：

- `Library/`
- `Temp/`
- `Obj/`
- `Logs/`
- `.vs/`
- `UserSettings/`

提交代码时通常需要保留并提交：

- `Assets/`
- `Packages/`
- `ProjectSettings/`
- `.gitignore`
- `README.md`

## 后续可改进方向

- 增加更多关卡和难度变化
- 优化角色控制手感
- 增加暂停菜单和设置界面
- 增加音量控制、关卡选择和存档功能
- 补充自动化测试或基础玩法测试
