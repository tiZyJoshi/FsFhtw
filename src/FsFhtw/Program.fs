[<EntryPoint>]
let main argv =
    printfn "Welcome to the FHTW Domain REPL!"
    printfn "Please enter your commands to interact with the system."
    printfn "Press CTRL+C to stop the program."
    
    let initialState = Domain.init ()
    printf "> "
    Repl.loop initialState
    0 // return an integer exit code
