namespace ProBowlVotingBot

module Args =
    
    type Position =
        | Nope = 0
        | QB = 1
        | RB = 2
        | WR = 3
        | FB = 4
        | TE = 5
        | T = 6
        | G = 7
        | C = 8
        | DE = 9
        | DT = 10
        | ILB = 11
        | OLB = 12
        | CB = 13
        | SS = 14
        | FS = 15
        | K = 16
        | RS = 17
        | P = 18
        | ST = 19

    [<Literal>]
    let PositionFlag = "--position"

    [<Literal>]
    let NameFlag = "--name"

    [<Literal>]
    let ShowFlag = "--show"

    [<Literal>]
    let ButtonSelectorPrefix = "#button-"

    let private positionSelector position =
        match position with
            | Position.Nope -> None
            | Position.QB -> Some (ButtonSelectorPrefix + "QB")
            | Position.RB -> Some (ButtonSelectorPrefix + "RB")
            | Position.WR -> Some (ButtonSelectorPrefix + "WR")
            | Position.FB -> Some (ButtonSelectorPrefix + "FB")
            | Position.TE -> Some (ButtonSelectorPrefix + "TE")
            | Position.T -> Some (ButtonSelectorPrefix + "T")
            | Position.G -> Some (ButtonSelectorPrefix + "G")
            | Position.C -> Some (ButtonSelectorPrefix + "C")
            | Position.DE -> Some (ButtonSelectorPrefix + "DE")
            | Position.DT -> Some (ButtonSelectorPrefix + "DT")
            | Position.ILB -> Some (ButtonSelectorPrefix + "ILB")
            | Position.OLB -> Some (ButtonSelectorPrefix + "OLB")
            | Position.CB -> Some (ButtonSelectorPrefix + "CB")
            | Position.SS -> Some (ButtonSelectorPrefix + "SS")
            | Position.FS -> Some (ButtonSelectorPrefix + "FS")
            | Position.K -> Some (ButtonSelectorPrefix + "K")
            | Position.RS -> Some (ButtonSelectorPrefix + "RS")
            | Position.P -> Some (ButtonSelectorPrefix + "P")
            | Position.ST -> Some (ButtonSelectorPrefix + "ST")
            | _ -> None

    let private convertPositionFlagValue value =
        match value with
            | "QB" -> Position.QB
            | "RB" -> Position.RB
            | "WR" -> Position.WR
            | "FB" -> Position.FB
            | "TE" -> Position.TE
            | "T" -> Position.T 
            | "G" -> Position.G
            | "C" -> Position.C
            | "DE" -> Position.DE
            | "DT" -> Position.DT
            | "ILB" -> Position.ILB
            | "OLB" -> Position.OLB
            | "CB" -> Position.CB
            | "SS" -> Position.SS
            | "FS" -> Position.FS
            | "K" -> Position.K
            | "RS" -> Position.RS
            | "P" -> Position.P
            | "ST" -> Position.ST
            | _ -> Position.Nope

    let private checkArgs argv =
        if Array.contains PositionFlag argv = false then
            printfn "Please give a position for the player to vote for."
           
            false
        else
            if Array.contains NameFlag argv = false then
               printfn "Please give the name for the player to vote for."

               false
            else
                true

    let private evaluateFlagValue argv flag =
        let result =
            argv 
            |> Array.mapi (fun i v -> if v = flag then (Array.item (i+1) argv) else "")
            |> Array.filter (fun v -> v <> "")

        if Array.isEmpty result = false then
            Array.item 0 result
        else
            ""

    let evaluateArgs (argv: string[]): string option * string option * bool = 
        match checkArgs argv with
            | false -> None, None, false
            | true ->
               
                let positionSelector =
                    evaluateFlagValue argv PositionFlag
                    |> convertPositionFlagValue
                    |> positionSelector

                let name = Some (evaluateFlagValue argv NameFlag)
                
                let show = evaluateFlagValue argv ShowFlag
                if show <> "" then
                    if show = bool.FalseString.ToLower() then
                        positionSelector, name, false
                    else
                        positionSelector, name, true
                else 
                    positionSelector, name, true