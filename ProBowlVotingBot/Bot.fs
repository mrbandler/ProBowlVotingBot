namespace ProBowlVotingBot

module Bot =

    open System
    open System.Threading
    open System.Net
    open PuppeteerSharp
    open ProBowlVotingBot.Website

    // Position selector.
    // Array.from(document.querySelectorAll('a')).filter(a => a.title === 'Wide Receivers')[0].click()

    [<Literal>]
    let VoteButtonSelector = "#ballot-submit"

    let checkArgs argv =
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

    let rec vote counter (browser: Browser) =
        printfn "Vote #%i" counter
        
        browser
        |> newPage
        |> goTo "http://www.nfl.com/probowl/ballot"
        |> waitForSelector VoteButtonSelector
        |> hover VoteButtonSelector
        |> click VoteButtonSelector
        |> waitForNavigation 
        |> closePage
        
        vote (counter + 1) browser

    let run (argv: string[]) =
        if checkArgs argv = false then  
            1
        else        
            let options = new LaunchOptions()
            options.Headless <- false
        
            fetchChroium ()
            createBrowser options
            |> vote 1

            0

