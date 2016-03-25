module Test

let test name fn expected =
       printfn name
       match fn with
       | actual when actual = expected -> printfn "Passed"
       | actual -> printfn "Failed. %A expected and got %A" expected actual

let suite name fn = printfn name; fn