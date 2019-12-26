open System
open System.IO
open System.Threading
open System.Net
open HttpFs.Client
open Hopac
open Microsoft.FSharp.Control.CommonExtensions  


                           
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