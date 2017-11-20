#/bin/sh
cd $1
java -Xmx1024M -Xms1024M -jar ./minecraft_$1.jar
exit
