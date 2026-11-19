using Npgsql;

namespace kaapp.API.Helpers
{
	public class DatabaseContext
	{
		private readonly NpgsqlDataSource _dataSource;

		public NpgsqlConnection? Connection { get; private set; }
		public NpgsqlTransaction? Transaction { get; private set; }

		public DatabaseContext(NpgsqlDataSource dataSource)
		{
			_dataSource = dataSource;
		}

		public async Task BeginTransactionAsync()
		{
			if(Connection != null)
			{
				throw new InvalidOperationException("Eine Transaction ist bereits gestartet.");
			}

			Connection = await _dataSource.OpenConnectionAsync();
			Transaction = await Connection.BeginTransactionAsync();
		}

		public async Task CommitAsync()
		{
			if (Transaction != null)
			{
				await Transaction.CommitAsync();
			}

			await DisposeAsync();
		}

		public async Task RollbackAsync()
		{
			if (Transaction != null)
			{
				await Transaction.RollbackAsync();
			}
			await DisposeAsync();
		}

		private async Task DisposeAsync()
		{
			if (Transaction != null)
			{
				await Transaction.DisposeAsync();
				Transaction = null;
			}
			if (Connection != null)
			{
				await Connection.CloseAsync();
				await Connection.DisposeAsync();
				Connection = null;
			}
		}

		public NpgsqlCommand CreateCommand(string sql)
		{
			var command = new NpgsqlCommand(sql, Connection);
			command.Transaction = Transaction;
			return command;
		}
	}
}
