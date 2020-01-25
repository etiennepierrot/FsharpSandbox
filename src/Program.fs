module Main =
    open OrderSystem.ModelOrderSystem
    open OrderSystem.Request

    [<EntryPoint>]
    let main argv = 
        let runRequestOrderProvider =
            let boulon =     {
                              Reference = "WA123"
                              CodeProduct = "12345" }

            
            async{
            let! quote = availabilities 5 boulon
            quote 
            |> Option.map attemptBuying 
            |> Option.fold (fun _ s -> sprintf "%A" s) "Incorrect quantity"
            |> printfn "%A"
            return 0} |> Async.RunSynchronously
            
        Spotify.PlaylistFetcher.Run |> ignore
        0;
        //runRequestOrderProvider