# RandomAudioPlayer
## 这是什么？
这是一个用于Unity2D平台游戏的音频解决方案。它的主要用途是将音频的播放随机化，并实现接触不同的瓦片表面播放不同声音的效果。可以利用本工具库实现诸如人物在不同环境行走，发出不同的脚步声音；子弹打击到不同的墙壁或物体发出不同的打击音效。并支持在Inspector面板中快速编辑。

![图片](https://user-images.githubusercontent.com/41114110/133920616-0a6b4a36-891c-4a75-bd67-f392356af35d.png)

## 示例项目说明
1. RandomAudioPlayer类是本库的核心类。此类的公共API：PlayRandomSound，接收TileBase类型的瓦片，方法体内根据传来的不同的瓦片播放对应的声音。
2. PhysicsHelper类是一个帮助类。用于查找目标瓦片（主角踩踏的瓦片，子弹打击的瓦片等）。
3. PlayerInput类为主角的Mono类。在脚落地时用帮助类查找脚下的瓦片，并根据此瓦片播放对应的音效。

## 使用方法
RandomAudioPlayer类是本库的核心类。如果想使用，只需将此类移到您的项目中即可。

## 相关引用
* Brackeys/[2D-Character-Controller](https://github.com/Brackeys/2D-Character-Controller)
* Unity-Technologies/2DKit
