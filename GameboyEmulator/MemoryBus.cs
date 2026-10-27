using System;

namespace GameboyEmulator;
// MemoryBus class handles the interaction between cartridge and memory, providing a single interface for reading and writing bytes to the appropriate location
public class MemoryBus
{
    private readonly Cartridge _cartridge;
    private readonly Memory _memory;

    public MemoryBus(Cartridge cartridge, Memory memory)
    {
        _cartridge = cartridge;
        _memory = memory;
    }

    public byte ReadByte(ushort address)
    {
        // If address is in ROM range (0x0000 to 0x7FFF), read from cartridge
        if (address <= 0x7FFF)
        {
            return _cartridge.ReadByte(address);
        }
        // else if address is in working RAM range (0xC000 to 0xDFFF), read from memory
        else if (address >= 0xC000 && address <= 0xDFFF)
        {
            return _memory.ReadByte(address);
        }
        else
        {
            return _memory.ReadByte(address); // temporary fallback for other addresses
        }
    }
    public void WriteByte(ushort address, byte value)
    {
        // If address is in ROM just return
        if (address <= 0x7FFF)
        {
            return; //ROM is read-only
        }
        _memory.WriteByte(address, value);
    }
}