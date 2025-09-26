using System;
using UI;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "TimeConfig", menuName = "Time/TimeConfig")]
    public class TimeData : ScriptableObject, IData
    {
        [SerializeField] private int _currentYear;
        [SerializeField] private Month _currentMonth;
        [SerializeField] private int _currentDay;
        [SerializeField] private float _timePerDayInSeconds = 1f;
        [SerializeField] private float _bubbleTimeDivider = 2f;

        private int _weekDays;

        public event Action OnDayChanged;
        public event Action OnWeekChange;
        public event Action OnMonthChanged; 
        public event Action OnYearChanged; 
        
        public float TimePerDayInSeconds => _timePerDayInSeconds;
        public float BubbleTimeDivider => _bubbleTimeDivider;
        public int CurrentYear => _currentYear;
        public Month CurrentMonth => _currentMonth;
        public int CurrentDay => _currentDay;

        public void SetNextDay()
        {
            _currentDay++;
            ChangeWeek();
            
            OnDayChanged?.Invoke();

            switch (_currentMonth)
            {
                case Month.January when _currentDay == 31:
                case Month.February when _currentDay == 28:
                case Month.March when _currentDay == 31:
                case Month.April when _currentDay == 30:
                case Month.May when _currentDay == 31:
                case Month.June when _currentDay == 30:
                case Month.July when _currentDay == 31:
                case Month.August when _currentDay == 31:
                case Month.September when _currentDay == 30:
                case Month.October when _currentDay == 31:
                case Month.November when _currentDay == 30:
                case Month.December when _currentDay == 31:
                    SetNextMonth();
                    break;
                case Month.None:
                default:
                    break;
            }
        }

        private void SetNextMonth()
        {
            if (_currentMonth == Month.December)
            {
                _currentMonth = Month.January;
                _currentDay = 1;
                SetNextYear();
                return;
            }
            _currentMonth++;
            _currentDay = 1;
            
            OnMonthChanged?.Invoke();
        }

        private void ChangeWeek()
        {
            _weekDays++;

            if (_weekDays >= 7)
            {
                OnWeekChange?.Invoke();
                _weekDays = 0;
            }
        }

        private void SetNextYear()
        {
            _currentYear++;
            OnYearChanged?.Invoke();
        }

        public void SetDay(int i)
        {
            _currentDay = i;
        }

        public void ResetTimeData()
        {
            _currentYear = 1;
            _currentMonth = Month.January;
            _currentDay = 1;
        }
    }
}