using System;
using System.Collections.Generic;
using LocalLangLibrary.Models;
using Microsoft.Extensions.Configuration;
using MongoDbAccess.DataAccess.Abstractions;

namespace LocalLangLibrary.DataAccess
{
	public class MongoDbConnection : DbConnectionBase
	{
		private static readonly Dictionary<Type, string> CollectionNames = new()
		{
			[typeof(Category)] = "category",
			[typeof(Expression)] = "expression"
		};

		public MongoDbConnection(IConfiguration config) : base(config.GetDatabaseName(), config.GetConnectionString())
		{
		}

		public override string GetCollectionName<TDocument>()
		{
			if (CollectionNames.TryGetValue(typeof(TDocument), out var name))
			{
				return name;
			}

			throw new ArgumentException("There is no such collection");
		}
	}
}
