# SubscriptionRenewScheduler

## 本機執行

啟動 Demo App

```
> cd DemoApp/DemoApp.MvcApp
> dotnet run
```

啟動 azurite:

```
> azurite -l azurite_workspace
```

新增本機的設定檔:

```
> cd FunctionApps/FunctionApps.SubscriptionStatusScheduler
> vim local.settings.json
```

加入以下內容:

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "DemoUrl": "https://localhost:7051/function/status"
    }
}
```

啟動 Function App:

```
> cd FunctionApps/FunctionApps.SubscriptionStatusScheduler
> func start
```
