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
            case 0x40: // LD B, B
                B = B;
                break;
            case 0x41: // LD B, C
                B = C;
                break;
            case 0x42: // LD B, D
                B = D;
                break;
            case 0x43: // LD B, E
                B = E;
                break;
            case 0x44: // LD B, H
                B = H;
                break;
            case 0x45: // LD B, L
                B = L;
                break;
            case 0x46: // LD B, (HL)
                B = _bus.ReadByte(HL);
                break;
            case 0x47: // LD B, A
                B = A;
                break;
            case 0x48: // LD C, B
                C = B;
                break;
            case 0x49: // LD C, C
                C = C;
                break;
            case 0x4A: // LD C, D
                C = D;
                break;
            case 0x4B: // LD C, E
                C = E;
                break;
            case 0x4C: // LD C, H
                C = H;
                break;
            case 0x4D: // LD C, L
                C = L;
                break;
            case 0x4E: // LD C, (HL)
                C = _bus.ReadByte(HL);
                break;
            case 0x4F: // LD C, A
                C = A;
                break;
            case 0x50: // LD D, B
                D = B;
                break;
            case 0x51: // LD D, C
                D = C;
                break;
            case 0x52: // LD D, D
                D = D;
                break;
            case 0x53: // LD D, E
                D = E;
                break;
            case 0x54: // LD D, H
                D = H;
                break;
            case 0x55: // LD D, L
                D = L;
                break;
            case 0x56: // LD D, (HL)
                D = _bus.ReadByte(HL);
                break;
            case 0x57: // LD D, A
                D = A;
                break;
            case 0x58: // LD E, B
                E = B;
                break;
            case 0x59: // LD E, C
                E = C;
                break;
            case 0x5A: // LD E, D
                E = D;
                break;
            case 0x5B: // LD E, E
                E = E;
                break;
            case 0x5C: // LD E, H
                E = H;
                break;
            case 0x5D: // LD E, L
                E = L;
                break;
            case 0x5E: // LD E, (HL)
                E = _bus.ReadByte(HL);
                break;
            case 0x5F: // LD E, A
                E = A;
                break;
            case 0x60: // LD H, B
                H = B;
                break;
            case 0x61: // LD H, C
                H = C;
                break;
            case 0x62: // LD H, D
                H = D;
                break;
            case 0x63: // LD H, E
                H = E;
                break;
            case 0x64: // LD H, H
                H = H;
                break;
            case 0x65: // LD H, L
                H = L;
                break;
            case 0x66: // LD H, (HL)
                H = _bus.ReadByte(HL);
                break;
            case 0x67: // LD H, A
                H = A;
                break;
            case 0x68: // LD L, B
                L = B;
                break;
            case 0x69: // LD L, C
                L = C;
                break;
            case 0x6A: // LD L, D
                L = D;
                break;
            case 0x6B: // LD L, E
                L = E;
                break;
            case 0x6C: // LD L, H
                L = H;
                break;
            case 0x6D: // LD L, L
                L = L;
                break;
            case 0x6E: // LD L, (HL)
                L = _bus.ReadByte(HL);
                break;
            case 0x6F: // LD L, A
                L = A;
                break;
            case 0x70: // LD (HL), B
                _bus.WriteByte(HL, B);
                break;
            case 0x71: // LD (HL), C
                _bus.WriteByte(HL, C);
                break;
            case 0x72: // LD (HL), D
                _bus.WriteByte(HL, D);
                break;
            case 0x73: // LD (HL), E
                _bus.WriteByte(HL, E);
                break;
            case 0x74: // LD (HL), H
                _bus.WriteByte(HL, H);
                break;
            case 0x75: // LD (HL), L
                _bus.WriteByte(HL, L);
                break;
            case 0x77: // LD (HL), A
                _bus.WriteByte(HL, A);
                break;
            case 0x78: // LD A, B
                A = B;
                break;
            case 0x79: // LD A, C
                A = C;
                break;
            case 0x7A: // LD A, D
                A = D;
                break;
            case 0x7B: // LD A, E
                A = E;
                break;
            case 0x7C: // LD A, H
                A = H;
                break;
            case 0x7D: // LD A, L
                A = L;
                break;
            case 0x7E: // LD A, (HL)
                A = _bus.ReadByte(HL);
                break;
            case 0x7F: // LD A, A
                A = A;
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{instruction:X2} not implemented.");
        }
    }
}