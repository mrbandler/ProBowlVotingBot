// Learn more about F# at http://fsharp.org

open System
open ProBowlVotingBot

[<EntryPoint>]
let main argv =
    use mre = new System.Threading.ManualResetEventSlim(false)
    let result = Bot.run argv
    mre.Wait()

    result // Return success code.
