namespace ProBowlVotingBot

module Website =
    
    open System
    open PuppeteerSharp

    let mutable counter = 200

    /// Checks if a local copy if Chromium is already installed.
    let chromiumInstalled =
        BrowserFetcher().LocalRevisions()
        |> List.ofSeq
        |> List.isEmpty
        |> not

    /// Fetches/Downloads the latest version of Chromium to run the bot.
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

    /// Creates a browser with given options.
    let createBrowser (options: LaunchOptions) =
        Puppeteer.LaunchAsync(options)
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Creates a new page with the a given browser.
    let newPage (browser: Browser) =
        browser.NewPageAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously
    
    /// Closes a given page.
    let closePage (page: Page) =
        page.CloseAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Navigates a given page to a given URL.
    let goTo url (page: Page) =
        page.GoToAsync(url)
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

        page

    /// Waits for the navigation with a given page.
    let waitForNavigation (page: Page) =
        let nav = new NavigationOptions()
        nav.WaitUntil <- [|WaitUntilNavigation.DOMContentLoaded|]

        page.WaitForNavigationAsync(nav)
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

        page

    /// Waits until a given selector is present in a given page.
    let waitForSelector selector (page: Page) =
        page.WaitForSelectorAsync(selector)
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

        page
    
    /// Hovers the cursor over a element with the given selector on the given page.
    let hover selector (page: Page) =
        page.HoverAsync(selector)
        |> Async.AwaitTask
        |> Async.RunSynchronously

        page

    /// Click on a element with the given selector on the given page.
    let click selector (page: Page) =
        page.ClickAsync(selector)
        |> Async.AwaitTask
        |> Async.RunSynchronously

        page
    
    /// Evaluates a given JS script on the given page.
    let evaluateExpression<'T> script (page: Page) =
        page.EvaluateExpressionAsync<'T>(script)
        |> Async.AwaitTask
        |> Async.RunSynchronously
