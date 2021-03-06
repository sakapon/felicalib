## felicalib 改

felicalib 改 (felicalib Remodeled) is a FeliCa access wrapper library for .NET, forked from [tmurakam/felicalib](https://github.com/tmurakam/felicalib).

### Setup
felicalib 改は、[NuGet Gallery](http://www.nuget.org/packages/FelicaLib.DotNet/) に登録されています。  
felicalib 改を利用するには、次の方法で NuGet パッケージをインストールします。

**方法 1.** Visual Studio の [パッケージ マネージャー コンソール] で、次のコマンドを実行します。

```
Install-Package FelicaLib.DotNet
```

**方法 2.** Visual Studio でプロジェクトを右クリックして、[Nuget パッケージの管理] で「felicalib Remodeled」を検索してインストールします。「felica」と入力すれば見つかります。

![VS-NuGet](Images/Preview/VS-NuGet.png)

### Usage
まず、FelicaLib 名前空間の using ディレクティブを追加します。

```c#
using FelicaLib;
```

Edy の残高を取得するなどの一部の特定の用途に対しては、[FelicaHelper クラス](https://github.com/sakapon/felicalib-remodeled/blob/master/FelicaLib_Remodeled/FelicaLib_DotNet/FelicaHelper.cs)にヘルパー メソッドが用意されています。

```c#
// Edy の残高
int balance = FelicaHelper.GetEdyBalance();
```

```c#
// Edy の利用履歴
foreach (var item in FelicaHelper.GetEdyHistory())
{
    Console.WriteLine("{0} 利用額: {1}, 残高: {2}", item.DateTime, item.Amount, item.Balance);
}
```

一般には、システム コード、サービス コードおよびアドレスを指定して、非暗号化領域のデータを取得します。
一部のシステム コードおよびサービス コードは、[FelicaHelper.cs](https://github.com/sakapon/felicalib-remodeled/blob/master/FelicaLib_Remodeled/FelicaLib_DotNet/FelicaHelper.cs) で定数として定義されています。

```c#
// Edy の残高
byte[] data = FelicaUtility.ReadWithoutEncryption(FelicaSystemCode.Edy, FelicaServiceCode.EdyBalance, 0);
int balance = new EdyBalanceItem(data).Balance;
```

```c#
// Edy ID
string edyId = FelicaUtility.ReadWithoutEncryption(FelicaSystemCode.Edy, 0x110B, 0)
    .Skip(2)
    .Take(8)
    .ToArray()
    .ToHexString();
```

```c#
// Suica の利用履歴
foreach (var data in FelicaUtility.ReadBlocksWithoutEncryption(FelicaSystemCode.Suica, FelicaServiceCode.SuicaHistory, 0, 20))
{
    var item = new SuicaHistoryItem(data);
    Console.WriteLine("{0:yyyy/MM/dd} 残高: {1}", item.DateTime, item.Balance);
}
```

### Testing environment
* [PaSoRi RC-S380](http://www.sony.co.jp/Products/felica/consumer/products/RC-S380.html)
* Windows 8
* .NET Framework 3.5, 4.5

### Release notes
* **v1.1.0** NuGet に登録しました (この時点でソースコードは変更していません)。
* **v1.1.6** アンマネージ リソースの扱いを改善し、安定性を向上させました。
* **v1.2.26** クラスを再設計し、安定性を向上させました。
* **v1.2.48** ユーティリティ メソッドを追加するなど、API を改善しました。
* **v1.2.67** 複数のブロックを連続的に読み込むためのメソッドを追加しました。

### Future plans
* **v1.3** 一定間隔の自動ポーリング。
* **v1.4** WebSocket サーバーによるホスト。
