namespace Deep.Magic
{
	using System;
	using System.Runtime.InteropServices;
	using System.Text;

	public static class Constants
	{
		public const int StdInput = -10;
		public const int StdOutput = -11;
	}

	public class Bindings
	{
		[DllImport("kernel32", SetLastError = true)]
		public static extern bool AddConsoleAlias(
			string source,
			string target,
			string exeName);

		[DllImport("kernel32", SetLastError = true)]
		public static extern bool AllocConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool AttachConsole(
			uint dwProcessId);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateConsoleScreenBuffer(
			uint dwDesiredAccess,
			uint dwShareMode, 
			IntPtr lpSecurityAttributes, 
			uint dwFlags,
			IntPtr lpScreenBufferData);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FillConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			ushort wAttribute, 
			uint nLength, 
			Coord dwWriteCoord, 
			out uint lpNumberOfAttrsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FillConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			char cCharacter,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfCharsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FlushConsoleInputBuffer(
			IntPtr hConsoleInput);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		public static extern bool FreeConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GenerateConsoleCtrlEvent(
			uint dwCtrlEvent,
			uint dwProcessGroupId);

		[DllImport("kernel32", SetLastError = true)]
		public static extern bool GetConsoleAlias(
			string source,
			out StringBuilder targetBuffer,
			uint targetBufferLength,
			string exeName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleAliases(
			StringBuilder[] lpTargetBuffer,
			uint targetBufferLength,
			string lpExeName);

		[DllImport("kernel32", SetLastError = true)]
		public static extern uint GetConsoleAliasesLength(
			string exeName);

		[DllImport("kernel32", SetLastError = true)]
		public static extern uint GetConsoleAliasExes(
			out StringBuilder exeNameBuffer,
			uint exeNameBufferLength);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleAliasExesLength();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleCP();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleCursorInfo(
			IntPtr hConsoleOutput,
			out ConsoleCursorInfo lpConsoleCursorInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleDisplayMode(
			out uint modeFlags);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern Coord GetConsoleFontSize(
			IntPtr hConsoleOutput,
			int nFont);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleHistoryInfo(
			out ConsoleHistoryInfo consoleHistoryInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleMode(
			IntPtr hConsoleHandle,
			out uint lpMode);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleOriginalTitle(
			out StringBuilder consoleTitle,
			uint size);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleOutputCP();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleProcessList(
			out uint[] processList,
			uint processCount);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleScreenBufferInfo(
			IntPtr hConsoleOutput,
			out ConsoleScreenBufferInfo lpConsoleScreenBufferInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleScreenBufferInfoEx(
			IntPtr hConsoleOutput,
			ref ConsoleScreenBufferInfoEx consoleScreenBufferInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleSelectionInfo(
			ConsoleSelectionInfo consoleSelectionInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleTitle(
			[Out] StringBuilder lpConsoleTitle,
			uint nSize);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetCurrentConsoleFont(
			IntPtr hConsoleOutput,
			bool bMaximumWindow,
			out ConsoleFontInfo lpConsoleCurrentFont);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetCurrentConsoleFontEx(
			IntPtr consoleOutput,
			bool maximumWindow,
			out ConsoleFontInfoEx consoleCurrentFont);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern Coord GetLargestConsoleWindowSize(
			IntPtr hConsoleOutput);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetNumberOfConsoleInputEvents(
			IntPtr hConsoleInput,
			out uint lpcNumberOfEvents);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetNumberOfConsoleMouseButtons(
			ref uint lpNumberOfMouseButtons);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetStdHandle(
			int nStdHandle);

		public delegate bool ConsoleCtrlDelegate(CtrlTypes ctrlType);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool PeekConsoleInput(
			IntPtr hConsoleInput,
			[Out] InputRecord[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsole(
			IntPtr hConsoleInput,
			[Out] StringBuilder lpBuffer,
			uint nNumberOfCharsToRead,
			out uint lpNumberOfCharsRead,
			IntPtr lpReserved);

		[DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
		public static extern bool ReadConsoleInput(
			IntPtr hConsoleInput,
			[Out] InputRecord[] lpBuffer,
			//out InputRecord[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsoleOutput(
			IntPtr hConsoleOutput,
			[Out] CharInfo[] lpBuffer,
			Coord dwBufferSize,
			Coord dwBufferCoord,
			ref SmallRect lpReadRegion);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			[Out] ushort[] lpAttribute,
			uint nLength,
			Coord dwReadCoord,
			out uint lpNumberOfAttrsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			[Out] StringBuilder lpCharacter,
			uint nLength,
			Coord dwReadCoord,
			out uint lpNumberOfCharsRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ScrollConsoleScreenBuffer(
			IntPtr hConsoleOutput,
			[In] ref SmallRect lpScrollRectangle,
			IntPtr lpClipRectangle,
			Coord dwDestinationOrigin,
			[In] ref CharInfo lpFill);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleActiveScreenBuffer(
			IntPtr hConsoleOutput);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCP(
			uint wCodePageId);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCtrlHandler(
			ConsoleCtrlDelegate handlerRoutine,
			bool add);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCursorInfo(
			IntPtr hConsoleOutput,
			[In] ref ConsoleCursorInfo lpConsoleCursorInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCursorPosition(
			IntPtr hConsoleOutput,
			Coord dwCursorPosition);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleDisplayMode(
			IntPtr consoleOutput,
			uint flags,
			out Coord newScreenBufferDimensions);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleHistoryInfo(
			ConsoleHistoryInfo consoleHistoryInfo);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleMode(
			IntPtr hConsoleHandle, 
			uint dwMode);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleOutputCP(
			uint wCodePageId);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleScreenBufferInfoEx(
			IntPtr consoleOutput,
			ConsoleScreenBufferInfoEx consoleScreenBufferInfoEx);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleScreenBufferSize(
			IntPtr hConsoleOutput,
			Coord dwSize);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleTextAttribute(
			IntPtr hConsoleOutput,
			ushort wAttributes);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleTitle(
			string lpConsoleTitle);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleWindowInfo(
			IntPtr hConsoleOutput,
			bool bAbsolute,
			[In] ref SmallRect lpConsoleWindow);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetCurrentConsoleFontEx(
			IntPtr consoleOutput, 
			bool maximumWindow,
			ConsoleFontInfoEx consoleCurrentFontEx);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetStdHandle(
			uint nStdHandle,
			IntPtr hHandle);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsole(
			IntPtr hConsoleOutput,
			string lpBuffer,
			uint nNumberOfCharsToWrite,
			out uint lpNumberOfCharsWritten,
			IntPtr lpReserved);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleInput(
			IntPtr hConsoleInput,
			InputRecord[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleOutput(
			IntPtr hConsoleOutput,
			CharInfo[] lpBuffer,
			Coord dwBufferSize,
			Coord dwBufferCoord,
			ref SmallRect lpWriteRegion);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			ushort[] lpAttribute,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfAttrsWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			string lpCharacter, 
			uint nLength, 
			Coord dwWriteCoord,
			out uint lpNumberOfCharsWritten);

		[StructLayout(LayoutKind.Sequential)]
		public struct Coord
		{
			public short X;
			public short Y;
		}

		public struct SmallRect
		{
			public short Left;
			public short Top;
			public short Right;
			public short Bottom;
		}

		public struct ConsoleScreenBufferInfo
		{
			public Coord DwSize;
			public Coord DwCursorPosition;
			public short WAttributes;
			public SmallRect SrWindow;
			public Coord DwMaximumWindowSize;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ConsoleScreenBufferInfoEx
		{
			public uint cbSize;
			public Coord dwSize;
			public Coord dwCursorPosition;
			public short wAttributes;
			public SmallRect srWindow;
			public Coord dwMaximumWindowSize;

			public ushort wPopupAttributes;
			public bool bFullscreenSupported;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public Colorref[] ColorTable;

			public static ConsoleScreenBufferInfoEx Create()
			{
				return new ConsoleScreenBufferInfoEx { cbSize = 96 };
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Colorref
		{
			public uint ColorDWORD;

			public Colorref(System.Drawing.Color color)
			{
				ColorDWORD = color.R + ((uint)color.G << 8) + ((uint)color.B << 16);
			}

			public System.Drawing.Color GetColor()
			{
				return System.Drawing.Color.FromArgb(
					(int)(0x000000FFU & ColorDWORD),
					(int)(0x0000FF00U & ColorDWORD) >> 8,
					(int)(0x00FF0000U & ColorDWORD) >> 16);
			}

			public void SetColor(System.Drawing.Color color)
			{
				ColorDWORD = color.R + ((uint)color.G << 8) + ((uint)color.B << 16);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ConsoleFontInfo
		{
			public int nFont;
			public Coord dwFontSize;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct ConsoleFontInfoEx 
		{
			public uint cbSize;
			public uint nFont;
			public Coord dwFontSize;
			public ushort FontFamily;
			public ushort FontWeight;
			private fixed char FaceName[(int)LfFacesize];
			private const uint LfFacesize = 32;
		} 

		[StructLayout(LayoutKind.Explicit)]
		public struct InputRecord
		{
			[FieldOffset(0)] public ushort EventType;
			[FieldOffset(4)] public KeyEventRecord KeyEvent;
			[FieldOffset(4)] public MouseEventRecord MouseEvent;
			[FieldOffset(4)] public WindowBufferSizeRecord WindowBufferSizeEvent;
			[FieldOffset(4)] public MenuEventRecord MenuEvent;
			[FieldOffset(4)] public FocusEventRecord FocusEvent;
		};

		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct KeyEventRecord
		{
			[FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
			public bool bKeyDown;

			[FieldOffset(4), MarshalAs(UnmanagedType.U2)]
			public ushort wRepeatCount;

			[FieldOffset(6), MarshalAs(UnmanagedType.U2)]
			public ushort wVirtualKeyCode;

			[FieldOffset(8), MarshalAs(UnmanagedType.U2)]
			public ushort wVirtualScanCode;

			[FieldOffset(10)]
			public char UnicodeChar;

			[FieldOffset(12), MarshalAs(UnmanagedType.U4)]
			public uint dwControlKeyState;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MouseEventRecord
		{
			public Coord dwMousePosition;
			public uint dwButtonState;
			public uint dwControlKeyState;
			public uint dwEventFlags;
		}

		public struct WindowBufferSizeRecord
		{
			public Coord DwSize;

			public WindowBufferSizeRecord(short x, short y)
			{
				DwSize = new Coord
				{
					X = x,
					Y = y
				};
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MenuEventRecord
		{
			public uint dwCommandId;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FocusEventRecord
		{
			public uint bSetFocus;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct CharInfo
		{
			// THESE OFFSETS MIGHT BE WRONG! I DON'T KNOW WHAT I'M DOING!
			[FieldOffset(0)] private readonly char UnicodeChar;
			[FieldOffset(2)] private readonly char AsciiChar;
			[FieldOffset(4)] private readonly ushort Attributes;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ConsoleCursorInfo
		{
			private readonly uint Size;
			private readonly bool Visible;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ConsoleHistoryInfo
		{
			private readonly ushort cbSize;
			private readonly ushort HistoryBufferSize;
			private readonly ushort NumberOfHistoryBuffers;
			private readonly uint dwFlags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ConsoleSelectionInfo 
		{
			private readonly uint Flags;
			private readonly Coord SelectionAnchor;
			private readonly SmallRect Selection;
		}

		public enum CtrlTypes : uint
		{
			CtrlCEvent = 0,
			CtrlBreakEvent,
			CtrlCloseEvent,
			CtrlLogoffEvent = 5,
			CtrlShutdownEvent
		}
	}
}