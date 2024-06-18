
protoc.exe -I=./ --csharp_out=./ ./Protocol.proto

IF ERRORLEVEL 1 PAUSE	


START ../PacketGenerator/bin/Debug/net8.0/PacketGenerator.exe ../Protocol/Protocol.proto
XCOPY /Y Protocol.cs "D:\Projects\BOMBERCHESS\BomberChess_Client\Assets\01.Scripts\Protocol\Generated"
XCOPY /Y Protocol.cs "../GameServer/Protocol/Generated"
XCOPY /Y ClientPacketManager.cs "D:\Projects\BOMBERCHESS\BomberChess_Client\Assets\01.Scripts\Protocol"
XCOPY /Y ServerPacketManager.cs "../GameServer/Protocol"


