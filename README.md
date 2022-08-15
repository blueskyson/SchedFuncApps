# SchedFuncApp

以 Timer-Triggered 的 Azure Functions 作為 unix-style crontab 的排程器的概念驗證。

## 開發環境

- .NET 6.0 SDK
- Azure Functions Core Tools 4.x
- Azure CLI 2.4.0 以上
- Azurite

## 簡介

- [**DemoApp**](https://github.com/blueskyson/SchedFuncApps/tree/main/DemoApp/DemoApp.MvcApp): 用以展示 Timer-Triggered Function App 定期發送 Request 的紀錄。
- [**SubscriptionRenewScheduler**](https://github.com/blueskyson/SchedFuncApps/tree/main/FunctionApps/FunctionApps.SubscriptionRenewScheduler): 每 20 秒發送一次 HTTP POST 到 DemoApp。
- [**SubscriptionStatusScheduler**](https://github.com/blueskyson/SchedFuncApps/tree/main/FunctionApps/FunctionApps.SubscriptionStatusScheduler): 每 10 秒發送一次 HTTP GET 到 DemoApp。

DemoApp 可以在本機測試或布屬到 Heroku。

FunctionApps 可以在本機測試或布屬到 Azure Functions。
