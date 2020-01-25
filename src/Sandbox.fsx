open System
open System.IO
open System.Threading
open System.Net
open Hopac
open Microsoft.FSharp.Control.CommonExtensions  
open FSharp.Data

let accountSpotifyUrl = "https://accounts.spotify.com/"
let redirectUri = "https://en9opgvu6mdrd.x.pipedream.net"

let clientIdSpotify = "922da6dc434b48c991ed3c2845af188f"
let clientSecretSpotify = "1877e394aaf44e3c88943f4fbc38b888"

let endpointToken = sprintf "%sapi/token" accountSpotifyUrl

let playlistUriSpotify = "spotify:playlist:0fF91CmIoQfDwzhlbiCEvw"

let authorizeUrl = sprintf 
                    "%sauthorize?response_type=code&client_id=%s&redirect_uri=%s" 
                    accountSpotifyUrl clientIdSpotify redirectUri
printfn "%s" authorizeUrl 

let authorization_code = "AQBeRLjxK2sJTEpwCdLVV7ninAhN2YlH6zIe2LSnUiqWbQerWOHk1EkH-rnqVhmuLEJVOF3HmnW3W7hc-A8vM3AwoJTpzuZRKQUi5FkVHSLcsvdJx-O3c_i3Ih27cY86XFKdYKgfoSkMWcxj1ZcwdCGIVfG-O8QuRFj2xQ4QXvvNn2GpajVfnkRlBk-HkhATt7n6PriLqEMzLM"

[<Literal>]
let spotifyRequestToken = """
{
  "grant_type": "authorization_code",
  "code": "code",
  "redirect_uri": "redirect_uri"
}
"""

type SpotifyRequestToken = JsonProvider<spotifyRequestToken, RootName="requestToken">

let contentTypes  = [ HttpContentTypes.Json ]
let headers       = [ 
                      HttpRequestHeaders.BasicAuth clientIdSpotify clientSecretSpotify
                      HttpRequestHeaders.Accept (String.concat ", " contentTypes) 
                    ]
let requestToken = SpotifyRequestToken.RequestToken (
                    "authorization_code",
                    authorization_code,
                    redirectUri
                  ) 



requestToken.JsonValue.Request endpointToken headers

 
Http.RequestString(endpointToken, body = FormValues ["test", "foo"])

Http.RequestString authorizeUrl
                           
let fetchUrlAsync url = 
        //let display url = printfn "finished downloading %s" url     
        async {
            let resp = url
                        |> Request.createUrl Get
                        |> Request.responseAsString
                        |> run
            return resp
        }           

// a list of sites to fetch
let sites = ["http://www.bing.com";
             "http://www.google.com";
             "http://www.microsoft.com";
             "http://www.amazon.com";
             "http://www.yahoo.com"]

#time                     // turn interactive timer on
sites                     // start with the list of sites
|> List.map fetchUrlAsync      // loop through each site and download
|> Async.Parallel
|> Async.RunSynchronously
#time 


let isNotMultiple x num = num % x <> 0
let crible n = 
    let rec criblerec acc list =  
            match list with
            | x :: xs -> 
                        let filtered = xs |> List.filter(isNotMultiple x)   
                        criblerec  (x :: acc) filtered                         
            | [] -> acc
    criblerec [] [2..n]
            
crible 100000           


[3..100] |> List.filter(isMultiple 2)

let primes =
    let a = ResizeArray[2]
    let grow() =
      let p0 = a.[a.Count-1]+1
      let b = Array.create p0 true
      for di in a do
        let rec loop i =
          if i<b.Length then
            b.[i] <- false
            loop(i+di)
        let i0 = p0/di*di
        loop(if i0<p0 then i0+di-p0 else i0-p0)
      for i=0 to b.Length-1 do
        if b.[i] then a.Add(p0+i)
    fun n ->
      while n >= a.Count do
        grow()
      a.[n];;



let rotate arr number = 
          let newvalue e = 
            match (e + number) % (Array.length arr ) with
            | 0 -> Array.length arr
            | x -> x 
          Array.map newvalue arr
rotate [|1..7|] 2

// Distribute an element y over a list:
// {x1,..,xn} --> {y,x1,..,xn}, {x1,y,x2,..,xn}, .. , {x1,..,xn,y}
let distrib y L =
    let rec aux pre post = seq {
        match post with
        | [] -> yield (L @ [y])
        | h::t -> yield (pre @ y::post)
                  yield! aux (pre @ [h]) t }
    aux [] L

let permutation str =
  let rec aux s = seq {
    match s with
    | h :: t -> yield (distrib h t)
                yield! aux t 
    |[] -> yield Seq.singleton []
  }
  aux str 



"abcdefgijkdqdsdqsqsdqdsdsqdsdqsdsq"
|> Seq.toList
|> permutation
|> Seq.concat
|> Seq.map( Seq.toArray >> (fun str -> new string(str)) )
|> Seq.toArray
|> Array.map (fun s -> printfn "%s" s)
