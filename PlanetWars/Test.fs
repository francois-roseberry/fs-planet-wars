module Test

let test name fn expected =
       printfn name
       match fn with
       | actual when actual = expected -> printfn "Passed"
       | actual -> printfn "Failed. %A expected and got %A" expected actual

let test_exception name fn expected =
       printfn name
       try
            fn |> ignore
            printfn "Failed. Did not throw any exception"
       with
            | e when e = expected -> printfn "Passed. Exception thrown as expected"
            | _ -> printfn "Failed. Did not throw any exception at all"

let suite name fn = printfn name; fn