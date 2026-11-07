# Game Boy Emulator
## Current Progress

This project is a Nintendo Game Boy emulator written in C# using .NET. It is being developed incrementally as a learning project, with a focus on understanding the Game Boy's hardware and CPU instruction set.

### Implemented Features

- **Cartridge:** Loading ROM files and reading cartridge data.
- **Memory:** Basic 64 KB memory implementation.
- **Memory Bus:** Handles memory reads and writes, including basic ROM read-only behavior.
- **CPU:**
  - 8-bit and 16-bit register operations.
  - Register-to-register and immediate load instructions.
  - Arithmetic and logical instructions.
  - Increment and decrement instructions.
  - Flag operations, including DAA, CPL, SCF, and CCF.
  - Accumulator rotate instructions.
  - CB-prefixed rotate and shift instructions (`0x00–0x1F`).

### Testing

The project uses **xUnit** for automated unit testing. Tests are written alongside CPU instruction implementations to verify register values, memory operations, and flag behavior.

### Future Development

The emulator is still a work in progress. Planned features include the remaining CB-prefixed instructions, additional CPU instructions, and eventually the other Game Boy hardware components.