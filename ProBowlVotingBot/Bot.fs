namespace ProBowlVotingBot

module Bot =

    open System
    open System.Threading
    open System.Net
    open PuppeteerSharp
    open ProBowlVotingBot.Website

    // Position selector.
    // Array.from(document.querySelectorAll('a')).filter(a => a.title === 'Wide Receivers')[0].click()

    // Name selector.
    //Array.from(document.querySelectorAll('div')).filter(div => div.className === 'player-inner yui3-pro-bowl-ballot-player-opt-content')

    [<Literal>]
    let VoteButtonSelector = "#ballot-submit"

    let private checkArgs argv =
        if Array.contains "-p" argv         = false ||
           Array.contains "--position" argv = false then
           printfn "Please give a position for the player to vote for."
           
           false
        else
            if Array.contains "-n" argv     = false ||
               Array.contains "--name" argv = false then
               printfn "Please give the name for the player to vote for."

               false
            else
                true

    let rec private vote counter (browser: Browser) =
        printfn "Vote #%i" counter
        
        browser
        |> newPage
        |> goTo "http://www.nfl.com/probowl/ballot?optid=983040_15_1_22&team=SF&pos=ST"
        |> waitForSelector VoteButtonSelector
        |> hover VoteButtonSelector
        |> click VoteButtonSelector
        |> waitForNavigation 
        |> closePage
        
        vote (counter + 1) browser

    let private browserOptions =
        let options = new LaunchOptions()
        options.Headless <- false

        options

    let run (argv: string[]) =
        //match checkArgs argv with
        //    | false -> 1
        //    | true -> 
        fetchChroium ()
        createBrowser browserOptions
        |> vote 1

        0


