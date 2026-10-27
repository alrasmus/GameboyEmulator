using System;
using GameboyEmulator;
// Load a real ROM file.
var cartridge = new Cartridge(args[0]);

var memory = new Memory();
var bus = new MemoryBus(cartridge, memory);

// Test a ROM read.
byte romByte = bus.ReadByte(0x0100);
Console.WriteLine($"ROM byte at 0x0100: 0x{romByte:X2}");

// Test a RAM write and read.
bus.WriteByte(0xC000, 42);
byte ramByte = bus.ReadByte(0xC000);
Console.WriteLine($"RAM byte at 0xC000: {ramByte}");