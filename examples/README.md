# SpiseMisu.Minecraft Examples

## Requirements

### Download Minecraft Server

As mentioned on the main `README.md` file, it's not possible to connect to
`Minecraft` servers running in `online-mode`. Therefore, you must first
download: `minecraft_server.1.12.2.jar`, the free `Minecraft Java Edition
Server` from:

```

https://minecraft.net/en-us/download/server

```

> Note: We only support version `340`.

### Setup Minecraft Server

Once again, as mentioned on the main `README.md`, authentication and
compression must be disabled. Also, the EULA (`eula=true` in `eula.txt`) must be
accepted.

- server.properties
```
#Minecraft server properties
#Tue Oct 10 11:17:09 CEST 2017
...
network-compression-threshold=-1
...
online-mode=false
...
```

- eula.txt
```
#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).
#Tue May 23 16:51:26 CEST 2017
eula=true
```

### Startup Minecraft Server

I use the following scripts in order to have several versions of `Minecraft` servers to test:

- start_server.sh
```sh
#/bin/sh
cd $1
java -Xmx1024M -Xms1024M -jar ./minecraft_$1.jar nogui
exit
```

- start_server_gui.sh
```sh
#/bin/sh
cd $1
java -Xmx1024M -Xms1024M -jar ./minecraft_$1.jar
exit
```

used like this: `./start_server.sh server.1.12.2` or `./start_server_gui.sh server.1.12.2`


## Info Scripts

### SpiseMisu.Minecraft.Info.LegacyServerInfo.fsx

- client:
```text
$ ./SpiseMisu.Minecraft.Info.LegacyServerInfo.fsx 
Legacy server info: §1 :: 127 :: 1.12.2 :: A Minecraft Server :: 0 :: 20
Closed connection.
```

- server:
```text
```

### SpiseMisu.Minecraft.Info.ServerListPing.fsx

- client:
```text
$ ./SpiseMisu.Minecraft.Info.ServerListPing.fsx 
2017-11-20T19:52:33.4638490+01:00 | Sending  | handshake
2017-11-20T19:52:33.4809840+01:00 | Sending  | follow-up request
2017-11-20T19:52:33.4995260+01:00 | Received | response: {"description":{"text":"A Minecraft Server"},"players":{"max":20,"online":0},"version":{"name":"1.12.2","protocol":340}}
2017-11-20T19:52:33.5017120+01:00 | Sending  | ping: 636468043534995990
2017-11-20T19:52:33.5477980+01:00 | Received | pong: 636468043534995990
Server info: {"description":{"text":"A Minecraft Server"},"players":{"max":20,"online":0},"version":{"name":"1.12.2","protocol":340}}
Closed connection.
```

## Building Scripts

![](../assets/screenshots/mc_skyscraper_00.png?raw=true)

![](../assets/screenshots/mc_skyscraper_01.png?raw=true)

![](../assets/screenshots/mc_skyscraper_02.png?raw=true)

### SpiseMisu.Minecraft.Skyscraper.Unsafe.fsx

- client:
```text
$ ./SpiseMisu.Minecraft.Skyscraper.Unsafe.fsx 
2017-11-20T19:51:30.2795930+01:00 | Sending  | handshake
2017-11-20T19:51:30.3028040+01:00 | Sending  | login username: BobTheBuilder
2017-11-20T19:51:30.3734950+01:00 | Received | login success pid: 0x02
2017-11-20T19:51:30.3810420+01:00 | Received | login success uuid: a5c7ab96-2812-31e2-aa0e-54b732bad409
2017-11-20T19:51:30.4051640+01:00 | Received | join game pid: 0x23
2017-11-20T19:51:30.4090290+01:00 | Received | plugin message pid: 0x18
2017-11-20T19:51:30.4097750+01:00 | Received | plugin message channel name: MC|Brand
2017-11-20T19:51:30.4119070+01:00 | Received | server difficulty pid: 0x0D
2017-11-20T19:51:30.4137570+01:00 | Received | player abilities pid: 0x2C
2017-11-20T19:51:30.4147060+01:00 | Sending  | client's brand
2017-11-20T19:51:30.4167950+01:00 | Sending  | client settings
2017-11-20T19:51:30.6917460+01:00 | Received | ping: 6370329L
2017-11-20T19:51:30.6935410+01:00 | Sending  | pong: 6370329L
```

- server:
```text
[19:51:30] [Server thread/INFO]: BobTheBuilder[/127.0.0.1:57922] logged in with entity id 15 at (0.5, 29.0, 0.5)
[19:51:30] [Server thread/INFO]: BobTheBuilder joined the game
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 4.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 4.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 4.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 4.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 4.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 9.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 9.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 9.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 9.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 14.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 14.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 14.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 14.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 19.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 19.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 19.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 19.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 24.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 24.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 24.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 24.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 49.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 29.0, 1.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 29.0, 0.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 29.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 50.5]
[19:51:30] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 0.5]
[19:51:33] [Server thread/INFO]: BobTheBuilder lost connection: Disconnected
[19:51:33] [Server thread/INFO]: BobTheBuilder left the game
```

### SpiseMisu.Minecraft.Skyscraper.Safe.fsx

- client:
```text
$ ./SpiseMisu.Minecraft.Skyscraper.Safe.fsx 
2017-11-20T19:49:42.8110490+01:00 | Sending  | handshake
2017-11-20T19:49:42.8278240+01:00 | Sending  | login username: BobTheBuilder
2017-11-20T19:49:42.9192710+01:00 | Received | login success pid: 0x02
2017-11-20T19:49:42.9262640+01:00 | Received | login success uuid: a5c7ab96-2812-31e2-aa0e-54b732bad409
2017-11-20T19:49:42.9528040+01:00 | Received | join game pid: 0x23
2017-11-20T19:49:42.9551680+01:00 | Received | plugin message pid: 0x18
2017-11-20T19:49:42.9562890+01:00 | Received | plugin message channel name: MC|Brand
2017-11-20T19:49:42.9588330+01:00 | Received | server difficulty pid: 0x0D
2017-11-20T19:49:42.9630540+01:00 | Received | player abilities pid: 0x2C
2017-11-20T19:49:42.9641120+01:00 | Sending  | client's brand
2017-11-20T19:49:42.9665090+01:00 | Sending  | client settings
2017-11-20T19:49:43.3232350+01:00 | Received | ping: 6262940L
2017-11-20T19:49:43.3266450+01:00 | Sending  | pong: 6262940L
```

- server:
```text
[19:49:42] [Server thread/INFO]: BobTheBuilder[/127.0.0.1:57912] logged in with entity id 4 at (0.5, 29.0, 0.5)
[19:49:42] [Server thread/INFO]: BobTheBuilder joined the game
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 4.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 2601 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 4.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 4.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 4.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 4.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 9.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 9.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 9.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 9.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 14.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 14.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 14.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 14.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 19.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 19.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 19.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 19.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 24.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 24.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 24.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 24.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 49.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 29.0, 1.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 29.0, 0.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 29.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 50.5]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:49:43] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 0.5]
[19:49:47] [Server thread/INFO]: BobTheBuilder lost connection: Disconnected
[19:49:47] [Server thread/INFO]: BobTheBuilder left the game
```

### SpiseMisu.Minecraft.Skyscraper.Safe.Colors.fsx

- client:
```text
$ ./SpiseMisu.Minecraft.Skyscraper.Safe.Colors.fsx 
2017-11-20T19:45:51.0584220+01:00 | Sending  | handshake
2017-11-20T19:45:51.0825390+01:00 | Sending  | login username: BobTheBuilder
2017-11-20T19:45:51.1722520+01:00 | Received | login success pid: 0x02
2017-11-20T19:45:51.1796540+01:00 | Received | login success uuid: a5c7ab96-2812-31e2-aa0e-54b732bad409
2017-11-20T19:45:51.2029040+01:00 | Received | join game pid: 0x23
2017-11-20T19:45:51.2042940+01:00 | Received | plugin message pid: 0x18
2017-11-20T19:45:51.2047020+01:00 | Received | plugin message channel name: MC|Brand
2017-11-20T19:45:51.2076390+01:00 | Received | server difficulty pid: 0x0D
2017-11-20T19:45:51.2110260+01:00 | Received | player abilities pid: 0x2C
2017-11-20T19:45:51.2119130+01:00 | Sending  | client's brand
2017-11-20T19:45:51.2135910+01:00 | Sending  | client settings
2017-11-20T19:45:51.7654290+01:00 | Received | ping: 6031261L
2017-11-20T19:45:51.7816070+01:00 | Sending  | pong: 6031261L
2017-11-20T19:46:06.6095070+01:00 | Received | ping: 6046284L
2017-11-20T19:46:06.6104350+01:00 | Sending  | pong: 6046284L
```

- server:
```text
[19:45:51] [Server thread/INFO]: BobTheBuilder[/127.0.0.1:57906] logged in with entity id 4 at (0.5, 129.0, 0.5)
[19:45:51] [Server thread/INFO]: BobTheBuilder joined the game
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 4.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 2601 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 4.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 4.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 4.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 4.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 9.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 9.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 9.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 9.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 9.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 9.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 14.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 14.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 14.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 14.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 14.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 14.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 19.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 19.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 19.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 19.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 19.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 19.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 24.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 24.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 24.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 24.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 49.5, 24.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 24.0, 49.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 5 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 1.5, 29.0, 1.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 2597 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 204 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 29.0, 0.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 50.5, 29.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 200 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 50.5]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: 196 blocks filled]
[19:45:51] [Server thread/INFO]: [BobTheBuilder: Teleported BobTheBuilder to 0.5, 29.0, 0.5]
[19:46:11] [Server thread/INFO]: BobTheBuilder lost connection: Disconnected
[19:46:11] [Server thread/INFO]: BobTheBuilder left the game
```
