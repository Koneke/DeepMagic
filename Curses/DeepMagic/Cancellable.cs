namespace Deep.Magic
{
	using System;
	using System.Threading;

	public class Cancellable
	{
		private int interval;

		public bool IsCancelling { get; private set; }
		public bool HasCanceled { get; private set; }
		public bool HasFinished { get; private set; }
		public bool HasEnded { get; private set; }

		private Thread thread;
		private Func<object[], bool> action;
		private Action<object[]> onFinish;
		private Action<object[]> onCancel;
		private Action<object[]> onAnyEnd;
		private CancellationTokenSource tokenSource;
		private CancellationToken token;

		public Cancellable(
			Func<object[], bool> action,
			Action<object[]> onFinish = null,
			Action<object[]> onCancel = null,
			Action<object[]> onAnyEnd = null
		) {
			this.interval = 100;
			this.action = action;
			this.onFinish = onFinish;
			this.onCancel = onCancel;
			this.onAnyEnd = onAnyEnd;
			this.tokenSource = new CancellationTokenSource();
			this.token = tokenSource.Token;
		}

		public Cancellable(
			int interval,
			Func<object[], bool> action,
			Action<object[]> onFinish = null,
			Action<object[]> onCancel = null,
			Action<object[]> onAnyEnd = null
		) {
			this.interval = interval;
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
			this.IsCancelling = true;
		}

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

				if (this.interval != -1)
				{
					Thread.Sleep(this.interval);
				}
			}

			if (this.onAnyEnd != null)
			{
				this.onAnyEnd(args);
			}
			this.HasEnded = true;
		}
	}
}