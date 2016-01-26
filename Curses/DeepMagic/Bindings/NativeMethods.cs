namespace Deep.Magic
{
	using System;
	using System.Runtime.InteropServices;
	using System.Text;

	public class NativeMethods
	{
		public delegate bool ConsoleCtrlDelegate(CtrlTypes ctrlType);

		public enum CtrlTypes : uint
		{
			CtrlCEvent = 0,
			CtrlBreakEvent,
			CtrlCloseEvent,
			CtrlLogoffEvent = 5,
			CtrlShutdownEvent
		}

		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool AddConsoleAlias(
			string source,
			string target,
			string exeName);

		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool AllocConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool AttachConsole(
			uint dwProcessId);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr CreateConsoleScreenBuffer(
			uint dwDesiredAccess,
			uint dwShareMode, 
			IntPtr lpSecurityAttributes, 
			uint dwFlags,
			IntPtr lpScreenBufferData);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FillConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			ushort wAttribute, 
			uint nLength, 
			Coord dwWriteCoord, 
			out uint lpNumberOfAttrsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FillConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			char cCharacter,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfCharsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FlushConsoleInputBuffer(
			IntPtr hConsoleInput);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		internal static extern bool FreeConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GenerateConsoleCtrlEvent(
			uint dwCtrlEvent,
			uint dwProcessGroupId);

		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool GetConsoleAlias(
			string source,
			out StringBuilder targetBuffer,
			uint targetBufferLength,
			string exeName);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint GetConsoleAliases(
			StringBuilder[] lpTargetBuffer,
			uint targetBufferLength,
			string lpExeName);

		[DllImport("kernel32", SetLastError = true)]
		internal static extern uint GetConsoleAliasesLength(
			string exeName);

		[DllImport("kernel32", SetLastError = true)]
		internal static extern uint GetConsoleAliasExes(
			out StringBuilder exeNameBuffer,
			uint exeNameBufferLength);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint GetConsoleAliasExesLength();

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint GetConsoleCP();

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleCursorInfo(
			IntPtr hConsoleOutput,
			out ConsoleCursorInfo lpConsoleCursorInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleDisplayMode(
			out uint modeFlags);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern Coord GetConsoleFontSize(
			IntPtr hConsoleOutput,
			int nFont);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleHistoryInfo(
			out ConsoleHistoryInfo consoleHistoryInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleMode(
			IntPtr hConsoleHandle,
			out uint lpMode);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint GetConsoleOriginalTitle(
			out StringBuilder consoleTitle,
			uint size);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint GetConsoleOutputCP();

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint GetConsoleProcessList(
			out uint[] processList,
			uint processCount);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleScreenBufferInfo(
			IntPtr hConsoleOutput,
			out ConsoleScreenBufferInfo lpConsoleScreenBufferInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleScreenBufferInfoEx(
			IntPtr hConsoleOutput,
			ref ConsoleScreenBufferInfoEx consoleScreenBufferInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleSelectionInfo(
			ConsoleSelectionInfo consoleSelectionInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint GetConsoleTitle(
			[Out] StringBuilder lpConsoleTitle,
			uint nSize);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetCurrentConsoleFont(
			IntPtr hConsoleOutput,
			bool bMaximumWindow,
			out ConsoleFontInfo lpConsoleCurrentFont);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetCurrentConsoleFontEx(
			IntPtr consoleOutput,
			bool maximumWindow,
			out ConsoleFontInfoEx consoleCurrentFont);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern Coord GetLargestConsoleWindowSize(
			IntPtr hConsoleOutput);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetNumberOfConsoleInputEvents(
			IntPtr hConsoleInput,
			out uint lpcNumberOfEvents);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetNumberOfConsoleMouseButtons(
			ref uint lpNumberOfMouseButtons);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetStdHandle(
			int nStdHandle);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool PeekConsoleInput(
			IntPtr hConsoleInput,
			[Out] InputRecord[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReadConsole(
			IntPtr hConsoleInput,
			[Out] StringBuilder lpBuffer,
			uint nNumberOfCharsToRead,
			out uint lpNumberOfCharsRead,
			IntPtr lpReserved);

		[DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
		internal static extern bool ReadConsoleInput(
			IntPtr hConsoleInput,
			[Out] InputRecord[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReadConsoleOutput(
			IntPtr hConsoleOutput,
			[Out] CharInfo[] lpBuffer,
			Coord dwBufferSize,
			Coord dwBufferCoord,
			ref SmallRect lpReadRegion);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReadConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			[Out] ushort[] lpAttribute,
			uint nLength,
			Coord dwReadCoord,
			out uint lpNumberOfAttrsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReadConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			[Out] StringBuilder lpCharacter,
			uint nLength,
			Coord dwReadCoord,
			out uint lpNumberOfCharsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ScrollConsoleScreenBuffer(
			IntPtr hConsoleOutput,
			[In] ref SmallRect lpScrollRectangle,
			IntPtr lpClipRectangle,
			Coord dwDestinationOrigin,
			[In] ref CharInfo lpFill);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleActiveScreenBuffer(
			IntPtr hConsoleOutput);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCP(
			uint wCodePageId);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCtrlHandler(
			ConsoleCtrlDelegate handlerRoutine,
			bool add);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCursorInfo(
			IntPtr hConsoleOutput,
			[In] ref ConsoleCursorInfo lpConsoleCursorInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCursorPosition(
			IntPtr hConsoleOutput,
			Coord dwCursorPosition);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleDisplayMode(
			IntPtr consoleOutput,
			uint flags,
			out Coord newScreenBufferDimensions);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleHistoryInfo(
			ConsoleHistoryInfo consoleHistoryInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleMode(
			IntPtr hConsoleHandle, 
			uint dwMode);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleOutputCP(
			uint wCodePageId);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleScreenBufferInfoEx(
			IntPtr consoleOutput,
			ConsoleScreenBufferInfoEx consoleScreenBufferInfoEx);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleScreenBufferSize(
			IntPtr hConsoleOutput,
			Coord dwSize);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleTextAttribute(
			IntPtr hConsoleOutput,
			ushort wAttributes);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleTitle(
			string lpConsoleTitle);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleWindowInfo(
			IntPtr hConsoleOutput,
			bool bAbsolute,
			[In] ref SmallRect lpConsoleWindow);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetCurrentConsoleFontEx(
			IntPtr consoleOutput, 
			bool maximumWindow,
			ConsoleFontInfoEx consoleCurrentFontEx);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetStdHandle(
			uint nStdHandle,
			IntPtr hHandle);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool WriteConsole(
			IntPtr hConsoleOutput,
			string lpBuffer,
			uint nNumberOfCharsToWrite,
			out uint lpNumberOfCharsWritten,
			IntPtr lpReserved);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool WriteConsoleInput(
			IntPtr hConsoleInput,
			InputRecord[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool WriteConsoleOutput(
			IntPtr hConsoleOutput,
			CharInfo[] lpBuffer,
			Coord dwBufferSize,
			Coord dwBufferCoord,
			ref SmallRect lpWriteRegion);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool WriteConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			ushort[] lpAttribute,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfAttrsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool WriteConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			string lpCharacter, 
			uint nLength, 
			Coord dwWriteCoord,
			out uint lpNumberOfCharsWritten);

		[StructLayout(LayoutKind.Sequential)]
		internal struct Coord
		{
			internal short X;
			internal short Y;
		}

		internal struct SmallRect
		{
			internal short Left;
			internal short Top;
			internal short Right;
			internal short Bottom;
		}

		internal struct ConsoleScreenBufferInfo
		{
			internal Coord DwSize;
			internal Coord DwCursorPosition;
			internal short WAttributes;
			internal SmallRect SrWindow;
			internal Coord DwMaximumWindowSize;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct ConsoleScreenBufferInfoEx
		{
			internal uint cbSize;
			internal Coord dwSize;
			internal Coord dwCursorPosition;
			internal short wAttributes;
			internal SmallRect srWindow;
			internal Coord dwMaximumWindowSize;

			internal ushort wPopupAttributes;
			internal bool bFullscreenSupported;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			internal Colorref[] ColorTable;

			internal static ConsoleScreenBufferInfoEx Create()
			{
				return new ConsoleScreenBufferInfoEx { cbSize = 96 };
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct Colorref
		{
			internal uint ColorDWORD;

			internal Colorref(System.Drawing.Color color)
			{
				this.ColorDWORD = color.R + ((uint)color.G << 8) + ((uint)color.B << 16);
			}

			internal System.Drawing.Color GetColor()
			{
				return System.Drawing.Color.FromArgb(
					(int)(0x000000FFU & this.ColorDWORD),
					(int)(0x0000FF00U & this.ColorDWORD) >> 8,
					(int)(0x00FF0000U & this.ColorDWORD) >> 16);
			}

			internal void SetColor(System.Drawing.Color color)
			{
				this.ColorDWORD = color.R + ((uint)color.G << 8) + ((uint)color.B << 16);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct ConsoleFontInfo
		{
			internal int nFont;
			internal Coord dwFontSize;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal unsafe struct ConsoleFontInfoEx 
		{
			internal uint cbSize;
			internal uint nFont;
			internal Coord dwFontSize;
			internal ushort FontFamily;
			internal ushort FontWeight;
			private const int LfFacesize = 32;
			private fixed char faceName[LfFacesize];
		} 

		[StructLayout(LayoutKind.Explicit)]
		internal struct InputRecord
		{
			[FieldOffset(0)] internal ushort EventType;
			[FieldOffset(4)] internal KeyEventRecord KeyEvent;
			[FieldOffset(4)] internal MouseEventRecord MouseEvent;
			[FieldOffset(4)] internal WindowBufferSizeRecord WindowBufferSizeEvent;
			[FieldOffset(4)] internal MenuEventRecord MenuEvent;
			[FieldOffset(4)] internal FocusEventRecord FocusEvent;
		}

		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		internal struct KeyEventRecord
		{
			[FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
			internal bool bKeyDown;

			[FieldOffset(4), MarshalAs(UnmanagedType.U2)]
			internal ushort wRepeatCount;

			[FieldOffset(6), MarshalAs(UnmanagedType.U2)]
			internal ushort wVirtualKeyCode;

			[FieldOffset(8), MarshalAs(UnmanagedType.U2)]
			internal ushort wVirtualScanCode;

			[FieldOffset(10)]
			internal char UnicodeChar;

			[FieldOffset(12), MarshalAs(UnmanagedType.U4)]
			internal uint dwControlKeyState;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MouseEventRecord
		{
			internal Coord dwMousePosition;
			internal uint dwButtonState;
			internal uint dwControlKeyState;
			internal uint dwEventFlags;
		}

		internal struct WindowBufferSizeRecord
		{
			internal Coord DwSize;

			internal WindowBufferSizeRecord(short x, short y)
			{
				this.DwSize = new Coord
				{
					X = x,
					Y = y
				};
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MenuEventRecord
		{
			internal uint dwCommandId;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct FocusEventRecord
		{
			internal uint bSetFocus;
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct CharInfo
		{
			// THESE OFFSETS MIGHT BE WRONG! I DON'T KNOW WHAT I'M DOING!
			[FieldOffset(0)] private readonly char UnicodeChar;
			[FieldOffset(2)] private readonly char AsciiChar;
			[FieldOffset(4)] private readonly ushort Attributes;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct ConsoleCursorInfo
		{
			private readonly uint Size;
			private readonly bool Visible;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct ConsoleHistoryInfo
		{
			private readonly ushort cbSize;
			private readonly ushort HistoryBufferSize;
			private readonly ushort NumberOfHistoryBuffers;
			private readonly uint dwFlags;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct ConsoleSelectionInfo 
		{
			private readonly uint Flags;
			private readonly Coord SelectionAnchor;
			private readonly SmallRect Selection;
		}
	}
}