namespace ProBowlVotingBot

module Bot =

    open PuppeteerSharp
    open ProBowlVotingBot.Website
    open ProBowlVotingBot.Args

    // Position selector.
    // Array.from(document.querySelectorAll('a')).filter(a => a.title === 'Wide Receivers')[0].click()

    // Name selector.
    //Array.from(document.querySelectorAll('div')).filter(div => div.className === 'player-inner yui3-pro-bowl-ballot-player-opt-content')

    [<Literal>]
    let VoteButtonSelector = "#ballot-submit"

    [<Literal>]
    let NameSelectorScript = "Array.from(document.querySelectorAll('div')).filter(div => div.className === 'player-inner yui3-pro-bowl-ballot-player-opt-content').filter(div => div.innerHTML.toLowerCase().includes('{0}') && div.innerHTML.toLowerCase().includes('{1}')).map(div => div.id)[0]"

    let private createScript (script: string) (name: string) =
        let lowerName = name.ToLower()
        let split = lowerName.Split [|' '|]

        let result = script.Replace("{0}", Array.item 0 split)
        result.Replace("{1}", Array.item 1 split)

    let rec private voteShow positionSelector name counter (browser: Browser) =
        let page = 
            browser
            |> newPage
            |> goTo "http://www.nfl.com/probowl/ballot"
            |> waitForSelector positionSelector
            |> hover positionSelector
            |> click positionSelector
        

        let script = createScript NameSelectorScript name
        let nameSelector = "#" + (page |> evaluateExpression<string> script)
        
        page
        |> hover nameSelector
        |> click nameSelector
        |> ignore

        (Async.Sleep 2000) |> Async.RunSynchronously

        page
        |> hover VoteButtonSelector
        |> click VoteButtonSelector
        |> waitForNavigation
        |> closePage

        printfn "Vote #%i casted!" (counter + 1)
        voteShow positionSelector name (counter + 1) browser
    
    let rec vote positionSelector name counter (browser: Browser) =
        let page = 
            browser
            |> newPage
            |> goTo "http://www.nfl.com/probowl/ballot"
            |> waitForSelector positionSelector
            |> click positionSelector
        

        let script = createScript NameSelectorScript name
        let nameSelector = "#" + (page |> evaluateExpression<string> script)
        
        page
        |> waitForSelector nameSelector
        |> click nameSelector
        |> waitForSelector VoteButtonSelector
        |> click VoteButtonSelector
        |> closePage
        
        printfn "Vote #%i casted!" (counter + 1)
        vote positionSelector name (counter + 1) browser

    let private browserOptions show =
        let options = new LaunchOptions()
        match show with
            | true ->
                options.Headless <- false
                options.DefaultViewport.IsMobile <- true
                options.DefaultViewport.IsLandscape <- true
                options
            | false -> 
                options.Headless <- true
                options

    let startBot (positionSelector: string) (name: string) (show: bool) =
        fetchChroium ()

        printfn ""
        printfn "Starting vote process..."
        printfn "Votes will be cast for %s" (name.ToLower())
        match show with
            | false -> createBrowser (browserOptions show) |> vote positionSelector name 0
            | true -> createBrowser (browserOptions show) |> voteShow positionSelector name 0

    let run (argv: string[]) =
        let positionSelector, name, show = evaluateArgs argv
        match positionSelector, name with   
            | None, None -> 0
            | None, Some _ -> printfn "The given position is not valid! Please try again..."; 1
            | Some p, Some n -> startBot p n show; 0



