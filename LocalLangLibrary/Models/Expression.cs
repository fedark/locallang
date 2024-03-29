﻿using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbAccess;
using MongoDbAccess.Models;

namespace LocalLangLibrary.Models
{
	[CachedCollection(1)]
	public class Expression : IModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[NotNull]
		public string? Id { get; set; }
		public string Word { get; set; }
		public string Translation { get; set; }
		public Category Category { get; set; }
		public int Likes { get; set; } = default;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public Status Status { get; set; } = Status.Pending;

		public Expression(string word, string translation, Category category)
		{
			Word = word;
			Translation = translation;
			Category = category;
		}
	}
}
