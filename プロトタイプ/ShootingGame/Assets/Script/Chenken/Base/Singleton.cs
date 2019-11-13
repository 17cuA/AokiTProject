using System;
using System.Collections.Generic;

/// <summary>
/// new()で生成するテンプレートシングルトン
/// XXX.Instance.YYY()でクラスに直接にアクセスできる
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : new()
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
				instance = new T();
			return instance;
		}
	}
}

