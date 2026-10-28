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

    public static IEnumerable<object[]> RegisterLoadOpcodes()
    {
        for (int opcode = 0x40; opcode <= 0x7F; opcode++)
        {
            if (opcode != 0x76)
            {
                yield return new object[] { (byte)opcode };
            }
        }
    }

    [Theory]
    [MemberData(nameof(RegisterLoadOpcodes))]
    public void RegisterToRegisterLdTests(byte opcode)
    {
        var program = new byte[]
        {
            0x06, 0x22, // LD B, 0x22
            0x0E, 0x33, // LD C, 0x33
            0x16, 0x44, // LD D, 0x44
            0x1E, 0x55, // LD E, 0x55
            0x26, 0xC1, // LD H, 0xC1
            0x2E, 0x00, // LD L, 0x00
            0x3E, 0x11, // LD A, 0x11
            opcode      // Instruction being tested
        };

        var (cpu, bus, _) = CreateSystem(program);

        // Give address 0xC100 a known value.
        bus.WriteByte(0xC100, 0x66);

        // Execute the seven setup instructions.
        for (int i = 0; i < 7; i++)
        {
            cpu.Step();
        }

        // Decode the source and destination registers.
        int destination = (opcode >> 3) & 0x07;
        int source = opcode & 0x07;

        // Capture the source value before executing the instruction.
        byte expected = source switch
        {
            0 => cpu.B,
            1 => cpu.C,
            2 => cpu.D,
            3 => cpu.E,
            4 => cpu.H,
            5 => cpu.L,
            6 => bus.ReadByte(cpu.HL),
            7 => cpu.A,
            _ => throw new InvalidOperationException()
        };

        cpu.Step();

        // Verify that the destination received the source value.
        switch (destination)
        {
            case 0:
                Assert.Equal(expected, cpu.B);
                break;
            case 1:
                Assert.Equal(expected, cpu.C);
                break;
            case 2:
                Assert.Equal(expected, cpu.D);
                break;
            case 3:
                Assert.Equal(expected, cpu.E);
                break;
            case 4:
                Assert.Equal(expected, cpu.H);
                break;
            case 5:
                Assert.Equal(expected, cpu.L);
                break;
            case 6:
                Assert.Equal(expected, bus.ReadByte(0xC100));
                break;
            case 7:
                Assert.Equal(expected, cpu.A);
                break;
        }

        Assert.Equal((ushort)0xC00F, cpu.PC);
    }
    [Fact]
    public void LdAImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x3E, 0x42}); // LD A, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.A);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }
    [Fact]
    public void LdBImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x06, 0x42}); // LD B, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.B);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }
    [Fact]
    public void LdCImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x0E, 0x42}); // LD C, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.C);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }
    [Fact]
    public void LdDImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x16, 0x42}); // LD D, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.D);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }
    [Fact]
    public void LdEImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x1E, 0x42}); // LD E, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.E);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }
    [Fact]
    public void LdHImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x26, 0x42}); // LD H, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.H);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }
    [Fact]
    public void LdLImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x2E, 0x42}); // LD L, 0x42

        cpu.Step(); // Execute instruction

        Assert.Equal((byte)0x42, cpu.L);
        Assert.Equal((ushort)0xC002, cpu.PC);
    }
    [Fact]
    public void LdBCImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x01, 0x34, 0x12}); // LD BC, 0x1234

        cpu.Step(); // Execute instruction

        Assert.Equal((ushort)0x1234, cpu.BC);
        Assert.Equal((ushort)0xC003, cpu.PC);
    }
    [Fact]
    public void LdDEImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x11, 0x34, 0x12}); // LD DE, 0x1234

        cpu.Step(); // Execute instruction

        Assert.Equal((ushort)0x1234, cpu.DE);
        Assert.Equal((ushort)0xC003, cpu.PC);
    }
    [Fact]
    public void LdHLImmediateTest()
    {
        // create system and get CPU
        var (cpu, _, _) = CreateSystem(new byte[] {0x21, 0x34, 0x12}); // LD HL, 0x1234

        cpu.Step(); // Execute instruction

        Assert.Equal((ushort)0x1234, cpu.HL);
        Assert.Equal((ushort)0xC003, cpu.PC);
    }
    [Fact]
    public void WriteAndReadFromMemoryThroughHLTest()
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
    public void UnsupportedOpcodeTest()
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
