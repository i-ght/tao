namespace Tao

open System.Text.RegularExpressions

open Tao.EnglishTexts

[<Struct>]
type Tao =
    { Slice: int
      Lines: string list }

module Tao =

    let private taoSliceRegex =
        Regex(
            "(\d+)\n\n(.*?)\n\nBack to Table of Contents", 
            RegexOptions.IgnoreCase 
            ||| RegexOptions.Compiled
            ||| RegexOptions.Singleline
        )
    let constructTao (tao: string) =
        let sliceMatches = taoSliceRegex.Matches(tao)

        let rec processMatches (matches: Match list) index acc =
            match matches with
            | [] -> List.rev acc
            | head :: tail ->
                let _id = head.Groups.[1].Value
                let text = head.Groups.[2].Value
                let lines =
                    text.Split("\n")
                    |> Array.mapi (fun i line -> $"{i}. {line}")
                let index = index + 1
                let newSlice = { Slice = index; Lines = List.ofArray lines }
                let newAcc = newSlice :: acc
                processMatches tail index newAcc

        processMatches (List.ofSeq sliceMatches) 0 []

    let construct () =
        [ constructTao GiaFuFenAndJaneEnglish.text; 
        constructTao CharlesMuller.text ]
