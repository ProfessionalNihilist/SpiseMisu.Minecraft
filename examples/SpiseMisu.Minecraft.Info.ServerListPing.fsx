#!/usr/bin/env fsharpi

(* This construct is for ML compatibility. The syntax '(typ,...,typ) ident'
   is not used in F# code. Consider using 'ident<typ,...,typ>' instead. *)
#nowarn "62"

#load "../SpiseMisu.Minecraft.fs"

open SpiseMisu

let mc () =
  use socket = TCP.Client.socket ()
  use client = TCP.Client.init socket "JohnDoe" "127.0.0.1" 25565
  
  (* Server List Ping: (server auto-kicks client afterwards) *)
  TCP.Client.serverListPing client
  |> Async.RunSynchronously
  |> printfn "Server info: %s"
  
  0

mc ()
