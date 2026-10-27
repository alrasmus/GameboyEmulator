using System;


using GameboyEmulator;
var memory = new Memory();
var bus = new MemoryBus(
    new Cartridge(args[0]),
    memory);
// LD A, 0x42
// LD B, 0x12
// LD C, 0x34
bus.LoadTestBytes(0xC000, new byte[]
{
    0x3E, 0x42,
    0x06, 0x12,
    0x0E, 0x34
});

var cpu = new CPU(bus);

cpu.Step();
cpu.Step();
cpu.Step();

Console.WriteLine($"A = 0x{cpu.A:X2}");
Console.WriteLine($"B = 0x{cpu.B:X2}");
Console.WriteLine($"C = 0x{cpu.C:X2}");
Console.WriteLine($"PC = 0x{cpu.PC:X4}");