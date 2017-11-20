#!/usr/bin/env fsharpi

(* This construct is for ML compatibility. The syntax '(typ,...,typ) ident'
   is not used in F# code. Consider using 'ident<typ,...,typ>' instead. *)
#nowarn "62"

#load "../SpiseMisu.Minecraft.fs"

open SpiseMisu
open SpiseMisu.MineCraft

module Unsafe =
  
  (* TP to origo facing +X *)
  let origo = 
    [ Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp 0 4 0 270 0"
        ]
    ]
  
  let foundation =
    [ Protocol.packet 0x02
        [ "Message" => Protocol.string "/fill ~50 3 ~ ~ 3 ~50 concrete 8"
        ]
    ]
  
  let frame = 
    [ Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~1 ~ ~1 270 0"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/fill ~ ~ ~ ~ ~4 ~ concrete"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~48 ~ ~ 270 0"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/fill ~ ~ ~ ~ ~4 ~ concrete"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~ ~ ~48 270 0"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/fill ~ ~ ~ ~ ~4 ~ concrete"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~-48 ~ ~ 270 0"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/fill ~ ~ ~ ~ ~4 ~ concrete"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~ ~5 ~-48 270 0"
        ]
    ]
  
  let ceil  =
    [ Protocol.packet 0x02
        [ "Message" => Protocol.string "/fill ~-1 ~-1 ~-1 ~49 ~-1 ~49 concrete"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~-1 ~ ~-1 270 0"
        ]
    ]
  
  let windows =
    [ Protocol.packet 0x02
        [ "Message" =>
            Protocol.string "/fill ~ ~-2 ~ ~50 ~-5 ~ stained_glass_pane 15"
        ]
    ; Protocol.packet 0x02
        [ "Message" =>
            Protocol.string "/tp ~50 ~ ~ 270 0"
        ]
    ; Protocol.packet 0x02
        [ "Message" =>
            Protocol.string "/fill ~ ~-2 ~ ~ ~-5 ~50 stained_glass_pane 15"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~ ~ ~50 270 0"
        ]
    ; Protocol.packet 0x02
        [ "Message" =>
            Protocol.string "/fill ~-50 ~-2 ~ ~ ~-5 ~ stained_glass_pane 15"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~-50 ~ ~ 270 0"
        ]
    ; Protocol.packet 0x02
        [ "Message" =>
            Protocol.string "/fill ~ ~-2 ~ ~ ~-5 ~-50 stained_glass_pane 15"
        ]
    ; Protocol.packet 0x02
        [ "Message" => Protocol.string "/tp ~ ~ ~-50 270 0"
        ]
    ]
  
  let story =
    [ frame
    ; ceil
    ; windows
    ]
    |> List.collect id
  
  let stories =
    fun n ->
      fun _ -> story
      |> List.init    n
      |> List.collect id (* flatten the list of lists *)
  
  let skyscraper =
    [ origo
    ; foundation
    ; stories 5
    ]
    |> List.collect id

let mc () =
  use socket = TCP.Client.socket ()
  use client = TCP.Client.init socket "BobTheBuilder" "127.0.0.1" 25565
  
  TCP.Client.build client Unsafe.skyscraper
  |> Async.RunSynchronously
  
  0

mc ()
