module Main
open ModelOrderSystem
open OrderSystem

[<EntryPoint>]
let main argv = 
    let boulon =
                    { Reference = "WA123"
                      CodeProduct = "12345" }
    let quote = availabilities 5 boulon
    quote 
    |> Option.map attemptBuying 
    |> Option.fold (fun _ s -> sprintf "%A" s) "Incorrect quantity"
    |> printfn "%A"
    0