using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace GestionVols.Controllers.Class.ControllerObjects
{
    public abstract class TimedApp
    {
        readonly TimeSpan startingDelay = TimeSpan.Zero;
        readonly TimeSpan timerInterval = TimeSpan.FromSeconds(1);
        public TimeSpan timerTotalTime { get; protected set; }
        public bool IsRunning = false;
        public TimeSpan timerActionCountdown { get; protected set; }

        public int interogated = 0;
        public TimeSpan timeToAction { get; protected set; }
        private Timer _TIMER;
        private delegate void __Action();
        private __Action action;


        public bool autoReset { get; private set; }

        public abstract void TimedAction();

        public TimedApp(TimeSpan _timeToAction, bool _autoReset = false)
        {
            _TIMER = null;
            timerTotalTime = TimeSpan.Zero;
            timerActionCountdown = _timeToAction;
            timeToAction = _timeToAction;
            autoReset = _autoReset;
            action += TimedAction;   
        }

        public void BeginTimer()
        {
            _TIMER = new Timer(CheckTimerStatus, new AutoResetEvent(autoReset), startingDelay, timerInterval);
            IsRunning = true;
        }

        public void DisposeTimer()
        {
            _TIMER.Dispose();
            IsRunning = false;
        }

        public void StopTask()
        {
            if(_TIMER != null)
            {
                _TIMER.Change(Timeout.Infinite, Timeout.Infinite);
                IsRunning = false ;
            }
                
        }

        public void StartTask()
        {
            if(_TIMER != null)
            {
                _TIMER.Change(startingDelay, timerInterval);
                IsRunning = true;
            }
                
        }

        private void CheckTimerStatus(Object stateInfo)
        {
            if (IsRunning)
            {
                timerTotalTime += timerInterval;
                timerActionCountdown -= timerInterval;
                interogated++;

                if (timerActionCountdown < TimeSpan.Zero)
                {
                    timerActionCountdown = timeToAction;
                    action?.Invoke();
                }
            }

        }

        

    }
}