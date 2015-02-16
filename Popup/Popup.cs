using System;
using UnityEngine;

namespace Gem
{
	public class Popup : MonoBehaviour
	{
		public bool isOpened { get; private set; }

		public Action onClose;
		
		public void Open(object _arg)
		{
			isOpened = true;
			DoOpen();
		}

		public virtual void Close()
		{
			isOpened = false;
			DoClose();
			onClose.CheckAndCall();
		}

		protected virtual void DoOpen() {}

		protected virtual void DoClose()
		{
			Destroy(gameObject);
		}

		public virtual void ComeToBG()
		{
			gameObject.SetActive(false);
		}

		public virtual void ComeToFG()
		{
			gameObject.SetActive(true);
		}
	}
}
