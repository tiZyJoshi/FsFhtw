module Domain

open System

type Player = Player1 | Player2

type PokemonName = Pikachu | Mauzi

type PokemonAction = {
    name : string
    damage : int
}

type Pokemon = {
    name : PokemonName
    hp : int
    actions : PokemonAction list
}

type ActionResult =
    | Player1ToMove
    | Player2ToMove
    | GameWon of Player

type GameState = {
    player1Pokemon : Pokemon
    player2Pokemon : Pokemon
}

type Message = PokemonAction of int

let private isGameWonBy player gameState =
    match player with
    | Player1 -> gameState.player2Pokemon.hp < 1
    | Player2 -> gameState.player1Pokemon.hp < 1

let rec printPokemonActions (actions : PokemonAction list) index =
    match actions with
    | [] -> ()
    | action::actions -> 
        printfn "Action (%A): %A" index action.name
        printfn "Damage: %A" action.damage
        printPokemonActions actions (index + 1)
    
let printPlayerActions gameState player =
    match player with
    | Player1 ->
        printfn "You have a %A" gameState.player1Pokemon.name
        printfn "It has %A HP" gameState.player1Pokemon.hp
        printPokemonActions gameState.player1Pokemon.actions 0
    | Player2 ->
        printfn "You have a %A" gameState.player2Pokemon.name
        printfn "It has %A HP" gameState.player2Pokemon.hp
        printPokemonActions gameState.player2Pokemon.actions 0

let player1Moved gameState action =
    try
        //check if action exists
        match gameState.player1Pokemon.actions.[action] with
        | _ -> 
            let newGameState = {
                    player1Pokemon = gameState.player1Pokemon
                    player2Pokemon = {
                    name = gameState.player2Pokemon.name
                    hp = gameState.player2Pokemon.hp - gameState.player1Pokemon.actions.[action].damage
                    actions = gameState.player2Pokemon.actions
            }}

            if newGameState |> isGameWonBy Player1 then
                printfn "GAME WON by %A" Player1
                newGameState, GameWon Player1
            else
                printPlayerActions newGameState Player2
                newGameState, Player2ToMove
    with
    | e -> printfn "Not a valid action."
           printPlayerActions gameState Player1
           gameState, Player1ToMove


let player2Moved gameState action =
    try
        //check if action exists
        match gameState.player1Pokemon.actions.[action] with
        | _ -> 
            let newGameState = {
                player1Pokemon = {
                    name = gameState.player1Pokemon.name
                    hp = gameState.player1Pokemon.hp - gameState.player2Pokemon.actions.[action].damage
                    actions = gameState.player1Pokemon.actions
                }
                player2Pokemon = gameState.player2Pokemon
            }

            if newGameState |> isGameWonBy Player2 then
                printfn "GAME WON by %A" Player2
                newGameState, GameWon Player2
            else 
                printPlayerActions newGameState Player1
                newGameState, Player1ToMove
    with
    | e -> printfn "Not a valid action."
           printPlayerActions gameState Player2
           gameState, Player2ToMove

let init () = 
    let gameState = {
        player1Pokemon = {
            name = Pikachu
            hp = 100
            actions = [
                {
                name = "Tackle"
                damage = 10
                }
                {
                name = "Thunder Wave"
                damage = 30
                }
                {
                name = "Thunderbolt"
                damage = 70
                }
            ]
        }
        player2Pokemon = {
            name = Mauzi
            hp = 100
            actions = [
                {
                name = "Tackle"
                damage = 10
                }
            ]
        }
    }
    printPlayerActions gameState Player1
    printfn ""
    printfn ""
    gameState, Player1ToMove

let update (msg : Message) model =
    let (gameState, actionResult) = model
    let action =
        match msg with
        | PokemonAction x -> x
    match actionResult with
    | Player1ToMove ->
        //printPlayerActions gameState Player1
        player1Moved gameState action
    | Player2ToMove ->
        //printPlayerActions gameState Player2
        player2Moved gameState action
    | GameWon player ->
        gameState, GameWon player
