let text = "Hello, World!"
text.Length

let greetPerson name age =
    sprintf "Hello, %s. You are %d years old" name age
let greeting = greetPerson "Fred" 25

let countWords (text:string) =
    let words = text.Split(' ')
    words.Length

countWords "Hallo, das ist ein Test"

let saveWordsAndWordCount (text:string) =
    let wordCount = countWords text
    System.IO.File.WriteAllLines(__SOURCE_DIRECTORY__ + @"\data.txt", [|text; sprintf "%i" wordCount|])

saveWordsAndWordCount "Hallo, das ist ein Test"

open System
open System.Net
open System.Windows.Forms

let showUrl (uri:Uri)=
    let text =
        let webClient = new WebClient()
        webClient.DownloadString(uri)
    let browser = new WebBrowser(ScriptErrorsSuppressed = true,
                   Dock = DockStyle.Fill,
                   DocumentText = text)
    let form = new Form(Text = "Hello from F#!")
    form.Controls.Add browser
    form.Show()

showUrl (Uri "http://orf.at")

let add a b = a + b
let sub a b = a - b
let mul a b = a * b
let div a b = a / b

add 1 2
sub 2 1
mul 6 7
div 6 3

3 |> add 4
3 |> sub 4
3 |> mul 4
3 |> div 2

let sayHello someValue =
    let innerFunction number =
        if number > 10 then "Isaac"
        elif number > 20 then "Fred"
        else "Sara"

    let resultOfInner =
        if someValue < 10.0 then innerFunction 5
        else innerFunction 15

    "Hello " + resultOfInner

let result = sayHello 10.5

let startingPetrol = 100

let drive (currentPetrol, location) =
    let locHome = "Home"
    let locOffice = "Office"
    let locStadium = "Stadium"
    let locGasStation = "Gas Station"
    let getRequiredPetrol location =
        if location = locHome then 25
        elif location = locOffice then 50
        elif location = locStadium then 25
        elif location = locGasStation then 10
        else 0
    let hasEnoughPetrol (currentPetrol, value) =
        currentPetrol >= value
    let refuel currentPetrol =
        currentPetrol + 50
    let consumedPetrol = getRequiredPetrol location
    if consumedPetrol > currentPetrol then currentPetrol
    else currentPetrol - consumedPetrol

let firstDrive = drive(startingPetrol, "Home")

type City = {
    Name : string
    Inhabitants : int
}

let cities = [
    {Name = "Vienna"; Inhabitants = 1987000};
    {Name = "Berlin"; Inhabitants = 3769000};
    {Name = "London"; Inhabitants = 8982000};
    {Name = "Paris"; Inhabitants = 2148000}]

let smallAndBigCities =
    cities
    |> List.partition (fun city -> city.Inhabitants > 3000000)

let numbers = [ 1.0 .. 10.0 ]
let total = numbers |> List.sum
let average = numbers |> List.average
let max = numbers |> List.max
let min = numbers |> List.min

let inventory =
    ["Apples", 0.33; "Oranges", 0.23; "Bananas", 0.45]
    |> Map.ofList 

let cheapFruit, expensiveFruit =
    inventory
    |> Map.partition(fun _ cost -> cost < 0.3)

let length xs =
    xs
    |> Seq.fold (fun state _ -> state + 1) 0

length cities

let maxFold xs =
    match xs with
    | [] -> failwith "Empty List"
    | x::xs -> (x, xs) ||> Seq.fold (fun state input -> if input > state then input else state)

let maxReduce xs =
    xs
    |> List.reduce (fun state input -> if input > state then input else state)

maxFold numbers
maxReduce numbers




type Rule = string -> bool * string

let rules : Rule list =
    [ fun text -> (text.Split ' ').Length = 3, "Must be three words"
      fun text -> text.Length <= 30, "Max length is 30 characters"
      fun text -> text
                  |> Seq.filter System.Char.IsLetter
                  |> Seq.forall System.Char.IsUpper, "All letters must be caps" ]

let buildValidator (rules : Rule list) =
    rules
    |> List.reduce(fun firstRule secondRule ->
        fun word ->
            let passed, error = firstRule word
            if passed then
                let passed, error = secondRule word
                if passed then true, "" else false, error
            else false, error)

let validate = buildValidator rules
let word = "HELLO FrOM F#"

validate word

type Customer = {
    Name : string
}

let getCustomer name customers =
    match List.tryFind (fun customer -> customer.Name = name) customers with
    | Some customer -> customer, customers
    | None ->
        printfn $"Creating Customer {name}"
        let customer = {Name = name}
        customer, customer :: customers

type Account = {
    Id : System.Guid
    Customer : Customer
    Balance : float
}

let getAccount customer accounts =
    match List.tryFind (fun account -> account.Customer = customer) accounts with
    | Some account -> account, accounts
    | None ->
        let id = System.Guid.NewGuid()
        printfn $"Creating Account for Customer {customer.Name} with Id {id}"
        let account = { Id = id; Customer = customer; Balance = 0.0 }
        account, account :: accounts

let changeBalance amount action account =
    match amount with
    | x when x < 0.0 -> failwith "Negative amount"
    | _ ->
        let newBalance = action account.Balance amount
        match newBalance with
        | x when x < 0.0 ->
            printfn $"Action was denied: new Balance cannot be less than 0 and would be {newBalance}"
            account
        | _ -> { account with Balance = action account.Balance amount }

let deposit amount account =
    account
    |> changeBalance amount (+)

let withdraw amount account =
    account
    |> changeBalance amount (-)

let dagobert, customers = getCustomer "Dagobert Duck" []
let dagobertsAccount, accounts = getAccount dagobert []
dagobertsAccount
|> deposit 100000.0
|> withdraw 20.0
|> withdraw 100000.0
let donald, newCustomers = getCustomer "Donald Duck" customers

