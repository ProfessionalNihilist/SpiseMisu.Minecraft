# SpiseMisu.Minecraft

## Background

### How do we make code fun and more intuitive?

This year I became a volunteer at [Coding Pirates][codingpirates], a Danish
non-profit organization that tries to help kids to understand technology so they
can create and not just consume.

There are a few challenges that you are going to meet when you try to teach kids
to code. Mostly mathematical/logical concepts but also language based (English
keywords).

[Scratch][scratch], developed by `MIT`, seems to solve these issues but tends to
become boring in the long run. I use to say that coding in `Scratch` is a bit
like reading a "picture book".

We seem to face a challenge when we want to transition the children from reading
"picture books" to "read books", as many of the technologies used by us grown
ups, aren't that user-friendly as we think they are.

### Outcome

Therefore, I wanted to create something that was easy-to-use, but mainly `safe`
in the sense that it's very difficult to write some erroneous code, just like
[Scratch][scratch]:

![](./assets/pics/minecraft_coordinates_white.png?raw=true)

```fsharp
#load "../SpiseMisu.Minecraft.fs"

open SpiseMisu
open SpiseMisu.MineCraft

module Safe =

  ...
  
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
```

### Talk (Slides + Video)

I gave a talk on the subject at Øredev 2017:
- Slides: [slides]
- Video: [video]

[codingpirates]: https://codingpirates.dk/
[scratch]: https://scratch.mit.edu/
[slides]: http://blog.stermon.com/assets/talks/2017-11-06_OEredevDeveloperConference.pdf
[video]: http://oredev.org/2017/sessions/how-do-we-make-code-fun-and-more-intuitive

## Usage

You can clone the project or just download the `raw` version of
`SpiseMisu.Minecraft.fs` file from this project. Ideally you should set up a
dependency to this file, usually with a tool like `paket`:

* [Referencing a single file][paket]

[paket]: https://fsprojects.github.io/Paket/github-dependencies.html#Referencing-a-single-file


## Examples

Please visit the `examples` folder in order to view a few scripts that uses this
library in order to get some information from a `Minecraft` server as well as
some that builds a skyscraper and colors it.


## Limitations

As it is right now, I have just added the `tp` (teleport) and the `fill`
`Minecraft` commands to the easy-to-write `DSL`. It should be possible though to
expand the `DSL` with all the other `Minecraft` commands without breaking
backward compatibility, as the `DSL` would just need to add new
`CustomOperation`s, only additive changes, and therefore complying with
`syntactic versioning`.

Also, it's not possible to access any `Minecraft` server as I have only
implemented login without authentication, see source function:
`loginUnauthenticated`. Therefore, this client will only connect to servers that
aren't running in `online-mode` (`online-mode=false` in `server.properties`).

Also in order to execute `Minecraft` commands, the users must be `OP` on the
server. That's very unlikely to be the default setting for `online-mode`
servers.

But at some point, I will give authentication a try with an implementation by
using something like: [The Legion of the Bouncy Castle][bouncycastle].

> Note: Also, compression is not implemented as well. Therefore, it must be
> disabled (`network-compression-threshold=-1` in `server.properties`) as well.

[bouncycastle]: http://www.bouncycastle.org/csharp/


## Future work

I leave the road-map open as my only intention was to showcase how easy it is to
implement low-level protocols and abstract it to an easy to understand interface
while still being able to make something useful.

I'm hoping though, that some of the older `kids` from `Coding Pirates`, might
expand it with some cool stuff, instead of just using it as it is. Our motto:
`understand technology to create and not just consume`.

But, as a guy who spends must of his time in a `terminal`, I'm not going to
exclude implementing a fully functional `Minecraft` client that could be
executed from a `head-less` server.


## Licensing

In order to ensure that the rights of the end-users are preserved as thought
initially by this project, we have come to the conclusion that correct and only
form of licensing that support this, is [copyleft][copyleft]. Therefore source
code and media content are released under **GPL-3.0** and **CC BY-SA**,
respectively:

* [GPL-3.0][gpl3]
* [CC BY-SA 4.0][ccbysa4]

[copyleft]: https://copyleft.org/
[gpl3]:     https://www.gnu.org/licenses/gpl.html
[ccbysa4]:  https://creativecommons.org/licenses/by-sa/4.0/
