using System;
using GameboyEmulator;

var memory = new Memory();

memory.WriteByte(0xC000, 0xFF);
var value = memory.ReadByte(0xC000);

Console.WriteLine($"Value at address 0xC000: {value}");

memory.WriteByte(0xC000, 0x1F);
value = memory.ReadByte(0xC000);

Console.WriteLine($"Value at address 0xC000: {value}");