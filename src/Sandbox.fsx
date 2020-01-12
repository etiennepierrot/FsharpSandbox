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
