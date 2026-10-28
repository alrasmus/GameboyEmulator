using GameboyEmulator;

namespace GameboyEmulator.Tests;

public class CpuTests
{
    private static (CPU cpu, MemoryBus bus, Memory memory) CreateSystem(byte[] program)
    {
        var cartridge = new Cartridge(new byte[0x8000]); // Create a dummy cartridge with 32KB of empty ROM
        var memory = new Memory();

        // load test program into working memory at 0xC000
        for (int i = 0; i < program.Length; i++)
        {
            memory.WriteByte((ushort)(0xC000 + i), program[i]);
        }

        // create memory bus and CPU
        var bus = new MemoryBus(cartridge, memory);
        var cpu = new CPU(bus);

        return (cpu, bus, memory);
    }

    [Fact]
    public void LdAImmediateAndAdvancePC()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x3E, 0x42}); // LD A, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.A);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }

    [Fact]
    public void CpuCanWriteToAndReadFromMemoryThroughHL()
    {
        // create system and get CPU and MemoryBus
        var (cpu, bus, _) = CreateSystem(new byte[]
        {
            0x21, 0x00, 0xC1, // LD HL, 0xC100
            0x3E, 0x42,       // LD A, 0x42
            0x77,             // LD (HL), A
            0x3E, 0x00,       // LD A, 0x00
            0x7E              // LD A, (HL)
        });

        // Execute instructions
        for (int i = 0; i < 5; i++)
        {
            cpu.Step();
        }

        Assert.Equal((byte)0x42, cpu.A);
        Assert.Equal((byte)0x42, bus.ReadByte(0xC100));
        Assert.Equal((ushort)0xC009, cpu.PC);
    }


    [Fact]
    public void UnsupportedOpcodeThrowsException()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(
            new byte[] { 0xD3 });

        // Execute instruction and assert that NotImplementedException is thrown
        var exception = Assert.Throws<NotImplementedException>(
            () => cpu.Step());

        // Assert the exception message correctly indicates the unsupported opcode
        Assert.Contains("0xD3", exception.Message);
    }

}
