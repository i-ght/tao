namespace Tao

open System.Text.RegularExpressions

open Tao.EnglishTexts

[<Struct>]
type Tao =
    { UniqueIdentifier: int
      Text: string list }

module Tao =

    let private taoSliceRegex =
        Regex(
            "(\d+)\n\n(.*?)\n\nBack to Table of Contents", 
            RegexOptions.IgnoreCase 
            ||| RegexOptions.Compiled
            ||| RegexOptions.Singleline
        )
    let private slice (tao: string) =
        let sliceMatches = taoSliceRegex.Matches(tao)

        let rec procMatches (matches: Match list) index acc =
            match matches with
            | [] -> List.rev acc
            | head :: tail ->
                let _id = head.Groups[1].Value
                let text = head.Groups[2].Value
                let lines =
                    text.Split("\n")
                    |> Array.mapi (fun i line -> $"{i}. {line}")
                let index = index + 1
                let tao = { UniqueIdentifier = index; Text = List.ofArray lines }
                let taos = tao :: acc
                procMatches tail index taos

        procMatches (List.ofSeq sliceMatches) 0 []

    let construct () =
        [ slice GiaFuFenAndJaneEnglish.text; 
          slice CharlesMuller.text ]
