# SchedFuncApp

以 Timer-Triggered 的 Azure Functions 作為 unix-style crontab 的排程器的概念驗證。

## 開發環境

- .NET 6.0 SDK
- Azure Functions Core Tools 4.x
- Azure CLI 2.4.0 以上
- Azurite

## 簡介

DemoApp 可以在本機測試或布屬到 Heroku。

FunctionApps 可以在本機測試或布屬到 Azure Functions。

### [**DemoApp**](https://github.com/blueskyson/SchedFuncApps/tree/main/DemoApp/DemoApp.MvcApp)

用以展示 Timer-Triggered Function App 定期發送 Request 的紀錄。其中 `FunctionController` 的 `Renew` 負責接收 `SubscriptionRenewScheduler` 的請求、`Status` 負責接收 `SubscriptionStatusScheduler` 的請求。

`HomeController` 的 `Index` 展示請求的紀錄、`Reset` 清空紀錄。

### [**SubscriptionRenewScheduler**](https://github.com/blueskyson/SchedFuncApps/tree/main/FunctionApps/FunctionApps.SubscriptionRenewScheduler)

每 20 秒發送一次 HTTP POST 到 DemoApp。

我在 `Startup` 用依賴注入註冊 appsetting.json，如此一來在應用程式中就能讀取 appsetting.json 中的字串當作設定值，其好處是不需要在 Azure 環境裡手動設定環境變數，但是 `TimerTriggerAttribute` 的 **cron-style 字串不能透過此方式**來設定，否則會在 Azure 環境 Runtime Error。

再者，[azure-functions-core-tools #122](https://github.com/Azure/azure-functions-core-tools/issues/122) 表明了 local.setting.json 已經被視作 Function App 的 appsetting.json，所以若非必要，我不推薦自己額外弄一個 appsetting.json。

### [**SubscriptionStatusScheduler**](https://github.com/blueskyson/SchedFuncApps/tree/main/FunctionApps/FunctionApps.SubscriptionStatusScheduler)

每 10 秒發送一次 HTTP GET 到 DemoApp。

在這個 Scheduler 中，我單純透過 `Environment.GetEnvironmentVariable` 來讀取 local.settings.json 所設定的環境變數，就不額外新增一個 appsetting.json 了。

注意到 SubscriptionStatusScheduler.cs 使用 `TimerTriggerAttribute` 的地方:

```csharp
[TimerTrigger("%Schedule%")] TimerInfo myTimer
```

`"%Schedule%"` 在 Function App 啟動時會把 local.setting.json 的 `"Schedule": "*/10 * * * * *"` 作為排程器的設定值。

當佈署到 Azure 上時，需要手動設定環境變數，從 `Home > SubscriptionStatusScheduler`，在左側找到 `Settings > Configuration`，選擇 `+ New Application Setting`，填入以下鍵值對:

|Name|Value|
|----|-----|
|Schedule|*/10 * * * * *|
|DemoUrl|https://jacklin-scheduler.herokuapp.com/function/status|

雖然手動設定環境變數有點麻煩，但是未來想要更改設定值時，只需要在 Azure 上改環境變數然後儲存、重新佈署即可。
