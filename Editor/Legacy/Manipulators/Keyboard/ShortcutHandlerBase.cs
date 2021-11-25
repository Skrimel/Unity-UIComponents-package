using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Farious.Gist.UIComponents
{
	public abstract class ShortcutHandlerBase<TRoot>
	{
		List<KeyCode> LastTryCombination = new List<KeyCode>();
		public abstract List<KeyCode> NecessaryKeys { get; }

		public abstract bool IsRepeatAvailable { get; }

		public abstract float RepeatStartThreshold { get; }
		public float CurrentStartThresholdProgress;

		public abstract float TimeBetweenRepeats { get; }
		public float CurrentTimeBetweenRepeats;

		KeyboardController<TRoot> _controller;

		public bool IsSequenceCorrect
		{
			get
			{
				bool result = true;

				NecessaryKeys.ForEach((KeyCode code) =>
					result &= _controller.KeysDown.Contains(code));

				result &= _controller.KeysDown.Count == NecessaryKeys.Count;

				return result;
			}
		}

		public abstract void Handler();

		public void TryHandle()
		{
			bool result = IsSequenceCorrect;

			result &= AdditionalCheck();

			//if (necessaryKeys.SequenceEqual(new List<KeyCode>{KeyCode.DownArrow}))
			//    Debug.Log("Is sequence correct: " + IsSequenceCorrect + "; Is additional check passed: " + AdditionalCheck());

			if (result && (!LastTryCombination.SequenceEqual(_controller.KeysDown) ||
			               (IsRepeatAvailable && CurrentStartThresholdProgress >= RepeatStartThreshold &&
			                CurrentTimeBetweenRepeats >= TimeBetweenRepeats)))
			{
				if (IsRepeatAvailable)
					CurrentTimeBetweenRepeats = 0f;

				Handler();
			}

			LastTryCombination = new List<KeyCode>(_controller.KeysDown);
		}

		public void UpdateTimers()
		{
			if (IsSequenceCorrect)
			{
				if (CurrentStartThresholdProgress < RepeatStartThreshold)
					CurrentStartThresholdProgress += Time.deltaTime;
				else
					CurrentTimeBetweenRepeats += Time.deltaTime;
			}
			else
			{
				CurrentStartThresholdProgress = 0f;
				CurrentTimeBetweenRepeats = 0f;
			}
		}

		public virtual bool AdditionalCheck()
		{
			return true;
		}

		public ShortcutHandlerBase(KeyboardController<TRoot> controller)
		{
			_controller = controller;
		}
	}
}