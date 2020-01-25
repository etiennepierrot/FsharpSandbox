namespace Spotify
module PlaylistFetcher =
    open System.Net
    open System
    open Microsoft.FSharp.Control
   
    type HttpListener with
        static member Run (url:string,handler: (HttpListenerRequest -> HttpListenerResponse -> Async<unit>)) = 
            let listener = new HttpListener()
            listener.Prefixes.Add url
            listener.Start()
            let asynctask = Async.FromBeginEnd(listener.BeginGetContext,listener.EndGetContext)
            async {
                while true do 
                    let! context = asynctask
                    Async.Start (handler context.Request context.Response)
            } |> Async.Start 
            listener

    let runServer requestHandler =
        HttpListener.Run("http://*:80/callback_spotify/",(fun req resp -> 
                async {
                    printfn "Call start : %s" req.RawUrl
                    requestHandler req
                    let out = Text.Encoding.ASCII.GetBytes "hello world"
                    resp.OutputStream.Write(out,0,out.Length)
                    resp.OutputStream.Close()
                }
            )) |> ignore

    let workflow (req : HttpListenerRequest)  =
        let code = req.QueryString.["Code"]
        printfn "Authorization Code : %s" code
        let oauthToken = Authorization.GetAccessToken code
        printfn "AccessToken : %s" oauthToken.AccessToken 
        let playlists = SpotifyConnector.Playlist oauthToken.AccessToken
        playlists.Items
        |> Array.map (fun x -> printfn "%s" x.Track.Name )
        |> ignore

    
    let Run =
        printfn "go to this url for getting authorizationCode"
        printfn "%s" Authorization.AuthorizeUrl
        runServer workflow
        Console.ReadKey()
     
    
    
    

