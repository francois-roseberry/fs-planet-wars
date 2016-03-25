// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#load "Test.fs"
#load "Game.fs"
open Test
open Game

suite "Tests for simple list alteration" (
    let constant_fn x = 1

    test "only once" (
        let constant_fn x = 1
        alter_list_index [3;3;3] constant_fn 2
    ) [3;3;1]

    test "original list is not modified" (
        let constant_fn x = 1
        let new_list = alter_list_index [3;3;3] constant_fn 1
        alter_list_index new_list constant_fn 0 |> ignore
        new_list
    ) [3;1;3]
)

type CustomRecord = { A: int; B: int }

suite "Tests for list alteration with records" (
    let increment c = {A=c.A+1;B=c.B+1}

    test "after one alteration" (
        let constant_fn x = 1
        alter_list_index [{A=1;B=2};{A=2;B=2}] increment 0
    ) [{A=2;B=3};{A=2;B=2}]

    test "original list is not modified" (
        let constant_fn x = 1
        let new_list = alter_list_index [{A=1;B=2};{A=2;B=2}] increment 0
        alter_list_index new_list increment 1 |> ignore
        new_list
    ) [{A=2;B=3};{A=2;B=2}]
)