# EasyUIFramework

Unity版本 2017.4.5f1

这是一个简单的Unity UI 框架,功能持续完善中

支持功能:
    1.支持Resources/Assetbundle同步加载UI,先将资源放置在Resources目录或Assetbundles目录下(需运行Build Assetbundle来创建Assetbundle资源,默认放置于StreamingAssets目录下),在BaseUIDefine中注册UI,之后就可以调用BaseUIManager的方法进行加载了

    2.支持计时器,可通过TimeManager方便注册

#v0.0.2
优化了资源加载模块,完善了同步加载部分

#v0.0.1
目前提供基本的UI创建和事件系统;