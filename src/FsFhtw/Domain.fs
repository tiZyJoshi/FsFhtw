module Domain

type Player = Player1 | Player2

type PokemonName = Pikachu | Mauzi

type PokemonAction = {
    name : string
    damage : int
    ep : int
}

type Pokemon = {
    name : PokemonName
    hp : int
    ep : int
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
        let epSign = if action.ep > 0 then $"-{action.ep}" else $"+{action.ep * -1}"
        printfn $"   {index}: {action.name} (Damage: {action.damage}, EP: {epSign})"
        printPokemonActions actions (index + 1)

let printPlayerActions gameState player =
    printfn ""
    match player with
    | Player1 ->
        printfn $"Player1's Turn:\n"
        printfn $"You have a {gameState.player1Pokemon.name}"
        printfn $"It has {gameState.player1Pokemon.hp} HP and {gameState.player1Pokemon.ep} EP"
        printfn $"Please choose your action!\n"
        printPokemonActions gameState.player1Pokemon.actions 0
    | Player2 ->
        printfn "Player2's Turn:\n"
        printfn $"You have a {gameState.player2Pokemon.name}"
        printfn $"It has {gameState.player2Pokemon.hp} HP and {gameState.player2Pokemon.ep} EP"
        printfn $"Please choose your action!\n"
        printPokemonActions gameState.player2Pokemon.actions 0

let init () = 
    let gameState = {
        player1Pokemon = {
            name = Pikachu
            hp = 100
            ep = 50
            actions = [
                {
                    name = "Tackle"
                    damage = 10
                    ep = 10
                }
                {
                    name = "Thunder Wave"
                    damage = 30
                    ep = 30
                }
                {
                    name = "Thunderbolt"
                    damage = 70
                    ep = 70
                }
                {
                    name = "Rest"
                    damage = 0
                    ep = -30
                }
            ]
        }
        player2Pokemon = {
            name = Mauzi
            hp = 100
            ep = 60
            actions = [
                {
                    name = "Tackle"
                    damage = 10
                    ep = 10
                }
                {
                    name = "Fake out"
                    damage = 30
                    ep = 30
                }
                {
                    name = "Bite"
                    damage = 60
                    ep = 60
                }
                {
                    name = "Rest"
                    damage = 0
                    ep = -40
                }
            ]
        }
    }
    printPlayerActions gameState Player1
    printfn ""

    gameState, Player1ToMove

let player1Moved gameState action =
    if (List.length gameState.player1Pokemon.actions) <= action then
        printfn "Please choose a valid action."
        printPlayerActions gameState Player1
        gameState, Player1ToMove
    else
        if gameState.player1Pokemon.ep < gameState.player1Pokemon.actions.[action].ep then
            printfn "\nNot enough EP, please try again"
            printPlayerActions gameState Player1
            gameState, Player1ToMove
        else 
            let newGameState = {
                player1Pokemon = { gameState.player1Pokemon with ep = gameState.player1Pokemon.ep - gameState.player1Pokemon.actions.[action].ep }
                player2Pokemon = { gameState.player2Pokemon with hp = gameState.player2Pokemon.hp - gameState.player1Pokemon.actions.[action].damage; ep = gameState.player2Pokemon.ep + 10 }
            }

            if newGameState |> isGameWonBy Player1 then
                printfn "GAME WON by %A" Player1
                newGameState, GameWon Player1
            else
                printPlayerActions newGameState Player2
                newGameState, Player2ToMove

let player2Moved gameState action =
    if (List.length gameState.player2Pokemon.actions) <= action then
        printfn "Please choose a valid action."
        printPlayerActions gameState Player2
        gameState, Player2ToMove
    else
        if gameState.player2Pokemon.ep < gameState.player2Pokemon.actions.[action].ep then
            printfn "\nNot enough EP, please try again"
            printPlayerActions gameState Player2
            gameState, Player2ToMove
        else 
            let newGameState = {
                player1Pokemon = { gameState.player1Pokemon with hp = gameState.player1Pokemon.hp - gameState.player2Pokemon.actions.[action].damage; ep = gameState.player1Pokemon.ep + 10  }
                player2Pokemon = { gameState.player2Pokemon with ep = gameState.player2Pokemon.ep - gameState.player2Pokemon.actions.[action].ep }
            }
                
            if newGameState |> isGameWonBy Player2 then
                printfn "GAME WON by %A" Player2
                newGameState, GameWon Player2
            else
                printPlayerActions newGameState Player1
                newGameState, Player1ToMove



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
