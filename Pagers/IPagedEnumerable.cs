﻿using System.Collections.Generic;

namespace MFramework.Common.Pagers
{
    /// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPagedEnumerable<out T> : IEnumerable<T>
	{
	    /// <summary>
	    ///     Total number of entries across all pages.
	    /// </summary>
	    int TotalCount { get; }
	}
}