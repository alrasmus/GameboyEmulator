using System;
using GameboyEmulator;

if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run -- <file path to ROM>");
    return;
}

try
{
    var cartridge = new Cartridge(args[0]);
    Console.WriteLine($"Loaded ROM: {cartridge.Title}");
    Console.WriteLine($"Loaded Cartridge Type: 0x{cartridge.CartridgeType:X2}");
    Console.WriteLine($"Loaded ROM Size Code: 0x{cartridge.RomSizeCode:X2}");
    Console.WriteLine($"Loaded ROM File Size: {cartridge.FileSize} bytes");
}
catch (InvalidDataException ex)
{
    Console.WriteLine($"Invalid ROM: {ex.Message}");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"File not found: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}