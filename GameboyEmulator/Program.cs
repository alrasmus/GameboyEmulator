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
    0x21, 0x00, 0xC0, // LD HL, 0xC000
    0x3E, 0x42,       // LD A, 0x42
    0x77,             // LD (HL), A
    0x3E, 0x00,       // LD A, 0x00
    0x7E              // LD A, (HL)
});

var cpu = new CPU(bus);

for (int i = 0; i < 5; i++)
{
    cpu.Step();
}

Console.WriteLine($"A = 0x{cpu.A:X2}");
Console.WriteLine($"HL = 0x{cpu.HL:X4}");
Console.WriteLine($"PC = 0x{cpu.PC:X4}");
Console.WriteLine($"Memory[0xC000] = {bus.ReadByte(0xC000)}");
