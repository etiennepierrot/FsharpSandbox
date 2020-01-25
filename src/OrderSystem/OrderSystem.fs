
namespace OrderSystem
module Request =
    open System
    open ModelOrderSystem
    
    let bind (f: 'a -> Async<'b option>) (a: 'a option) = async {
          match a with
          | None -> return None
          | Some q -> let r = f q
                      return! r
          }

    let availabilities qty product =

        let createQuantityRequested qty =
            if qty > 0 && qty < 100 then Some(QuantityRequested qty) else None

        let createRequest product qty =
            { RequestAvailability.Product = product
              QuantityRequested = qty }

        let askForAvailability (request: RequestAvailability) =
            async {
                do! Async.Sleep 2000
                let response =
                    { Product = request.Product
                      QuantityAvailable = QuantityAvailable(2)
                      Price = Money 123.0 }
                return Some
                    { Request = request
                      Response = response }
            }

        let requestProductAsync = (createRequest product) >> askForAvailability
        
        
            
        let quantityRequested = qty |> createQuantityRequested
        bind requestProductAsync quantityRequested
        
        
     
    let attemptBuying quote =

        let toOrder (request: RequestAvailability) =
            { Order.Product = request.Product
              Order.Quantity = request.QuantityRequested }

        let isEnoughStockAvailable quote =
            let (QuantityRequested qtyRequested) = quote.Request.QuantityRequested
            let (QuantityAvailable qtyAvailable) = quote.Response.QuantityAvailable
            qtyRequested <= qtyAvailable

        let purchaseOrder (order : Order )=
            Confirmed
                ({ Product = order.Product
                   Quantity = order.Quantity
                   Price = Money 123.0
                   DateDelivery = new DateTime(2018, 1, 1) })

        match quote with
        | q when isEnoughStockAvailable q ->
            q.Request
            |> toOrder
            |> purchaseOrder
        | _ -> OutofStock