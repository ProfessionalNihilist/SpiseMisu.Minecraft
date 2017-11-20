#!/usr/bin/env fsharpi

(* This construct is for ML compatibility. The syntax '(typ,...,typ) ident'
   is not used in F# code. Consider using 'ident<typ,...,typ>' instead. *)
#nowarn "62"

#load "../SpiseMisu.Minecraft.fs"

open SpiseMisu
open SpiseMisu.MineCraft

module Safe =
  
  let pilar = minecraft {
    fill
      Relative 0 Relative 0 Relative 0
      Relative 0 Relative 4 Relative 0
      Concrete White
  }
  
  let frame = minecraft {
    teleport
      Relative 1 Relative 0 Relative 1
      East Horizon
      
    add pilar
    
    teleport
      Relative 48 Relative 0 Relative 0
      East Horizon
      
    add pilar
    
    teleport
      Relative 0 Relative 0 Relative 48
      East Horizon
      
    add pilar
    
    teleport
      Relative -48 Relative 0 Relative 0
      East Horizon
      
    add pilar
    
    teleport
      Relative 0 Relative 5 Relative -48
      East Horizon
  }
  
  let ceil = minecraft {
    fill
      Relative -1 Relative -1 Relative -1
      Relative 49 Relative -1 Relative 49
      Concrete White
    
    teleport
      Relative -1 Relative 0 Relative -1
      East Horizon
  }
  
  let windows = minecraft {
    fill
      Relative  0 Relative -2 Relative 0
      Relative 50 Relative -5 Relative 0
      StainedGlassPane Black
    
    teleport
      Relative 50 Relative 0 Relative 0
      East Horizon
    
    fill
      Relative 0 Relative -2 Relative 0
      Relative 0 Relative -5 Relative 50
      StainedGlassPane Black
    
    teleport
      Relative 0 Relative 0 Relative 50
      East Horizon
    
    fill
      Relative -50 Relative -2 Relative 0
      Relative   0 Relative -5 Relative 0
      StainedGlassPane Black
    
    teleport
      Relative -50 Relative 0 Relative 0
      East Horizon
    
    fill
      Relative 0 Relative -2 Relative   0
      Relative 0 Relative -5 Relative -50
      StainedGlassPane Black
    
    teleport
      Relative 0 Relative 0 Relative -50
      East Horizon
  }
  
  let story = minecraft {
    add frame
    add ceil
    add windows
  }
  
  let stories =
    fun n ->
      fun _ -> story
      |> List.init    n
      |> List.collect id (* flatten the list of lists *)
  
  let skyscraper = minecraft {
    (* origo facing +X (East) *)
    teleport
      Global 0 Global 4 Global 0
      East Horizon
    
    (* foundation *)
    fill
      Relative 50 Global 3 Relative  0
      Relative  0 Global 3 Relative 50
      Concrete LightGray
    
    (* stories *)
    add (stories 5)
  }

let mc () =
  use socket = TCP.Client.socket ()
  use client = TCP.Client.init socket "BobTheBuilder" "127.0.0.1" 25565
  
  TCP.Client.build client Safe.skyscraper
  |> Async.RunSynchronously
  
  0

mc ()
