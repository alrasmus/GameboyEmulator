using System;

namespace GameboyEmulator;

public class Memory
{
    private readonly byte[] _memory = new byte[0x10000]; // 64KB memory space

    // methods to read and write bytes to memory
    public byte ReadByte(ushort address)
    {
        return _memory[address];
    }
    public void WriteByte(ushort address, byte value)
    {
        _memory[address] = value;
    }
}