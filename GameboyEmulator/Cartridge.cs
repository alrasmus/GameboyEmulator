using System;
using System.Text;
using System.IO;

namespace GameboyEmulator;

// Cartridge class represents a Game Boy cartridge, containing game data and metadata. Built from rom
public class Cartridge
{
    // rom data loaded from file
    private readonly byte[] _romData;

    public string Title { get; }
    public byte CartridgeType { get; }
    public byte RomSizeCode { get; }
    public int FileSize => _romData.Length;


    // Constructor for initializig cartridge with Rom data from filepath
    public Cartridge(string filepath)
    {
        // Load the ROM data from file path
        _romData = File.ReadAllBytes(filepath);

        // Cartridge header is located from 0x0100 to 0x014F, if size is less than 0x0150, throw exception
        if (_romData.Length < 0x0150)
        {
            throw new InvalidDataException("ROM data is too small to contain a valid header.");
        }

        // Extract the title and other metadata from the header (0x0134 to 0x0143)
        Title = Encoding.ASCII.GetString(_romData, 0x0134, 16).TrimEnd('\0');
        CartridgeType = _romData[0x0147];
        RomSizeCode = _romData[0x0148];
    }
    public byte ReadByte(ushort address)
    {
        // Read a byte from ROM at specified address
        if (address >= _romData.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(address), "Address is outside of loaded ROM data.");
        }
        return _romData[address];
    }

}