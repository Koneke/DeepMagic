namespace Deep.Magic
{
	using System.Threading;

	public class Cancellable
	{
		public int Interval;
		private Thread thread;
		private System.Func<object[], bool> action;
		private System.Action<object[]> onFinish;
		private System.Action<object[]> onCancel;
		private System.Action<object[]> onAnyEnd;
		private CancellationTokenSource tokenSource;
		private CancellationToken token;

		public Cancellable(
			System.Func<object[], bool> action,
			System.Action<object[]> onFinish = null,
			System.Action<object[]> onCancel = null,
			System.Action<object[]> onAnyEnd = null
		) {
			this.Interval = 100;
			this.action = action;
			this.onFinish = onFinish;
			this.onCancel = onCancel;
			this.onAnyEnd = onAnyEnd;
			this.tokenSource = new CancellationTokenSource();
			this.token = tokenSource.Token;
		}

		public Cancellable(
			int interval,
			System.Func<object[], bool> action,
			System.Action<object[]> onFinish = null,
			System.Action<object[]> onCancel = null,
			System.Action<object[]> onAnyEnd = null
		) {
			this.Interval = interval;
			this.action = action;
			this.onFinish = onFinish;
			this.onCancel = onCancel;
			this.onAnyEnd = onAnyEnd;
			this.tokenSource = new CancellationTokenSource();
			this.token = this.tokenSource.Token;
		}

		public void Cancel()
		{
			this.tokenSource.Cancel();
		}

		public bool HasCanceled;
		public bool HasFinished;
		public bool HasEnded;

		public void Run(params object[] args)
		{
			this.thread = new Thread(() => this.internalRun(args));
			this.thread.Start();
		}

		private void internalRun(object[] args)
		{
			while (true)
			{
				// If cancelled
				if (this.token.IsCancellationRequested)
				{
					if (this.onCancel != null)
					{
						this.onCancel(args);
					}
					this.HasCanceled = true;
					break;
				}

				// If finished
				if (!this.action(args))
				{
					if (this.onFinish != null)
					{
						this.onFinish(args);
					}
					this.HasFinished = true;
					break;
				}

				Thread.Sleep(this.Interval);
			}

			if (this.onAnyEnd != null)
			{
				this.onAnyEnd(args);
			}

			this.HasEnded = true;
		}
	}
}