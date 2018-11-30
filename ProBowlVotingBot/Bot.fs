namespace ProBowlVotingBot

module Bot =

    open PuppeteerSharp
    open ProBowlVotingBot.Website
    open ProBowlVotingBot.Args

    /// Root webpage of the voting process.
    [<Literal>]
    let VotingWebsite = "http://www.nfl.com/probowl/ballot"

    /// Vote button selector.
    [<Literal>]
    let VoteButtonSelector = "#ballot-submit"

    /// JS Script to retrive the player name selector.
    [<Literal>]
    let NameSelectorScript = "Array.from(document.querySelectorAll('div')).filter(div => div.className === 'player-inner yui3-pro-bowl-ballot-player-opt-content').filter(div => div.innerHTML.toLowerCase().includes('{0}') && div.innerHTML.toLowerCase().includes('{1}')).map(div => div.id)[0]"

    /// Creates the JS script with the given name over the CLI.
    let private createScript (script: string) (name: string) =
        let lowerName = name.ToLower()
        let split = lowerName.Split [|' '|]

        let result = script.Replace("{0}", Array.item 0 split)
        result.Replace("{1}", Array.item 1 split)
       
    /// Recursive vote that shows the browser to the user.
    let rec private voteShow positionSelector name counter (browser: Browser) =
        let page = 
            browser
            |> newPage
            |> goTo VotingWebsite
            |> waitForSelector positionSelector
            |> hover positionSelector
            |> click positionSelector
        
        let script = createScript NameSelectorScript name
        let nameSelector = "#" + (page |> evaluateExpression<string> script)
        
        page
        |> hover nameSelector
        |> click nameSelector
        |> ignore

        (Async.Sleep 1000) |> Async.RunSynchronously

        page
        |> hover VoteButtonSelector
        |> click VoteButtonSelector
        |> ignore

        printfn "Vote #%i casted!" (counter + 1)

        page
        |> waitForNavigation
        |> closePage

        voteShow positionSelector name (counter + 1) browser
    
    /// Recursive vote that hides the browser from the user.
    let rec vote positionSelector name counter (browser: Browser) =
        let page = 
            browser
            |> newPage
            |> goTo VotingWebsite   
            |> waitForSelector positionSelector
            |> click positionSelector
        
        let script = createScript NameSelectorScript name
        let nameSelector = "#" + (page |> evaluateExpression<string> script)
        
        page
        |> waitForSelector nameSelector
        |> click nameSelector
        |> waitForSelector VoteButtonSelector
        |> click VoteButtonSelector
        |> ignore

        printfn "Vote #%i casted!" (counter + 1)

        page
        |> closePage
        
        vote positionSelector name (counter + 1) browser
       
    /// Creates browser options based on passed CLI flags.
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
    
    /// Strts the bot with the given CLI arguments.
    let startBot (positionSelector: string) (name: string) (show: bool) =
        fetchChroium ()

        printfn ""
        printfn "Starting vote process..."
        printfn "Votes will be cast for %s" (name.ToLower())
        match show with
            | false -> createBrowser (browserOptions show) |> vote positionSelector name 0
            | true -> createBrowser (browserOptions show) |> voteShow positionSelector name 0

    /// Runs the bot with given CLI arguments.
    let run (argv: string[]) =
        let positionSelector, name, show = evaluateArgs argv
        match positionSelector, name with   
            | None, None -> 0
            | None, Some _ -> printfn "The given position is not valid! Please try again..."; 1
            | Some p, Some n -> startBot p n show; 0
            | _, None -> 1



