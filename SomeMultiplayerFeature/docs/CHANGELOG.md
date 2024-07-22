# 0.17.0

- 添加一键修改所有玩家额度的游戏内UI

# 0.16.1

- 修复花钱限制处理不正确的问题
    - 修复当商品不需要金币时，依然计算当日消费的问题
    - 修复当手持物品时，购买物品未成功依然计算当日消费的问题
    - 修复当商品既需要有物品有需要金币时，物品不够时依然计算当日消费的问题
    - 修复当总金钱不足时，购买物品依然计算当日消费的问题
- 修复购物信息显示不正确的问题

# 0.16.0

- 移除`change_limit`、`set_limit`和`reload_limit`命令
- 修复玩家未成功购买物品依然显示购物信息的问题
- 添加花钱限制管理菜单功能，主机可以按下快捷键（默认是N）打开游戏内UI来设置玩家的花钱额度
- 添加额度信息显示功能，客机可以按下快捷键（默认是N）查看额度信息
- 添加旧版本数据清除功能，载入存档时会清楚旧版本添加的无用数据

# 0.15.4

- 修改炸弹配方
    - 修改樱桃炸弹的配方为 2树液 + 1铜矿
    - 修改炸弹的配方为 4树液 + 3铁矿
    - 修改超级炸弹的配方为 8树液 + 4金矿
    - 修改爆炸弹丸的配方为 10树液 + 10铱矿 = 5爆炸弹丸

# 0.15.3

- 修复当玩家不满足版本要求时，玩家提前退出，聊天框信息显示错误的问题

# 0.15.2

- 修改浇水获得的经验为5点
- 修改树木生长的概率为50%、掉落种子的概率为0%

# 0.15.1

- 修复购物限制功能关闭后，有玩家连接或者输入命令时，购物限制功能又生效的问题

# 0.15.0

- 修复动物睡觉后可以无限刷经验的问题
- 修复`矿井即时刷新`功能客机端无效的问题
- 修改采矿经验
    - 移除精通后经验减半
    - 修改采集铜矿、铁矿、金矿、铱矿获得的经验分别为11、12、13、14点
- 修改钓鱼经验
    - 移除精通后钓鱼经验翻倍
    - 修改钓鱼经验为原来的2倍
- 修改砍树获得的经验为30点、砍树桩获得的经验为15点
- 修改炸弹配方
    - 修改樱桃炸弹的配方为 5树液 + 2石头
    - 修改炸弹的配方为 10树液 + 5石头
    - 修改超级炸弹的配方为 20树液 + 10石头
    - 修改爆炸弹丸的配方为 10树液 + 10石头 = 5爆炸弹丸
- 移除商店中的炸弹、超级炸弹和爆炸弹丸
- 为部分功能添加SMAPI控制台提示信息

# 0.14.2

- 修复商店兑换不了物品的问题

# 0.14.1

- 修复`版本限制`功能验证的版本号未更新的问题

# 0.14.0

- 修改收获煤炭窑获得的经验为采集经验
- 修改`版本限制`功能的提示信息，使主机端的信息更详细，移除客机端的消息，以尝试修复空玩家的问题
- 移除`冻结金钱`功能
- 移除`小屋花费`功能，将其移入`BetterCabin`模组中
- 添加`显示购物信息`功能
- 添加`购物限制`功能，替代原来的`冻结金钱`功能

# 0.13.0

- 移除测试代码
- 修改小屋的建造花费为0金、浇水获得的经验为3点
- 为初始种子包添加三种树种各10个
- 当玩家精通后，采集铜矿、铁矿、金矿和铱矿获得的经验减半
- 添加`禁止购买背包`、`禁止房屋升级`功能

# 0.12.0

- 添矿井即时刷新功能，当玩家出矿井时，立即刷新所有没有人的矿井层

# 0.11.2

- 修复当金钱接近设置的阈值时，依然能一次购买大量物品的问题

# 0.11.1

- 添加`打开配置菜单`功能

# 0.11.0

- 移除`访问商店信息`功能、`显示延迟玩家`功能
- 修复在载入存档时，`自动设置Ip连接`功能不会打开Ip连接
- 修复某些设置需要重启游戏才会生效的问题
- 现在`版本限制`功能在踢出玩家后，会在游戏内聊天框显示一条消息
- 现在客机版本验证不通过的消息会显示在聊天框，而不是SMAPI控制台
- 添加`收获回收机获得4点钓鱼经验`、`收获种子生成器获得4点耕种经验`、`收获树液采集器获得4点采集经验`、`收获煤炭窑获得4点采矿经验功能`、`收获熏鱼机获得4点钓鱼经验`功能
- 添加`冻结金钱`功能

# 0.10.0

- 现在耕地上面要有作物浇水才会获得经验
- 修改铜矿获得的经验为14点，铁矿获得的经验为16点，铱矿获得的经验为20点
- 重写`版本限制`功能，略微改善其性能
- 添加`禁止取消后摇`功能
- 添加`精通后钓鱼经验翻倍`功能

# 0.9.0

- 添加`浇水获得4点耕种经验`、`收获小桶获得20点耕种经验`、`收获熔炉获得7点采矿经验`、`收获重型熔炉获得35点采矿经验`功能
- 修改抚摸动物获得的经验值为50点、砍树获得的经验为20点、砍树桩获得的经验为10点

# 0.8.1

- 修改`自动点击`功能的延迟时间为20s

# 0.8.0

- 移除`模组限制`功能

# 0.7.1

- 修改`list`命令，现在会显示地点的显示名称和Ip地址
- 修改`ping`命令，现在会显示Ip地址

# 0.7.0

- 现在`模组限制`功能和`版本限制`功能的消息提示会显示在客户端
- 添加显示间隔选项，可以设置显示延迟玩家的间隔时间
- 添加`自动设置Ip连接`功能
- 添加`自动点击`功能，可以为不结算的玩家自动点击OK按钮

# 0.6.0

- 添加`kick`和`kickall`命令
- 将`版本限制`功能从`模组限制`功能中移出，并修改其实现方式
- 修改`list`命令，现在`list`命令可以查看离线玩家

# 0.5.1

- 修复`模组限制`功能中的`版本限制`功能空引用的问题

# 0.5.0

- 修复客户端玩家在自定义角色时，`ban`命令无效的问题
- 添加`ping`命令
- 添加`list`命令，查看在线的玩家和其所在的位置
- 添加`inventory`命令，查看某个玩家背包中的物品
- 更新`模组限制`功能，现在会要求该模组版本必须是最新版本

# 0.4.0

- 添加`显示提示`功能，该功能可以显示QQ群号
- 添加`ban`和`unban`命令
- 重写`踢出未准备玩家`功能
- 重写`踢出延迟玩家`功能
    - 当某个玩家加入游戏后，若超过一半的玩家延迟超过100ms，则发送信息
    - 每5秒显示延迟超过100ms的玩家中延迟最高的玩家

# 0.3.0

- 添加`显示不满足要求的模组`功能
- 添加`踢出延迟玩家`功能
- 添加`显示玩家数量`功能
- 添加`踢出未准备玩家`功能

# 0.2.0

- 添加`限制模组`功能
- 移除`查看模组信息`功能
- 移除`强制结束等待`功能

# 0.1.0

- 添加`访问商店信息`功能
- 添加`查看模组信息`功能
- 添加`强制结束等待`功能