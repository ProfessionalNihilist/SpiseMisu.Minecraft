namespace SpiseMisu

(* This construct is for ML compatibility. The syntax '(typ,...,typ) ident'
   is not used in F# code. Consider using 'ident<typ,...,typ>' instead. *)
#nowarn "62"

(* Override implementations in augmentations are now deprecated. Override
implementations should be given as part of the initial declaration of a type. *)
#nowarn "60"

[<RequireQualifiedAccess>]
module List =
  
  [<RequireQualifiedAccess>]
  module Lazy =
  
    type 'a lazylist = 'a list -> 'a list
    
    let nil  : 'a lazylist = id
    let cons : 'a -> 'a lazylist = fun x xs -> x :: xs
  
    let toArray : 'a lazylist -> 'a array = fun xs -> xs [] |> List.toArray

module MineCraft =
  
  open System
  
  (* Data could be replaced with a TypeProvider that retrieves info from wiki *)
  type position =
    | Relative | Global
  type color =
    | White     | Orange | Magenta | LightBlue
    | Yellow    | Lime   | Pink    | Gray
    | LightGray | Cyan   | Purple  | Blue
    | Brown     | Green  | Red     | Black
    | Colorless
  type facing =
    | North | East     | South  | West
  type look   =
    | Up    | Horizon  | Down
  type block  =
    | Air  | Concrete  | Planks | StainedGlassPane
    | Snow | Stone     | Water
  
  let private condition : byte -> bool = fun b -> b &&& 0b10000000uy = 0x00uy
  
  let inline (=>) x y = x,y
    
  module Protocol =
    
    let version = 340
    
    open System.Text
    
    (* http://wiki.vg/Protocol#Data_types *)
    type DataType =
      | Boolean   of Boolean
      | Byte      of Byte
      | UByte     of UByte
      | Short     of Short
      | UShort    of UShort
      | Int       of Int
      | Long      of Long
      | Float     of Float
      | Double    of Double
      | String    of String
      | Chat      of Chat
      | VarInt    of VarInt
      | ByteArray of ByteArray
    
    and [<Struct>] Boolean   = private Boolean   of bool
    and [<Struct>] Byte      = private Byte      of int8
    and [<Struct>] UByte     = private UByte     of uint8
    and [<Struct>] Short     = private Short     of int16
    and [<Struct>] UShort    = private UShort    of uint16
    and [<Struct>] Int       = private Int       of int
    and [<Struct>] Long      = private Long      of int64
    and [<Struct>] Float     = private Float     of float32
    and [<Struct>] Double    = private Double    of float
    and            String    = private String    of prefix:VarInt * utf8:string
    and [<Struct>] Chat      = private Chat      of String
    and [<Struct>] VarInt    = private VarInt    of int
    and [<Struct>] VarLong   = private VarLong   of int64
    (*
    TODO:
    and EntityMetadata ...
    and Slot ...
    and NBTTag ...
    and Position ...
    and Angle ...
    and UUID ...
    and Optional ...
    and Array ...
    and Enum ...
    *)
    and [<Struct>] ByteArray = private ByteArray of byte array
    
    module PM =
      
      (* TODO: Add the rest of the types *)
      
      let (|Short|)  : Short  -> int16           = function
        | Short x -> x
      let (|Long|)   : Long   -> int64           = function
        | Long  x -> x
      let (|String|) : String -> VarInt * string = function
        | String (prefix,utf8) -> (prefix,utf8)
    
    (* Moved .ToString overriding for better TDD/DDD readability *)
    type Boolean   with
      override x.ToString () = let (Boolean v)          = x in sprintf "%b" v
    type Byte      with
      override x.ToString () = let (Byte v)             = x in sprintf "%i" v
    type UByte     with
      override x.ToString () = let (UByte v)            = x in sprintf "%i" v
    type Short     with
      override x.ToString () = let (Short v)            = x in sprintf "%i" v
    type UShort    with
      override x.ToString () = let (UShort v)           = x in sprintf "%i" v
    type Int       with
      override x.ToString () = let (Int v)              = x in sprintf "%i" v
    type Long      with
      override x.ToString () = let (Long v)             = x in sprintf "%i" v
    type Float     with
      override x.ToString () = let (Float v)            = x in sprintf "%f" v
    type Double    with
      override x.ToString () = let (Double v)           = x in sprintf "%f" v
    type String    with
      override x.ToString () = let (String (_,v))       = x in sprintf "%s" v
    type Chat      with
      override x.ToString () = let (Chat(String (_,v))) = x in sprintf "%s" v
    type VarInt    with
      override x.ToString () = let (VarInt v)           = x in sprintf "%i" v
    type VarLong   with
      override x.ToString () = let (VarLong v)          = x in sprintf "%i" v
    type ByteArray with
      override x.ToString () = let (ByteArray v)        = x in sprintf "%A" v
    
    module Utils =
      
      module Bytes =
        
        let fromVarInt : VarInt -> byte array =
          fun (VarInt x) ->
            let rec aux acc = function
              | 0 ->
                if acc [] |> List.isEmpty then List.Lazy.cons 0x00uy else acc
              | n ->
                let b  = n           &&& 0b01111111 |> byte
                let n' = n |> uint32 >>> 0x00000007 |> int
                let b' = if n' = 0x00000000 then b else b ||| 0b10000000uy
                aux (acc << List.Lazy.cons b') n'
            aux List.Lazy.nil x [] |> List.toArray
        
        let fromBoolean : Boolean -> byte array =
          fun (Boolean x) ->
            if x then [| 0x01uy |] else [| 0x00uy |]
        
        let fromString : String -> byte array =
          fun (String (prefix,x)) ->
            Array.append (fromVarInt prefix) (Encoding.UTF8.GetBytes x)
        
        let fromByteArray : ByteArray -> byte array =
          fun (ByteArray xs) ->
            xs
        
        let toVarInt : byte array -> VarInt =
          fun bs ->
            let rec aux acc i = function
              | [     ] -> acc
              | b :: bs ->
                let n    = b   &&& 0b01111111uy    |> int
                let acc' = acc ||| (n <<< (7 * i))
                aux acc' (i+1) bs
            bs |> Array.toList |> aux 0 0 |> VarInt
        
        let toString : byte array -> String =
          fun bs ->
            let index = bs |> Array.tryFindIndex condition
            let ((VarInt p) as prefix) =
              match index with
                | None   -> toVarInt [| 0x00uy |]
                | Some i -> toVarInt bs.[ .. i  ]
            let utf8 =
              match index with
                | None   -> Encoding.UTF8.GetString [|             |]
                | Some i -> Encoding.UTF8.GetString bs.[ (i+1) .. p ]
            (prefix, utf8) |> String
        
        module BigEndian =
          
          let private littleEndian = BitConverter.IsLittleEndian
          
          let fromString : string -> byte array =
            Encoding.BigEndianUnicode.GetBytes
          
          let fromByte : Byte -> byte array =
            fun (Byte x) ->
              (*
                 Two's complement:
              
                 https://en.wikipedia.org/wiki/Two%27s_complement
                 
                 and
                 
                 http://sandbox.mc.edu/~bennet/cs110/tc/dtotc.html
                 
                 -72y |> byte = 184uy
                 
                 and
                 
                 -72y |> abs |> byte |> (~~~) |> ((+) 1uy) = 184uy
              *)
              [| byte x |]
          
          let fromUByte : UByte -> byte array =
            fun (UByte x) ->
              [| x |]
          
          let fromShort : Short -> byte array =
            fun (Short x) ->
              let bs  = BitConverter.GetBytes x
              if littleEndian then bs |> Array.rev else bs
          
          let fromUShort : UShort -> byte array =
            fun (UShort x) ->
              let bs  = BitConverter.GetBytes x
              if littleEndian then bs |> Array.rev else bs
          
          let fromInt : Int -> byte array =
            fun (Int x) ->
              let bs  = BitConverter.GetBytes x
              if littleEndian then bs |> Array.rev else bs
          
          let fromLong : Long -> byte array =
            fun (Long x) ->
              let bs  = BitConverter.GetBytes x
              if littleEndian then bs |> Array.rev else bs
          
          let fromFloat : Float -> byte array =
            fun (Float x) ->
              let bs = BitConverter.GetBytes x
              if littleEndian then bs |> Array.rev else bs
        
          let fromDouble : Double -> byte array =
            fun (Double x) ->
              let bs  = BitConverter.GetBytes x
              if littleEndian then bs |> Array.rev else bs
          
          let toShort : byte array -> Short =
            fun bs ->
              let bs' = if littleEndian then bs |> Array.rev else bs
              (bs',0) |> BitConverter.ToInt16 |> Short
          
          let toUShort : byte array -> UShort =
            fun bs ->
              let bs' = if littleEndian then bs |> Array.rev else bs
              (bs',0) |> BitConverter.ToUInt16 |> UShort
          
          let toLong : byte array -> Long =
            fun bs ->
              let bs' = if littleEndian then bs |> Array.rev else bs
              (bs',0) |> BitConverter.ToInt64 |> Long
    
    open Utils.Bytes
    
    let typeTobytes : DataType -> byte array = function
      | DataType.Boolean    x  -> x |>           fromBoolean
      | DataType.Byte       x  -> x |> BigEndian.fromByte
      | DataType.UByte      x  -> x |> BigEndian.fromUByte
      | DataType.Short      x  -> x |> BigEndian.fromShort
      | DataType.UShort     x  -> x |> BigEndian.fromUShort
      | DataType.Int        x  -> x |> BigEndian.fromInt
      | DataType.Long       x  -> x |> BigEndian.fromLong
      | DataType.Float      x  -> x |> BigEndian.fromFloat
      | DataType.Double     x  -> x |> BigEndian.fromDouble
      | DataType.String     x  -> x |>           fromString
      | DataType.Chat (Chat x) -> x |>           fromString
      | DataType.VarInt     x  -> x |>           fromVarInt
      | DataType.ByteArray  x  -> x |>           fromByteArray
    
    let bytesLength : DataType -> int = typeTobytes >> Array.length
    
    let varInt     : int        -> DataType = VarInt    >> DataType.VarInt
    let bool       : bool       -> DataType = Boolean   >> DataType.Boolean
    let byte       : int8       -> DataType = Byte      >> DataType.Byte
    let ubyte      : uint8      -> DataType = UByte     >> DataType.UByte
    let short      : int16      -> DataType = Short     >> DataType.Short
    let ushort     : uint16     -> DataType = UShort    >> DataType.UShort
    let int        : int        -> DataType = Int       >> DataType.Int
    let long       : int64      -> DataType = Long      >> DataType.Long
    let float      : float32    -> DataType = Float     >> DataType.Float
    let double     : float      -> DataType = Double    >> DataType.Double
    let string     : string     -> DataType =
      fun x ->
        let len = x |> Encoding.UTF8.GetBytes |> Array.length
        String (VarInt len,x) |> DataType.String
    let byteArray  : byte array -> DataType = ByteArray >> DataType.ByteArray
  
    type Packet = private Packet of length:VarInt * pid:VarInt * data:ByteArray
    
    type Packet with
      override x.ToString () =
        let (Packet(VarInt length, VarInt pid, ByteArray data)) = x
        sprintf "(%i,%i,%A)" length pid data
    
    let packet : int -> (string * DataType) list -> Packet =
      fun pid ts ->
        let bs  = ts |> List.toArray |> Array.collect (snd >> typeTobytes)
        let len = Array.length bs + 1
        Packet(VarInt len, VarInt pid, ByteArray bs)
    
    let pkgLength : Packet -> int        = fun (Packet (VarInt x,_,_))    -> x
    let pkgId     : Packet -> int        = fun (Packet (_,VarInt x,_))    -> x
    let pkgData   : Packet -> byte array = fun (Packet (_,_,ByteArray x)) -> x
    
    let send : (byte array -> unit Async) -> Packet -> unit Async =
      fun write (Packet (length,pid,data)) ->
        async {
          do! length |> DataType.VarInt    |> typeTobytes |> write
          do! pid    |> DataType.VarInt    |> typeTobytes |> write
          do! data   |> DataType.ByteArray |> typeTobytes |> write
          
          return ()
        }
    
    let receive :
      ((byte -> bool) -> byte array Async)
      -> (int -> byte array Async)
      -> Packet Async =
        fun readUntil read ->
          async {
            let! xs                      = condition               |> readUntil
            let! ys                      = condition               |> readUntil
            let (VarInt len, VarInt pid) = toVarInt xs, toVarInt ys
            let! bs                      = len - (Array.length ys) |> read
            
            return Packet(VarInt len, VarInt pid, ByteArray bs)
          }
  
  [<AutoOpen>]
  module DSL =
    
    let private position : position -> string = function
      | Relative -> "~" | Global -> ""
    
    let private color = function
      | White     -> 00 | Orange -> 01 | Magenta -> 02 | LightBlue -> 03
      | Yellow    -> 04 | Lime   -> 05 | Pink    -> 06 | Gray      -> 07
      | LightGray -> 08 | Cyan   -> 09 | Purple  -> 10 | Blue      -> 11
      | Brown     -> 12 | Green  -> 13 | Red     -> 14 | Black     -> 15
      | Colorless -> 00
    
    let private facing : facing -> int = function
      | North -> -180 | East -> -90 | South -> 0 | West -> 90
    
    let private look : look -> int = function
      | Up -> -90 | Horizon -> 0 | Down -> 90
    
    let private block : block -> string =
      fun b ->
        (* Example: StainedGlassPane -> stained_glass_pane *)
        sprintf "%A" b
        |> Seq.map string
        |> Seq.mapi (
          fun i x ->
            if i <> 0 && x = x.ToUpper() then
              "_" + x.ToLower()
            else
              x.ToLower()
        )
        |> Seq.fold (+) ""
    
    let data : string -> Protocol.Packet =
      fun msg ->
        Protocol.packet 0x02
          [ "Message" => Protocol.string msg
          ]
      
    [<Sealed>]
    type MinecraftBuilder () =
      member __.Yield (()) : Protocol.Packet list = []
      
      [<CustomOperation("teleport")>]
      member __.Teleport (pkts,px,x,py,y,pz,z,d,l) =
        let msg =
          let (px',py',pz') = position px, position py, position pz
          
          sprintf "/tp %s%i %s%i %s%i %i %i"
            px' x py' y pz' z (facing d) (look l)
          
        [ pkts; [ data msg ] ] |> List.collect id
      
      [<CustomOperation("fill")>]
      member __.Fill (pkts,px1,x1,py1,y1,pz1,z1,px2,x2,py2,y2,pz2,z2,blk,c) =
        let msg =
          let (px1',py1',pz1') = position px1, position py1, position pz1
          let (px2',py2',pz2') = position px2, position py2, position pz2
          let (blk'        )   = block blk
          let (c'          )   = color c
          
          sprintf "/fill %s%i %s%i %s%i %s%i %s%i %s%i %s %i"
            px1' x1 py1' y1 pz1' z1 px2' x2 py2' y2 pz2' z2 blk' c'
            
        [ pkts; [ data msg ] ] |> List.collect id
      
      [<CustomOperation("add")>]
      member __.Add (xs,ys:Protocol.Packet list) : Protocol.Packet list =
        [ xs; ys] |> List.collect id
    
    let minecraft = MinecraftBuilder ()

module TCP =
  
  module Client =
    
    open System
    open System.IO
    open System.Net
    open System.Net.Sockets
    open System.Text
    
    open MineCraft
    
    type communication = Sending | Received
    
    type client = private Config of config with
      interface IDisposable with
        member client.Dispose () =
          let (Config c) = client
          try
            c.socket.Close()
            printfn "Closed connection."
          with
            ex -> printfn "Error closing connection: %s" ex.Message
    
    and  config =
      {
        socket : Socket
        user   : string
        ip     : string
        port   : uint16
      }
    
    let private takeOwnershipSocket =
      (* Remark:
      
         The NetworkStream is created with read/write access to the specified
         Socket. If the value of ownsSocket parameter is true, the NetworkStream
         takes ownership of the underlying Socket, and calling the Close method
         also closes the underlying Socket.
         
         https://msdn.microsoft.com/
                 en-us/library/te7e60bx(v=vs.110).aspx#Anchor_2
      *)
      false (* Taking ownership will dispose main socket *)
    
    let readAsync : NetworkStream -> int -> byte array Async =
      fun stream count ->
        async {
          let buffer  = Array.create count 0uy
          let! length = stream.AsyncRead (buffer,0,count)
          
          if length = 0 then
            return [| |]
          else
            return buffer.[..length-1]
        }
    
    let readUntil : NetworkStream -> (byte -> bool) -> byte array Async =
      fun stream f ->
        let rec aux acc =
          async {
            let! bs = readAsync stream 0x01
            if Array.isEmpty bs || (f bs.[0]) then
              return List.Lazy.toArray (acc << List.Lazy.cons bs.[0])
            else
              return! aux (acc << List.Lazy.cons bs.[0])
          }
        aux List.Lazy.nil
    
    let writeAsync : NetworkStream -> byte array -> unit Async =
      fun stream bytes ->
        async { return! stream.AsyncWrite bytes } 
    
    let private asyncAccept : Socket -> Socket Async =
      fun socket ->
        Async.FromBeginEnd(socket.BeginAccept, socket.EndAccept)
    
    let stream : Socket -> NetworkStream =
      fun socket ->
        new NetworkStream (socket,takeOwnershipSocket)
    
    let ts () =
      System.DateTime.Now.ToString("o")
    
    let log : communication -> string -> unit =
      fun com str ->
        let compad = (string com).PadRight(8,' ')
        printfn "%s | %s | %s" (ts ()) compad str
    
    let socket () =
      new Socket(
        AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp
      )
  
    let init : Socket -> string -> string -> int -> client =
      fun socket username ipaddress port ->
        socket.Connect <| IPEndPoint (IPAddress.Parse ipaddress, port)
        Config
          { socket = socket
            user   = username
            ip     = ipaddress
            port   = uint16 port
          }
    
    let legacyServerInfo : client -> string Async =
      fun (Config { socket = socket } as client) ->
        async {
          use stream = stream socket
          
          (* http://wiki.vg/Protocol#Legacy_Server_List_Ping *)
          do! writeAsync stream [| 0xFEuy; 0x01uy |]
          
          (* http://wiki.vg/Server_List_Ping#Server_to_client *)
          let! pid = readAsync stream 0x01       (* 0xFF kick-package prefix  *)
          let! len = readAsync stream 0x02       (* Two-byte big endian short *)
          
          let (Protocol.PM.Short length) =
            Protocol.Utils.Bytes.BigEndian.toShort len   (* UTF-16BE char length  *)
          let len'   = length |> int
          let! bytes = readAsync stream (2 * len')   (* UTF-16BE 2 bytes/char *)
          
          return
            Encoding
              .BigEndianUnicode
              .GetString(bytes)
              .Replace("\u0000", " :: ")
        }
    
    let serverListPing : client -> string Async =
      fun (Config { socket = s; ip = ip; port = port } as client) ->
        async {
          use stream = stream s
          
          (* http://wiki.vg/Server_List_Ping#Handshake *)
          let handshake =
            Protocol.packet 0x00
              [ "Protocol Version" => Protocol.varInt Protocol.version
              ; "Server Address"   => Protocol.string ip
              ; "Server Port"      => Protocol.ushort port
              ; "Next State"       => Protocol.varInt 0x01
              ]
          
          log Sending "handshake"
          
          do! Protocol.send (writeAsync stream) handshake
          
          (* http://wiki.vg/Server_List_Ping#Request *)
          let request = 
            Protocol.packet 0x00 []
          
          log Sending "follow-up request"
          
          do! Protocol.send (writeAsync stream) request
          
          (* http://wiki.vg/Server_List_Ping#Response *)
          let! response =
            Protocol.receive (readUntil stream) (readAsync stream)
          
          let json =
            response
            |> Protocol.pkgData
            |> Protocol.Utils.Bytes.toString
          
          log Received (sprintf "response: %A" json)
          
          (* http://wiki.vg/Server_List_Ping#Ping *)
          let timestamp = DateTime.Now.Ticks
          let ping      =
            Protocol.packet 0x01
              [ "Payload" => Protocol.long timestamp
              ]
          
          log Sending (sprintf "ping: %i" timestamp)
          
          do! Protocol.send (writeAsync stream) ping
          
          (* http://wiki.vg/Server_List_Ping#Pong *)
          let! pong =
            Protocol.receive (readUntil stream) (readAsync stream)
          let timestamp' =
            pong
            |> Protocol.pkgData
            |> Protocol.Utils.Bytes.BigEndian.toLong
          
          log Received (sprintf "pong: %A" timestamp')
          
          return string json
        }
    
    let loginUnauthenticated : client -> unit Async =
      fun (Config { socket = s; user = usr; ip = ip; port = port } as client) ->
        async {
          use stream = stream s
          
          (* http://wiki.vg/Protocol#Login *)
          
          (* 1. C -> S: Handshake with Next State set to 2 (login) *)
          let handshake =
            Protocol.packet 0x00
              [ "Protocol Version" => Protocol.varInt Protocol.version
              ; "Server Address"   => Protocol.string ip
              ; "Server Port"      => Protocol.ushort port
              ; "Next State"       => Protocol.varInt 0x02
              ]
          
          log Sending "handshake"
          
          do! Protocol.send (writeAsync stream) handshake
          
          (* 2. C -> S: Login Start *)
          let profileid = usr
          let username  =
            Protocol.packet 0x00
              [ "Name" => Protocol.string profileid
              ]
          
          log Sending (sprintf "login username: %s" profileid)
          
          do! Protocol.send (writeAsync stream) username
          
          (*
             For unauthenticated and localhost connections (either of the two
             conditions is enough for an unencrypted connection) there is no
             encryption. In that case Login Start is directly followed by Login
             Success.
          *)
          
          (* 3. S -> C: Encryption Request *)
          (* 4. Client auth *)
          (* 5. C -> S: Encryption Response *)
          (* 6. Server auth, both enable encryption *)
          (* 7. S -> C: Set Compression (optional) *)
          
          (* 8. S -> C: Login Success *)
          let! login =
            Protocol.receive (readUntil stream) (readAsync stream)
          let lpid =
            login
            |> Protocol.pkgId
          
          let uuid =
            login
            |> Protocol.pkgData
            |> Protocol.Utils.Bytes.toString
          
          if lpid = 0x02 then
            log Received (sprintf "login success pid: 0x%02X" lpid)
            log Received (sprintf "login success uuid: %A"    uuid)
          else
            log Received (sprintf "login success pgk not recieved")
          
          (* http://wiki.vg/Protocol_FAQ *)
          
          (* 9. S -> C: Join Game *)
          let! joinGame =
            Protocol.receive (readUntil stream) (readAsync stream)
          let jgpid =
            joinGame
            |> Protocol.pkgId
          
          if jgpid = 0x23 then
            log Received (sprintf "join game pid: 0x%02X" jgpid)
          else
            log Received (sprintf "join game pgk not recieved")
          
          (* 10. S -> C: Plugin Message:
          
          MC|Brand with the server's brand (Optional) *)
          let! pluginMsg =
            Protocol.receive (readUntil stream) (readAsync stream)
          let pmpid =
            pluginMsg
            |> Protocol.pkgId
          let cpname =
            pluginMsg
            |> Protocol.pkgData
            |> Protocol.Utils.Bytes.toString
          
          if pmpid = 0x18 then
            log Received (sprintf "plugin message pid: 0x%02X" pmpid)
            log Received (sprintf "plugin message channel name: %A" cpname)
          else
            log Received (sprintf "plugin message pgk not recieved")
          
          (* 11. S -> C: Server Difficulty (Optional) *)
          let! serverDifficulty =
            Protocol.receive (readUntil stream) (readAsync stream)
          let sdpid =
            serverDifficulty
            |> Protocol.pkgId
          
          if sdpid = 0x0D then
            log Received (sprintf "server difficulty pid: 0x%02X" sdpid)
          else
            log Received (sprintf "server difficulty pgk not recieved")
          
          (* 12. S -> C: Spawn Position
          
          (“home” spawn, not where the client will spawn on login) *)
          
          (* 13. S -> C: Player Abilities *)
          let! playerAbilities =
            Protocol.receive (readUntil stream) (readAsync stream)
          let papid =
            playerAbilities
            |> Protocol.pkgId
          
          if papid = 0x2C then
            log Received (sprintf "player abilities pid: 0x%02X" papid)
          else
            log Received (sprintf "player abilities pgk not recieved")
          
          (* 14. C -> S: Plugin Message:
          
          MC|Brand with the client's brand (Optional) *)
          let clientBrand =
            Protocol.packet 0x09
              [ "Channel" => Protocol.string "MC|Brand"
              ; "Data"    => Protocol.byteArray [|   |] (* TODO: Add name *)
              ]
          
          log Sending "client's brand"
          
          do! Protocol.send (writeAsync stream) clientBrand
          
          (* 15. C -> S: Client Settings *)
          let clientSettings =
            Protocol.packet 0x04
              [ "Locale"               => Protocol.string "en_GB"
              (* https://minecraft.gamepedia.com/Server.properties *)
              ; "View Distance"        => Protocol.byte   0x02y  (* 2: minimum *)
              ; "Chat Mode"            => Protocol.varInt 0x00   (* 0: enabled *)
              ; "Chat Colors"          => Protocol.bool   false
              ; "Displayed Skin Parts" => Protocol.ubyte  0x00uy (* none       *)
              ; "Main Hand"            => Protocol.varInt 0x00   (* 0: left    *)
              ]
          
          log Sending "client settings"
          
          do! Protocol.send (writeAsync stream) clientSettings
          
          (* 16. S -> C: Player Position And Look
          
          (Required, tells the client they're ready to spawn) *)
          
          (* 17. C -> S: Teleport Confirm *)
          
          (* 18. C -> S: Player Position And Look
          
          (to confirm the spawn position) *)
          
          (* 19. C -> S: Client Status
          
          (sent either before or while receiving chunks, further testing needed,
          server handles correctly if not sent) *)
          
          (* 20. S -> C: inventory, Chunk Data, entities, etc *)
          
          return ()
        }
    
    let rec keepalive : client -> Protocol.Packet list -> unit Async =
      fun (Config { socket = socket } as client) packets ->
        async {
          use stream = stream socket
          
          let! received =
            Protocol.receive (readUntil stream) (readAsync stream)
          let pid =
            received
            |> Protocol.pkgId
          
          (* Do stuff *)
          let pkt, packets' =
            match packets with
              | [     ] -> None, [  ]
              | x :: xs -> Some x, xs
          
          match pkt with
            | None   -> ()
            | Some x -> do! Protocol.send (writeAsync stream) x
          
          (* http://wiki.vg/Protocol#Keep_Alive_.28clientbound.29 *)
          if pid = 0x1F then
            let randomID =
              received
              |> Protocol.pkgData
              |> Protocol.Utils.Bytes.BigEndian.toLong
            let (Protocol.PM.Long randomID') = randomID
            
            log Received (sprintf "ping: %A" randomID')
            
            (* http://wiki.vg/Protocol#Keep_Alive_.28serverbound.29 *)
            let keptalive =
              Protocol.packet 0x0B
                [ "Keep Alive ID" => Protocol.DataType.Long randomID
                ]
            
            do! Protocol.send (writeAsync stream) keptalive
            
            log Sending (sprintf "pong: %A" randomID')
          
          (* http://wiki.vg/Protocol#Disconnect_.28play.29 *)
          if pid = 0x1A then
            log Received (sprintf "disconnect by server pid: 0x%02X" pid)
            return ()
          else
            return! keepalive client packets'
        }
    
    let build : client -> Protocol.Packet list -> unit Async =
      fun client pkgs ->
        async {
          do!     loginUnauthenticated client
          
          return! keepalive client pkgs
        }
