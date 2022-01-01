using System;
using System.Collections.Generic;
using System.Linq;
using LogicCore.Extensions;

namespace LogicCore.Tasking.Scheduler.Conditions
{
	public class ByDaysOfWeekCondition : IIntervalCondition
	{
		private readonly HashSet<DayOfWeek> _daysHash;
        private readonly DayOfWeek[] _days;
        private readonly int[] _numbers;
        private readonly bool _hasNumbers;

        public ByDaysOfWeekCondition(params DayOfWeek[] days)
		{
			_daysHash = new HashSet<DayOfWeek>(days);
		}

		public ByDaysOfWeekCondition(DayOfWeek[] days, int[] numbers)
		{
			_daysHash = new HashSet<DayOfWeek>(days);
			_days = days;
			_numbers = numbers;
			_hasNumbers = numbers.Any(n => n != 0);
		}

		public IEnumerable<DayOfWeek> Days
		{
			get { return _daysHash; }
		}


		public string DaysString
		{
			get
			{
				return string.Join(",", GetDayNames());
			}
		}

		private string[] GetDayNames()
		{
			var result = new string[_days.Length];
			for (int i = 0; i < _days.Length; i++)
            {
				result[i] = _numbers[i] == 0 ? _days[i].ToString() : $"{_numbers[i]}{_days[i]}";
            }
			return result;
		}

		#region ITriggerChecker Members

		public bool CheckForReady(DateTime? lastRun, DateTime now)
		{
			var check = _daysHash.Contains(now.DayOfWeek);
			if (check && _hasNumbers)
            {
				for (int i = 0; i < _days.Length; i++)
				{
					check = CheckForReadyWithNumbers(now, _days[i], _numbers[i]);
					if (check) break;
				}
            }
			return check;
		}

		private bool CheckForReadyWithNumbers(DateTime now, DayOfWeek dayOfWeek, int number)
		{
			if (now.DayOfWeek == dayOfWeek)
            {
				if (number != 0)
                {
					var numberDayweekOfMonth = now.Day / 7;
					numberDayweekOfMonth +=numberDayweekOfMonth * 7 == now.Day ? 0 : 1; //корректируем остаток
					if (number > 0)
					{
						return number == numberDayweekOfMonth;
					} 
					if (number == -1)
                    {
						return now.Day > now.EndOfMonth().Day - 7;
                    }
				}
				return true;
            }
			return false;
		}

        public string Key
		{
			get { return "days"; }
		}

		public string Title
		{
			get
			{
				var result = "дни " + DaysString;
				return result;
			}
		}
		#endregion

		public override string ToString()
		{
			return Title;
		}

		public DateTime GetNextTime(DateTime? lastRun, DateTime now)
		{
			if (lastRun != null && lastRun >= now)
				now = lastRun.Value.AddDays(1);

			var next = now.Date.AddMonths(3);
			for (int i = 0; i < _days.Length; i++)
			{
				var ntime = GetNextTime(now, lastRun, _days[i], _numbers[i]);
				if (ntime < next) next = ntime;
			}
			return next;
		}

		private DateTime GetNextTime(DateTime now, DateTime? lastRun, DayOfWeek dayOfWeek, int number) 
		{
			//текущий день тоже может быть
			//if ((lastRun == null || now.Date > lastRun.Value.Date) &&
			//	now.DayOfWeek == dayOfWeek)
			//	return now;

			var start = now.Date;
			var end = now.AddMonths(3);
			var needDayweek = dayOfWeek.GetNumberDayOfWeek();

			while (now <= end)
			{
				DateTime result;
                if (number >= 0)
				{
					if (number == 0)
					{
						//определяем разницу между днями
						var shift = needDayweek - now.GetNumberDayOfWeek();
						if (shift < 0) shift += 7;
						result = now.AddDays(shift);
					}
					else
					{
						//определяем первый день старта в текущем месяце
						var firstDayweekOfMonth = now.StartOfMonth().GetNumberDayOfWeek();
						var firstDayStart = needDayweek - firstDayweekOfMonth + 1;
						//if (firstDayStart == 1) firstDayStart = needDayweek;
						if (firstDayStart <= 0) firstDayStart += 7;
						//получаем смещение следующего запуска
						var shift =	firstDayStart + (number - 1) * 7;
						result = now.StartOfMonth().AddDays(shift - 1);
					}
				}
				else
				{
					//определяем последний день старта в текущем месяце
					var lastDayweekOfMonth = now.EndOfMonth().GetNumberDayOfWeek();
					var lastDayStart = needDayweek - lastDayweekOfMonth;
					if (lastDayStart > 0) lastDayStart -= 7;

					//получаем смещение следующего запуска
					var shift = lastDayStart + (number + 1) * 7;
					result = now.EndOfMonth().Date.AddDays(shift);
				}
				if (result >= start && (lastRun == null || result > lastRun))
					return result;
				now = now.AddMonths(1).StartOfMonth();
			}
			throw new FormatException($"Указан несуществующий день недели {number} {dayOfWeek}");
		}
    }
}
