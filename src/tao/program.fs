open System

open Tao.Lib


let taoSlices = Tao.construct()
let r = Random()


let spinTaoWheel () =
    let index = r.Next(0, List.length taoSlices[0])
    let slice = taoSlices[0][index]
    slice

let taoSlice = spinTaoWheel ()
printfn "%A" taoSlice

