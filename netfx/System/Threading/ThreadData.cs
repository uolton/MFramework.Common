﻿#region BSD License
/* 
Copyright (c) 2010, NETFx
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

* Neither the name of Clarius Consulting nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/// <summary>
/// Provides strong-typed access to <see cref="Thread"/> local storage data.
/// </summary>
internal static class ThreadData
{
	/// <summary>
	/// Sets the given data in the <see cref="Thread.CurrentThread"/> storage. The returned 
	/// <see cref="IDisposable"/> can be used to remove the state 
	/// when disposed.
	/// </summary>
	public static IDisposable SetData<T>(T data)
		where T : class
	{
		return Thread.CurrentThread.SetData(data);
	}

	/// <summary>
	/// Gets the data from the <see cref="Thread.CurrentThread"/> storage.
	/// </summary>
	public static T GetData<T>()
		where T : class
	{
		return Thread.CurrentThread.GetData<T>();
	}

	/// <summary>
	/// Sets the given data in the <see cref="Thread"/> storage. The returned 
	/// <see cref="IDisposable"/> can be used to remove the state 
	/// when disposed.
	/// </summary>
	/// <nuget id="netfx-System.Threading.ThreadData" />
	/// <param name="thread" this="true">The thread to set data</param>
	/// <param name="data">The data to be set</param>
	public static IDisposable SetData<T>(this Thread thread, T data)
		where T : class
	{
		return new TransientData<T>(data, thread.GetData<T>());
	}

	/// <summary>
	/// Gets the data from the <see cref="Thread"/> storage.
	/// </summary>
	/// <nuget id="netfx-System.Threading.ThreadData" />
	/// <param name="thread" this="true">The thread to get data from</param>
	public static T GetData<T>(this Thread thread)
		where T : class
	{
		return (T)Thread.GetData(Thread.GetNamedDataSlot(typeof(T).FullName));
	}

	private class TransientData<T> : IDisposable
		where T : class
	{
		private T oldData;
		private LocalDataStoreSlot dataSlot;

		public TransientData(T newData, T oldData)
		{
			this.oldData = oldData;
			this.dataSlot = Thread.GetNamedDataSlot(typeof(T).FullName);
			Thread.SetData(this.dataSlot, newData);
		}

		public void Dispose()
		{
			Thread.SetData(this.dataSlot, oldData);
		}
	}
}
