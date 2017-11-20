#!/usr/bin/env fsharpi

(* This construct is for ML compatibility. The syntax '(typ,...,typ) ident'
   is not used in F# code. Consider using 'ident<typ,...,typ>' instead. *)
#nowarn "62"

#load "../SpiseMisu.Minecraft.fs"

open SpiseMisu
open SpiseMisu.MineCraft

module Safe =
  
  let pilar =
    fun color -> minecraft {
      fill
        Relative 0 Relative 0 Relative 0
        Relative 0 Relative 4 Relative 0
        Concrete color
  }
  
  let frame =
    fun color -> minecraft {
      teleport
        Relative 1 Relative 0 Relative 1
        East Horizon
        
      add (pilar color)
      
      teleport
        Relative 48 Relative 0 Relative 0
        East Horizon
        
      add (pilar color)
      
      teleport
        Relative 0 Relative 0 Relative 48
        East Horizon
        
      add (pilar color)
      
      teleport
        Relative -48 Relative 0 Relative 0
        East Horizon
        
      add (pilar color)
      
      teleport
        Relative 0 Relative 5 Relative -48
        East Horizon
  }
  
  let ceil =
    fun color -> minecraft {
      fill
        Relative -1 Relative -1 Relative -1
        Relative 49 Relative -1 Relative 49
        Concrete color
    
      teleport
        Relative -1 Relative 0 Relative -1
        East Horizon
    }
  
  let windows =
    fun window -> minecraft {
      fill
        Relative  0 Relative -2 Relative 0
        Relative 50 Relative -5 Relative 0
        StainedGlassPane window
      
      teleport
        Relative 50 Relative 0 Relative 0
        East Horizon
      
      fill
        Relative 0 Relative -2 Relative 0
        Relative 0 Relative -5 Relative 50
        StainedGlassPane window
      
      teleport
        Relative 0 Relative 0 Relative 50
        East Horizon
      
      fill
        Relative -50 Relative -2 Relative 0
        Relative   0 Relative -5 Relative 0
        StainedGlassPane window
      
      teleport
        Relative -50 Relative 0 Relative 0
        East Horizon
      
      fill
        Relative 0 Relative -2 Relative   0
        Relative 0 Relative -5 Relative -50
        StainedGlassPane window
      
      teleport
        Relative 0 Relative 0 Relative -50
        East Horizon
  }
  
  let story =
    fun concrete window -> minecraft {
      add (frame concrete)
      add (ceil  concrete)
      add (windows window)
    }
  
  let stories =
    fun n concrete window ->
      fun _ -> minecraft { add (story concrete window) }
      |> List.init    n
      |> List.collect id (* flatten the list of lists *)
  
  let skyscraper =
    fun concrete window -> minecraft {
      (* origo facing +X (East) *)
      teleport
        Global 0 Global 4 Global 0
        East Horizon
    
      (* foundation *)
      fill
        Relative 50 Global 3 Relative  0
        Relative  0 Global 3 Relative 50
        Concrete concrete
    
      (* stories *)
      add (stories 5 concrete window)
  }

let mc () =
  use socket = TCP.Client.socket ()
  use client = TCP.Client.init socket "BobTheBuilder" "127.0.0.1" 25565

  (* Razer HQ *)
  TCP.Client.build client (Safe.skyscraper color.Black color.Lime)
  |> Async.RunSynchronously
  
  0

mc ()
