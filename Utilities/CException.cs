using System;

class CException:Exception {
	public string Title { get; }
	public int ExitCode { get; }
	public CException(string message, string title = "Error", int iExitCode = -1)
		:base(message)
	{
		Title = title;
		ExitCode = iExitCode;
	}
}

