namespace Deep.Magic
{
	using System.Threading;

	public class Cancellable
	{
		Thread t;
		System.Func<object[], bool> action;
		System.Action<object[]> onFinish;
		System.Action<object[]> onCancel;
		System.Action<object[]> onAnyEnd;
		CancellationTokenSource tokenSource;
		CancellationToken token;

		public Cancellable(
			System.Func<object[], bool> action,
			System.Action<object[]> onFinish = null,
			System.Action<object[]> onCancel = null,
			System.Action<object[]> onAnyEnd = null
		) {
			this.action = action;
			this.onFinish = onFinish;
			this.onCancel = onCancel;
			this.onAnyEnd = onAnyEnd;
			this.tokenSource = new CancellationTokenSource();
			this.token = tokenSource.Token;
		}

		public void Cancel()
		{
			tokenSource.Cancel();
		}

		public bool HasCanceled;
		public bool HasFinished;
		public bool HasEnded;

		public void Run(params object[] args)
		{
			t = new Thread(() => internalRun(args));
			t.Start();
		}

		private void internalRun(object[] args)
		{
			while (true)
			{
				// If cancelled
				if (token.IsCancellationRequested)
				{
					if (onCancel != null)
					{
						onCancel(args);
					}
					HasCanceled = true;
					break;
				}

				// If finished
				if (!this.action(args))
				{
					if (onFinish != null)
					{
						onFinish(args);
					}
					HasFinished = true;
					break;
				}

				Thread.Sleep(100);
			}

			if (onAnyEnd != null)
			{
				onAnyEnd(args);
			}

			HasEnded = true;
		}
	}
}