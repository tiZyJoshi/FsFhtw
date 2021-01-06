module Parser

open System

let safeEquals (it : string) (theOther : string) =
    String.Equals(it, theOther, StringComparison.OrdinalIgnoreCase)

[<Literal>]
let HelpLabel = "Help"

let (|PokemonAction|Help|ParseFailed|) (input : string) =
    let tryParseInt (arg : string) valueConstructor =
        let (worked, arg') = Int32.TryParse arg
        if worked then valueConstructor arg' else ParseFailed

    let parts = input.Split(' ') |> List.ofArray
    match parts with
    
    //| [ verb ] when safeEquals verb (nameof Domain.Thundershock) -> Thundershock
    //| [ verb ] when safeEquals verb (nameof Domain.Elektroball) -> Elektroball
    | [ verb ] when safeEquals verb HelpLabel -> Help
    | [ arg ] -> tryParseInt arg (fun value -> PokemonAction)
    //| [ verb; arg ] when safeEquals verb (nameof Domain.IncrementBy) ->
    //    tryParseInt arg (fun value -> IncrementBy value)
    //| [ verb; arg ] when safeEquals verb (nameof Domain.DecrementBy) ->
    //    tryParseInt arg (fun value -> DecrementBy value)
    | _ -> ParseFailed
