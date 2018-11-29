namespace ProBowlVotingBot

module Website =
    
    open System
    open PuppeteerSharp

    let mutable counter = 200

    let chromiumInstalled =
        BrowserFetcher().LocalRevisions()
        |> List.ofSeq
        |> List.isEmpty
        |> not

    let fetchChroium () =
        if chromiumInstalled = false then
            printfn "Downloading chromium..."
            
            let browserFetcher = BrowserFetcher()
            browserFetcher.DownloadProgressChanged.Add((fun args ->
                                                            if counter = 200 then
                                                                counter <- 0

                                                                Console.SetCursorPosition(0, Console.CursorTop)
                                                                Console.Write(args.ProgressPercentage.ToString() + "%")
                                                            else 
                                                                if args.ProgressPercentage = 100 then
                                                                    Console.SetCursorPosition(0, Console.CursorTop)
                                                                    Console.Write(args.ProgressPercentage.ToString() + "%")
                                                                    Console.WriteLine("")
                                                                else
                                                                    counter <- counter + 1
                                                      ))
            browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision)
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> ignore

            printfn "Installing Chromium..."
            printfn "Chromium downloaded..."
        else
            printfn "Chromium already installed!"

    let createBrowser (options: LaunchOptions) =
        Puppeteer.LaunchAsync(options)
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let newPage (browser: Browser) =
        browser.NewPageAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously
    
    let closePage (page: Page) =
        page.CloseAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let goTo url (page: Page) =
        page.GoToAsync(url)
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

        page

    let waitForNavigation (page: Page) =
        let nav = new NavigationOptions()
        nav.WaitUntil <- [|WaitUntilNavigation.DOMContentLoaded|]

        page.WaitForNavigationAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

        page

    let waitForSelector selector (page: Page) =
        page.WaitForSelectorAsync(selector)
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

        page

    let hover selector (page: Page) =
        page.HoverAsync(selector)
        |> Async.AwaitTask
        |> Async.RunSynchronously

        page

    let click selector (page: Page) =
        page.ClickAsync(selector)
        |> Async.AwaitTask
        |> Async.RunSynchronously

        page
    
    let evaluateExpression<'T> script (page: Page) =
        page.EvaluateExpressionAsync<'T>(script)
        |> Async.AwaitTask
        |> Async.RunSynchronously
