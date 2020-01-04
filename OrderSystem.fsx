open System

type QuantityRequested = QuantityRequested of int

type QuantityAvailable = QuantityAvailable of int

type Reference = string

type CodeProduct = string

type Money = Money of float

type Product =
    { Reference: Reference
      CodeProduct: CodeProduct }

type RequestAvailability =
    { Product: Product
      QuantityRequested: QuantityRequested }

type ResponseAvailability =
    { Product: Product
      QuantityAvailable: QuantityAvailable
      Price: Money }

type Quote =
    { Request: RequestAvailability
      Response: ResponseAvailability }

type Order =
    { Product: Product
      Quantity: QuantityRequested }

type Purchase =
    { Product: Product
      Quantity: QuantityRequested
      Price: Money
      DateDelivery: DateTime }

type StatusOrder =
    | OutofStock
    | Confirmed of Purchase

let availabilities qty product =

    let createQuantityRequested qty =
        if qty > 0 && qty < 100 then Some(QuantityRequested qty) else None

    let createRequest product qty =
        { RequestAvailability.Product = product
          QuantityRequested = qty }

    let askForAvailability (request: RequestAvailability) =
        let response =
            { Product = request.Product
              QuantityAvailable = QuantityAvailable(2)
              Price = Money 123.0 }
        Some
            { Request = request
              Response = response }

    qty
    |> createQuantityRequested
    |> Option.bind ((createRequest product) >> askForAvailability)



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




//call
let boulon =
    { Reference = "WA123"
      CodeProduct = "12345" }
let quote = availabilities 2 boulon
quote |> Option.map (attemptBuying)
