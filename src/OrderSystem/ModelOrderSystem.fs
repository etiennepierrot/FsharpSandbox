namespace OrderSystem
module ModelOrderSystem =
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