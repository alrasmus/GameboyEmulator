using System;

namespace GameboyEmulator;

public class CPU
{
    private readonly MemoryBus _bus;
    public ushort PC { get; private set; } // Program Counter
    public byte A { get; private set; } // Accumulator register
    
    // Registers B, C, D, E, H, L
    public byte B { get; private set; }
    public byte C { get; private set; }
    public byte D { get; private set; }
    public byte E { get; private set; }
    public byte H { get; private set; }
    public byte L { get; private set; }

    // 16-bit register pairs
    public ushort BC => (ushort)((B << 8) | C);
    public ushort DE => (ushort)((D << 8) | E);
    public ushort HL => (ushort)((H << 8) | L);

    public CPU(MemoryBus bus)
    {
        _bus = bus;
        PC = 0xC000; // Entry point for GB roms
        A = 0; // Initialize accumulator
    }

    public byte FetchByte()
    {
        // Fetches next byte from memory at current PC and increments PC
        byte value = _bus.ReadByte(PC);
        PC++;
        return value;
    }
    public void Step()
    {
        // Fetch next instruction byte and execute it
        byte instruction = FetchByte();

        // Switch statement to handle different instructions (aka opcodes)
        switch (instruction)
        {
            case 0x00: // NOP (No Operation)
                // do nothing
                break;
            case 0x01: // LD BC, d16
                C = FetchByte();
                B = FetchByte();
                break;
            case 0x06: // LD B, d8
                B = FetchByte();
                break;
            case 0x0E: // LD C, d8
                C = FetchByte();
                break;
            case 0x11: // LD DE, d16
                E = FetchByte();
                D = FetchByte();
                break;
            case 0x16: // LD D, d8
                D = FetchByte();
                break;
            case 0x1E: // LD E, d8
                E = FetchByte();
                break;
            case 0x21: // LD HL, d16
                L = FetchByte();
                H = FetchByte();
                break;
            case 0x26: // LD H, d8
                H = FetchByte();
                break;
            case 0x2E: // LD L, d8
                L = FetchByte();
                break;
            case 0x3E: // LD A, d8 
                A = FetchByte();
                break;
            case 0x77: // LD (HL), A
                _bus.WriteByte(HL, A);
                break;
            case 0x7E: // LD A, (HL)
                A = _bus.ReadByte(HL);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{instruction:X2} not implemented.");
        }
    }
}